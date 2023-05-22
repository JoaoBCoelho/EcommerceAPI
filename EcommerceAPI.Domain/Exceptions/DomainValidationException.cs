namespace EcommerceAPI.Domain.Exceptions
{
    public class DomainValidationException : Exception
    {
        public DomainValidationException(string error) : base(error) { }
    }
}
