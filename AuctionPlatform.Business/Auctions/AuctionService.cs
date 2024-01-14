using AuctionPlatform.Domain._DTO.Auction;
using AuctionPlatform.Domain.Entities;
using AuctionPlatform.Domain.Enums;
using AuctionPlatforn.Infrastructure.Repositories.Auctions;
using AuctionPlatforn.Infrastructure.Repositories.Bids;
using AuctionPlatforn.Infrastructure.Repositories.Users;
using AutoMapper;

namespace AuctionPlatform.Business.Auctions
{
    public class AuctionService : IAuctionService
    {
        private readonly IAuctionRepository _auctionRepository;
        private readonly IUserRepository _userRepository;
        private readonly IBidRepository _bidRepository;
        private readonly IMapper _mapper;

        public AuctionService(IAuctionRepository auctionRepository, IMapper mapper, IUserRepository userRepository, IBidRepository bidRepository)
        {
            _auctionRepository = auctionRepository;
            _mapper = mapper;
            _userRepository = userRepository;
            _bidRepository = bidRepository;
        }

        public async Task<IList<AuctionDto>> GetCurrentAuctionsByTimeLeftAscending()
        {
            var auctions = await _auctionRepository.GetCurrentAuctionsByTimeLeftAscending();

            return _mapper.Map<IList<AuctionDto>>(auctions);
        }

        public async Task<IList<AuctionDto>> GetActiveAuctions()
        {
            var activeAuctions = await _auctionRepository.GetActiveAuctions();

            return activeAuctions.Select(auction => _mapper.Map<AuctionDto>(auction)).ToList();
        }

        public async Task<AuctionDto> CreateAuction(AuctionCreateDto auctionCreateDto)
        {
            var mappedAuction = _mapper.Map<Auction>(auctionCreateDto);

            var createdAuction = await _auctionRepository.CreateAsync(mappedAuction);

            return _mapper.Map<AuctionDto>(createdAuction);
        }

     
        public async Task<Response.Response> PlaceBid(BidRequestDto bidRequestDto)
        {
            var auction = await _auctionRepository.GetAuctionById(bidRequestDto.AuctionId);

            if (auction == null || auction.AuctionStatus != AuctionStatusEnum.Open || auction.EndTime <= DateTime.Now)
            {
                return new Response.Response { Success = false, Message = "Invalid bid." };
            }

            var bidder = await _userRepository.GetUserByIdAsync(bidRequestDto.BidderUserId);
            if (bidder == null || bidder.WalletAmount < bidRequestDto.BidAmount)
            {
                return new Response.Response { Success = false, Message = "Bidder has insufficient funds." };
            }

            if (bidRequestDto.BidAmount > auction.CurrentBid)
            {
                try
                {
                    var newBid = new Bid
                    {
                        AuctionId = auction.Id,
                        BidderUserId = bidder.Id,
                        Amount = bidRequestDto.BidAmount,
                        BidTime = DateTime.Now
                    };

                    _bidRepository.Create(newBid);

                    auction.Bids.Add(newBid);

                    auction.CurrentBid = bidRequestDto.BidAmount;
                    auction.HighestBidderId = bidder.Id;

                    bidder.WalletAmount -= bidRequestDto.BidAmount;

                    await _userRepository.UpdateAsync(bidder);
                    await _auctionRepository.UpdateAsync(auction);

                    return new Response.Response { Success = true, Message = "Bid successful." };
                }
                catch (Exception ex)
                {
                    return new Response.Response { Success = false, Message = "An error occurred while placing the bid." };
                }
            }

            return new Response.Response { Success = false, Message = "Bid amount is not higher than the current highest bid." };
        }

        public void CheckAndCompleteAuctions()
        {
            var endedAuctions = _auctionRepository.GetEndedAuctions().Result;

            foreach (var auction in endedAuctions)
            {
                HandleAuctionCompletion(auction);
            }
        }

        private void HandleAuctionCompletion(Auction auction)
        {
            var auctionOwner = auction.User;

            var highestBidder = auction.HighestBidder;
            highestBidder.WalletAmount -= auction.CurrentBid;
            auctionOwner.WalletAmount += auction.CurrentBid;

            auction.AuctionStatus = AuctionStatusEnum.Closed;

            _userRepository.UpdateAsync(highestBidder);
            _userRepository.UpdateAsync(auctionOwner);
            _auctionRepository.UpdateAsync(auction);
        }

        public async Task<AuctionDto> GetAuctionById(int auctionId)
        {
            var auction = await _auctionRepository.GetAuctionById(auctionId);

            if (auction == null)
            {
                return null;
            }

            return _mapper.Map<AuctionDto>(auction);
        }

        public async Task<bool> Delete(int id)
        {
            var auction = await _auctionRepository.GetById(id);

            if (auction is null) return false;

            await _auctionRepository.DeleteAsync(auction);

            return true;
        }
    }
}
