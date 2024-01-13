using AuctionPlatform.Domain.Entities;

namespace AuctionPlatforn.Infrastructure.Repositories.Users
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(AuctionPlatformDbContext context) : base(context)
        {
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            return await DbSet.FindAsync(userId);
        }
    }
}
