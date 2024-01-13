using AuctionPlatform.Domain.Entities;

namespace AuctionPlatforn.Infrastructure.Repositories.Auctions
{
    public interface IAuctionRepository : IGenericRepository<Auction>
    {
        Task<IList<Auction>> GetActiveAuctions();
        Task<Auction> GetAuctionById(int auctionId);
        Task<IList<Auction>> GetCurrentAuctionsByTimeLeftAscending();
    }
}
