using AuctionPlatform.Business._01_Common;
using AuctionPlatform.Domain._DTO.Auction;
using AuctionPlatform.Domain.Entities;

namespace AuctionPlatform.Business.Auctions
{
    public interface IAuctionService : IService
    {
        void CheckAndCompleteAuctions();
        Task<AuctionDto> CreateAuction(AuctionCreateDto auctionCreateDto);
        Task<bool> Delete(int id);
        Task<IList<AuctionDto>> GetActiveAuctions();
        Task<AuctionDto> GetAuctionById(int auctionId);
        Task<IList<AuctionDto>> GetCurrentAuctionsByTimeLeftAscending();
        Task<Response.Response> PlaceBid(BidRequestDto bidRequestDto);
    }
}
