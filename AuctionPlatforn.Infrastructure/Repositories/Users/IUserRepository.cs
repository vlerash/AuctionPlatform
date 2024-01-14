using AuctionPlatform.Domain.Entities;

namespace AuctionPlatforn.Infrastructure.Repositories.Users
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> GetUserById(int userId);
    }
}
