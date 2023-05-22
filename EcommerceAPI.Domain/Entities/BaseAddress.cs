using EcommerceAPI.Domain.Exceptions;

namespace EcommerceAPI.Domain.Entities
{
    public abstract class BaseAddress : BaseEntity
    {
        public string FullName { get; protected set; }
        public string Address { get; protected set; }
        public string City { get; protected set; }
        public string State { get; protected set; }
        public string PostalCode { get; protected set; }
        public string Country { get; protected set; }

        public BaseAddress(string fullName, string address, string city, string state, string postalCode, string country)
        {
            ValidateAddress(fullName, address, city, state, postalCode, country);

            FullName = fullName;
            Address = address;
            City = city;
            State = state;
            PostalCode = postalCode;
            Country = country;
        }

        private static void ValidateAddress(string fullName, string address, string city, string state, string postalCode, string country)
        {
            if (string.IsNullOrEmpty(fullName))
                throw new DomainValidationException("Invalid Full Name.");

            if (string.IsNullOrEmpty(address))
                throw new DomainValidationException("Invalid address.");

            if (string.IsNullOrEmpty(city))
                throw new DomainValidationException("Invalid city.");

            if (string.IsNullOrEmpty(state))
                throw new DomainValidationException("Invalid state.");

            if (string.IsNullOrEmpty(postalCode))
                throw new DomainValidationException("Invalid postalCode.");

            if (string.IsNullOrEmpty(country))
                throw new DomainValidationException("Invalid country.");
        }
    }
}
