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

        // Foreign key for the auction creator
        public int? UserId { get; set; }
        //public User Creator { get; set; }
        public User? User { get; set; }


        // Foreign key for the highest bidder
        public int? HighestBidderId { get; set; }
        public User? HighestBidder { get; set; }

        public decimal MinimumBid { get; set; }

        public AuctionStatusEnum AuctionStatus { get; set; }
    }
}
