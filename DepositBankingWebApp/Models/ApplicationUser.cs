using Microsoft.AspNetCore.Identity;

namespace DepositBankingWebApp.Models
{
    public class ApplicationUser:IdentityUser
    {
        //public int Id { get; set; }dotnet identity is a string Id model
        // additional stuff for the user dunno like image?

        // Navigation properties
        public ICollection<Deposit> Deposits { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
