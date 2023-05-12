using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace DepositBankingWebApp.Models
{
    public class Comment
    {
        public int Id { get; set; }        
        public string Description { get; set; }

        // Foreign key
        public int DepositId { get; set; }
        public string ApplicationUserId { get; set; }

        // Navigation properties
        public Deposit Deposit { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
