using Microsoft.AspNetCore.Identity;

namespace AuctionPlatform.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public int? UserId { get; set; }
        public virtual User? User { get; set; }
    }
}
