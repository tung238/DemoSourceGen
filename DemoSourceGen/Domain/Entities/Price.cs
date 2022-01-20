namespace DemoSourceGen.Domain.Entities
{
    public class Price : BaseEntity
    {
        public string StripePriceId { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public bool IsActive { get; set; }
        public long FreeTrialDays { get; set; }
    }
}
