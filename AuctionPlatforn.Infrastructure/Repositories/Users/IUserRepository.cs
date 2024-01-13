using AuctionPlatform.Domain.Entities;

namespace AuctionPlatforn.Infrastructure.Repositories.Users
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> GetUserByIdAsync(int userId);
    }
}
