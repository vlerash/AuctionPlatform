using AuctionPlatform.Domain.Entities;

namespace AuctionPlatforn.Infrastructure.Repositories.Auctions
{
    public class AuctionRepository : GenericRepository<Auction>, IAuctionRepository
    {
        public AuctionRepository(AuctionPlatformDbContext context) : base(context)
        {
        }
    }
}
