using AuctionPlatform.Business.Users;
using Microsoft.AspNetCore.Mvc;

namespace AuctionPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpGet("getById")]
        public async Task<IActionResult> GetUserById(int userId)
        {
            try
            {
                var auction = await _userService.GetUserById(userId);

                if (auction == null)
                {
                    return NotFound(new { Success = false, Message = "User not found." });
                }

                return Ok(auction);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving User by ID");
                return StatusCode(500, new { Message = "Internal Server Error" });
            }
        }
    }
}
