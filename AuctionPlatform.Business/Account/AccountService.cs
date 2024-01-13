using AuctionPlatform.Business.Account;
using AuctionPlatform.Domain._DTO.User;
using AuctionPlatform.Domain.Entities;
using AuctionPlatforn.Infrastructure.Repositories.Users;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace AuctionPlatform.Business.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;


        public AccountService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IUserRepository userRepository, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<IdentityResult> RegisterAsync(UserRegisterDto userRegisterDto)
        {
            var applicationUser = new ApplicationUser { UserName = userRegisterDto.Username, Email = userRegisterDto.Email };
            var result = await _userManager.CreateAsync(applicationUser, userRegisterDto.Password);

            if (result.Succeeded)
            {
                var mappedUser = _mapper.Map<User>(userRegisterDto);

                var createdUser = await _userRepository.CreateAsync(mappedUser);
            }

            return result;
        }

        public async Task<SignInResult> LoginAsync(UserLoginDto userLoginDto, bool rememberMe)
        {
            var result = await _signInManager.PasswordSignInAsync(userLoginDto.UserName, userLoginDto.Password, rememberMe, lockoutOnFailure: false);
            return result;
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}