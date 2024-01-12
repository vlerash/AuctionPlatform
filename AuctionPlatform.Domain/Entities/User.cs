namespace AuctionPlatform.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }

        public string PasswordHash { get; set; }

        public string Email { get; set; }

        public DateTime RegistrationDate { get; set; }

        public decimal WalletAmount { get; set; }

        // Navigation property for auctions created by the user
        public virtual IList<Auction> CreatedAuctions { get; set; }

        // Navigation property for auctions where the user is the highest bidder
        public virtual IList<Auction> BiddedAuctions { get; set; }
    }
}
