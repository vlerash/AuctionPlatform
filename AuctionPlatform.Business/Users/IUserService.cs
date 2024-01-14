using AuctionPlatform.Domain._DTO.User;

namespace AuctionPlatform.Business.Users
{
    public interface IUserService
    {
        Task<UserDto> GetUserById(int userId);
    }
}
