using AuctionPlatform.Domain.Entities;

namespace AuctionPlatforn.Infrastructure.Repositories.Users
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(AuctionPlatformDbContext context) : base(context)
        {
        }
    }
}
