using AuctionPlatform.Domain.Entities;

namespace AuctionPlatforn.Infrastructure.Repositories.Auctions
{
    public interface IAuctionRepository : IGenericRepository<Auction>
    {
        Task<Auction> GetAuctionById(int auctionId);
        Task<IList<Auction>> GetCurrentAuctionsByTimeLeftAscending();
        Task<IList<Auction>> GetEndedAuctions();
    }
}
