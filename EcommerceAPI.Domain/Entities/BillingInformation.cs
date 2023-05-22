namespace EcommerceAPI.Domain.Entities
{
    public sealed class BillingInformation : BaseAddress
    {
        public BillingInformation(string fullName, string address, string city, string state, string postalCode, string country)
            : base(fullName, address, city, state, postalCode, country) { }
    }
}
