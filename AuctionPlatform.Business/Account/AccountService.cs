using AuctionPlatform.Business.Account;
using AuctionPlatform.Domain._DTO.User;
using AuctionPlatform.Domain.Entities;
using AuctionPlatforn.Infrastructure.Repositories.Users;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuctionPlatform.Business.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public AccountService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IUserRepository userRepository, IMapper mapper, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userRepository = userRepository;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<IdentityResult> RegisterAsync(UserRegisterDto userRegisterDto)
        {
            var applicationUser = new ApplicationUser { UserName = userRegisterDto.Username, Email = userRegisterDto.Email };
            var result = await _userManager.CreateAsync(applicationUser, userRegisterDto.Password);

            if (result.Succeeded)
            {
                var mappedUser = _mapper.Map<User>(userRegisterDto);

                await _userRepository.CreateAsync(mappedUser);
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

            public string GenerateAuthToken(string username)
            {
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
                var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, username),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    claims: claims,
                    expires: DateTime.UtcNow.AddHours(1),
                    signingCredentials: credentials
                );

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
    }
}