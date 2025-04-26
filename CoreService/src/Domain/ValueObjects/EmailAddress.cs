using System.Text.RegularExpressions;

namespace CoreService.Domain.ValueObjects
{
    public partial class EmailAddress : ValueObject
    {
        public string Value { get; } = default!;
        protected EmailAddress() { }

        public EmailAddress(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new Exception("Email address cannot be empty.");

            if (!MyRegex().IsMatch(email))
                throw new Exception("Invalid email address format.");

            Value = email;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value.ToLowerInvariant();
        }

        [GeneratedRegex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase | RegexOptions.Compiled, "en-US")]
        private static partial Regex MyRegex();
    }
}
