namespace DemoSourceGen.Domain.Entities
{
    public class Plan : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageURL { get; set; }
        //inactive mean not available for new supscription
        public bool IsActive { get; set; }
        public string StripeProductId { get; set; }
        public string Features { get; set; }
        public virtual List<Price> Prices { get; set; }
    }
}
