namespace AuctionPlatform.Domain._DTO.Auction
{
    public class BidRequestDto
    {
        public int AuctionId { get; set; }
        public decimal BidAmount { get; set; }
        public int BidderUserId { get; set; }
    }
}
