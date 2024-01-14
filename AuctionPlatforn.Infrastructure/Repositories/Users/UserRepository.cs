using AuctionPlatform.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuctionPlatforn.Infrastructure.Repositories.Users
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(AuctionPlatformDbContext context) : base(context)
        {
        }

        public async Task<User> GetUserById(int userId)
        {
            return await DbSet.Include(x => x.CreatedAuctions)
                            //.Include(x => x.BiddedAuctions)
                            .Include(x => x.PlacedBids)
                            .FirstOrDefaultAsync();
        }
    }
}
