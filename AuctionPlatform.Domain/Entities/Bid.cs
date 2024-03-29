﻿namespace AuctionPlatform.Domain.Entities
{
    public class Bid
    {
        public int Id { get; set; }
        public int AuctionId { get; set; }
        public int BidderUserId { get; set; }
        public decimal Amount { get; set; }
        public DateTime BidTime { get; set; }

        public Auction Auction { get; set; }
        public User BidderUser { get; set; }
    }
}
