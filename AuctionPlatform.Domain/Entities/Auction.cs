using AuctionPlatform.Domain.Enums;

namespace AuctionPlatform.Domain.Entities
{
    public class Auction
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal CurrentBid { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public decimal MinimumBid { get; set; }
        public decimal TotalBidAmount { get; set; }
        public int? UserId { get; set; }
        public int? HighestBidderId { get; set; }
        public AuctionStatusEnum AuctionStatus { get; set; }

        public User? User { get; set; }
        public User? HighestBidder { get; set; }
        public virtual IList<Bid> Bids { get; set; }
    }
}
