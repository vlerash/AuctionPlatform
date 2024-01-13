using AuctionPlatform.Business.Auctions;
using AuctionPlatform.Domain._DTO.Auction;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace AuctionPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuctionController : ControllerBase
    {
        private readonly IAuctionService _auctionService;

        public AuctionController(IAuctionService auctionService)
        {
            _auctionService = auctionService;
        }

        [HttpGet("getCurrentAuctionsByTimeLeftAscending")]
        public async Task<IActionResult> GetCurrentAuctionsByTimeLeftAscending()
        {
            var auctions = await _auctionService.GetCurrentAuctionsByTimeLeftAscending();
            return Ok(auctions);
        }

        [HttpGet("getActiveAuctions")]
        public async Task<IActionResult> GetActiveAuctions()
        {
            var activeAuctions = await _auctionService.GetActiveAuctions();
            return Ok(activeAuctions);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateAuction([FromBody] AuctionCreateDto auctionCreateDto)
        {
            var createdAuction = await _auctionService.CreateAuction(auctionCreateDto);
            return Ok(new { Message = "Auction created successfully." });
        }

        [HttpPost("placeBid")]
        public async Task<IActionResult> PlaceBid([FromBody] BidRequestDto bidRequest)
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

        [HttpGet("getById")]
        public async Task<IActionResult> GetAuctionById(int auctionId)
        {
            var auction = await _auctionService.GetAuctionById(auctionId);

            if (auction == null)
            {
                return NotFound(new { Success = false, Message = "Auction not found." });
            }

            return Ok(auction);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteAuction(int id)
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
    }
}
