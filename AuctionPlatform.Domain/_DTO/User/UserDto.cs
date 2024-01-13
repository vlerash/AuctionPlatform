using AuctionPlatform.Domain._DTO.Auction;

namespace AuctionPlatform.Domain._DTO.User
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public DateTime RegistrationDate { get; set; }
        public decimal WalletAmount { get; set; }

        public virtual IList<AuctionDto> CreatedAuctions { get; set; }
        public virtual IList<AuctionDto> BiddedAuctions { get; set; }
    }
}
