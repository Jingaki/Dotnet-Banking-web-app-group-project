using System.ComponentModel.DataAnnotations.Schema;

namespace DepositBankingWebApp.Models
{
    public class Deposit
    {
        public int Id { get; set; }
        public bool IsStandardTermDeposit { get; set; }
        public bool IsInterestFixed { get; set; }
        public bool TimeDeposit { get; set; }
        public bool OverdraftPossability { get; set; }
        public bool CreditPossability { get; set; }
        public bool MonthlyCompounding { get; set; }
        public bool TerminalCapitalization { get; set; }
        public bool ValidForClientsOnly { get; set; }

        public CurrencyType CurrencyType { get; set; }
        public InterestPaymentType InterestPaymentType { get; set; }
        public OwnershipType OwnershipType { get; set; }

        public float EffectiveAnnualInterestRate { get; set; }

        public string? WebLinkToOffer { get; set; }

        public string? DescriptionOfNegotiatedInterestRate { get; set; }

        public float MinSum { get; set; }
        public string? MinSumDescription { get; set; }
        public string? MinDuration { get; set; }

        public float MaxSum { get; set; }
        public string? MaxSumDescription { get; set; }
        public string? MaxDuration { get; set; }

        public string? DurationDescription { get; set; }

        // Foreign key
        public string ApplicationUserId { get; set; }

        // Navigation properties
        public ICollection<Comment>? Comments { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}

public enum CurrencyType
{
    BGN = 1,
    EUR = 2,
    USD = 3,
    GBP = 4,
    CHF = 5
}
public enum OwnershipType
{
    Individual = 1,
    Retiree = 2,
    Child = 3
}
public enum InterestPaymentType
{
    Monthly = 1,
    Quarterly = 2,
    Semiannually = 3,
    Annually
}