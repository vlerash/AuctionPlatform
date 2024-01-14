using AuctionPlatform.Domain.Entities;
using AuctionPlatforn.Infrastructure.Repositories.Bids;

namespace AuctionPlatforn.Infrastructure.Repositories.Users
{
    public class BidRepository : GenericRepository<Bid>, IBidRepository
    {
        public BidRepository(AuctionPlatformDbContext context) : base(context)
        {
        }

        public async Task<Bid> GetBidByIdAsync(int bidId)
        {
            return await DbSet.FindAsync(bidId);
        }
    }
}
