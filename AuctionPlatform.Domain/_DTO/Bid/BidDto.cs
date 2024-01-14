namespace AuctionPlatform.Domain._DTO.Bid
{
    public class BidDto
    {
        public int Id { get; set; }
        public int AuctionId { get; set; }
        public int BidderUserId { get; set; }
        public decimal Amount { get; set; }
        public DateTime BidTime { get; set; }
    }
}
