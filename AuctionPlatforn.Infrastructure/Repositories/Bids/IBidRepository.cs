using AuctionPlatform.Domain.Entities;

namespace AuctionPlatforn.Infrastructure.Repositories.Bids
{
    public interface IBidRepository : IGenericRepository<Bid>
    {
        Task<Bid> GetBidByIdAsync(int bidId);
    }
}
