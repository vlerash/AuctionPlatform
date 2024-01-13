using AuctionPlatform.Domain._DTO.User;
using Microsoft.AspNetCore.Identity;

namespace AuctionPlatform.Business.Account
{
    public interface IAccountService
    {
        string GenerateAuthToken(string username);
        Task<SignInResult> LoginAsync(UserLoginDto userLoginDto, bool rememberMe);
        Task LogoutAsync();
        Task<IdentityResult> RegisterAsync(UserRegisterDto userRegisterDto);
    }
}
