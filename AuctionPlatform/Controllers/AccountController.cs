using AuctionPlatform.Business.Account;
using AuctionPlatform.Domain._DTO.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuctionPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly ILogger<AccountController> _logger; 

        public AccountController(IAccountService accountService, ILogger<AccountController> logger)
        {
            _accountService = accountService;
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto userRegisterDto)
        {
            try
            {
                var result = await _accountService.RegisterAsync(userRegisterDto);

                if (result.Succeeded)
                {
                    return Ok(new { Message = "Registration successful" });
                }

                return BadRequest(new { Message = "Registration failed", Errors = result.Errors });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during registration"); 
                return StatusCode(500, new { Message = "Internal Server Error" });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto userLoginDto, bool rememberMe)
        {
            try
            {
                var result = await _accountService.LoginAsync(userLoginDto, rememberMe);

                if (result.Succeeded)
                {
                    var token = _accountService.GenerateAuthToken(userLoginDto.UserName);
                    return Ok(new { Message = "Login successful", Token = token });
                }

                return BadRequest(new { Message = "Login failed" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during login");
                return StatusCode(500, new { Message = "Internal Server Error" });
            }
        }

        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await _accountService.LogoutAsync();
                return Ok(new { Message = "Logout successful" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during logout"); 
                return StatusCode(500, new { Message = "Internal Server Error" });
            }
        }
    }
}
