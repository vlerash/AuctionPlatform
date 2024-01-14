using AuctionPlatform.Domain._DTO.User;
using AuctionPlatform.Domain.Enums;

namespace AuctionPlatform.Domain._DTO.Auction
{
    public class AuctionCreateDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal CurrentBid { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int? UserId { get; set; }
        public int? HighestBidderId { get; set; }
        public decimal MinimumBid { get; set; }
        public AuctionStatusEnum AuctionStatus { get; set; }
    }
}
