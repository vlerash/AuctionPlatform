namespace AuctionPlatform.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public DateTime RegistrationDate { get; set; }
        public decimal WalletAmount { get; set; }

        public virtual IList<Auction> CreatedAuctions { get; set; }
        public virtual IList<Auction> BiddedAuctions { get; set; }
        public virtual IList<Bid> PlacedBids { get; set; }
    }
}
