using AuctionPlatform.Business.Response;
using AuctionPlatform.Domain._DTO.Auction;
using AuctionPlatform.Domain.Entities;
using AuctionPlatform.Domain.Enums;
using AuctionPlatforn.Infrastructure.Repositories.Auctions;
using AuctionPlatforn.Infrastructure.Repositories.Users;
using AutoMapper;

namespace AuctionPlatform.Business.Auctions
{
    public class AuctionService : IAuctionService
    {
        private readonly IAuctionRepository _auctionRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public AuctionService(IAuctionRepository auctionRepository, IMapper mapper, IUserRepository userRepository)
        {
            _auctionRepository = auctionRepository;
            _mapper = mapper;
            _userRepository = userRepository;
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

            if (auction == null || auction.AuctionStatus != AuctionStatusEnum.Open || auction.EndTime <= DateTime.Now 
                )
            {
                return new Response.Response { Success = false, Message = "Invalid bid." };
            }

            var bidder = await _userRepository.GetUserByIdAsync(bidRequestDto.BidderUserId);
            if (bidder == null || bidder.WalletAmount < bidRequestDto.BidAmount)
            {
                return new Response.Response { Success = false, Message = "Bidder has insufficient funds." };
            }

            bidder.WalletAmount -= bidRequestDto.BidAmount;

            await _userRepository.UpdateAsync(bidder);

            auction.CurrentBid = bidRequestDto.BidAmount;
            auction.HighestBidderId = bidRequestDto.BidderUserId;

            //might need to do this with a hangfire job
            if (auction.EndTime <= DateTime.Now)
            {
                var auctionOwner = auction.User;
                auctionOwner.WalletAmount += auction.TotalBidAmount;
                auction.AuctionStatus = AuctionStatusEnum.Closed;
            }
            auction.TotalBidAmount += bidRequestDto.BidAmount;

            await _auctionRepository.UpdateAsync(auction);

            return new Response.Response { Success = true, Message = "Bid successful." };
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

        public async Task<bool>Delete(int id)
        {
            var auction = await _auctionRepository.GetById(id);

            if (auction is null) return false;

            await _auctionRepository.DeleteAsync(auction);

            return true;
        }
    }
}
