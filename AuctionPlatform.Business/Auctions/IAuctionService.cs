using AuctionPlatform.Business._01_Common;
using AuctionPlatform.Domain._DTO.Auction;

namespace AuctionPlatform.Business.Auctions
{
    public interface IAuctionService : IService
    {
        Task<AuctionDto> CreateAuction(AuctionCreateDto auctionCreateDto);
        Task<bool> Delete(int id);
        Task<IList<AuctionDto>> GetActiveAuctions();
        Task<AuctionDto> GetAuctionById(int auctionId);
        Task<IList<AuctionDto>> GetCurrentAuctionsByTimeLeftAscending();
        Task<Response.Response> PlaceBid(BidRequestDto bidRequestDto);
    }
}
