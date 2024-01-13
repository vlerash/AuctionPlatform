using AuctionPlatforn.Infrastructure.Repositories.Users;

namespace AuctionPlatform.Business.Users
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
    }
}
