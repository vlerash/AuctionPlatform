using AuctionPlatform.Business.Auctions;
using AuctionPlatform.Domain._DTO.Auction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuctionPlatform.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuctionController : ControllerBase
    {
        private readonly IAuctionService _auctionService;
        private readonly ILogger<AuctionController> _logger; 

        public AuctionController(IAuctionService auctionService, ILogger<AuctionController> logger)
        {
            _auctionService = auctionService;
            _logger = logger;
        }

        [Authorize]
        [HttpGet("getCurrentAuctions")]
        public async Task<IActionResult> GetCurrentAuctionsByTimeLeftAscending()
        {
            try
            {
                var auctions = await _auctionService.GetCurrentAuctionsByTimeLeftAscending();
                return Ok(auctions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving current auctions by time left"); 
                return StatusCode(500, new { Message = "Internal Server Error" });
            }
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateAuction([FromBody] AuctionCreateDto auctionCreateDto)
        {
            try
            {
                var createdAuction = await _auctionService.CreateAuction(auctionCreateDto);
                return Ok(new { Message = "Auction created successfully." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating auction"); 
                return StatusCode(500, new { Message = "Internal Server Error" });
            }
        }

        [HttpPost("placeBid")]
        public async Task<IActionResult> PlaceBid([FromBody] BidRequestDto bidRequest)
        {
            try
            {
                var bidResponse = await _auctionService.PlaceBid(bidRequest);

                if (bidResponse.Success)
                {
                    return Ok(new { Success = true, Message = "Bid successful." });
                }
                else
                {
                    return BadRequest(new { Success = false, Message = bidResponse.Message });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error placing bid");
                return StatusCode(500, new { Message = "Internal Server Error" });
            }
        }

        [HttpGet("getById")]
        public async Task<IActionResult> GetAuctionById(int auctionId)
        {
            try
            {
                var auction = await _auctionService.GetAuctionById(auctionId);

                if (auction == null)
                {
                    return NotFound(new { Success = false, Message = "Auction not found." });
                }

                return Ok(auction);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving auction by ID");
                return StatusCode(500, new { Message = "Internal Server Error" });
            }
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteAuction(int id)
        {
            try
            {
                var result = await _auctionService.Delete(id);

                if (result)
                {
                    return Ok(new { Success = true, Message = "Auction deleted successfully." });
                }
                else
                {
                    return NotFound(new { Success = false, Message = "Auction not found or deletion failed." });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting auction"); 
                return StatusCode(500, new { Message = "Internal Server Error" });
            }
        }
    }
}
