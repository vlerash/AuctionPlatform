using AuctionPlatform.Domain._DTO.User;
using AuctionPlatforn.Infrastructure.Repositories.Users;
using AutoMapper;

namespace AuctionPlatform.Business.Users
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserDto> GetUserById(int userId)
        {
            var auction = await _userRepository.GetUserById(userId);

            if (auction == null)
            {
                return null;
            }

            return _mapper.Map<UserDto>(auction);
        }
    }
}
