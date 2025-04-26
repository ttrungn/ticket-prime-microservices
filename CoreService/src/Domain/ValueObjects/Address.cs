namespace CoreService.Domain.ValueObjects
{
    public partial class Address : ValueObject
    {
        public string Street { get; private set; } = default!;
        public string City { get; private set; } = default!;
        public string Country { get; private set; } = default!;
        public string ZipCode { get; private set; } = default!;
        public Address() { }

        public Address(string street, string city, string country, string zipCode)
        {
            Street = NotEmpty(street, nameof(street));
            City = NotEmpty(city, nameof(city));
            Country = NotEmpty(country, nameof(country));
            ZipCode = NotEmpty(zipCode, nameof(zipCode));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Street;
            yield return City;
            yield return Country;
            yield return ZipCode;
        }
    }
}
