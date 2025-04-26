using System.Text.RegularExpressions;

namespace CoreService.Domain.ValueObjects;
public partial class PhoneNumber : ValueObject
{
    public string Value { get; private set; } = default!;

    public PhoneNumber() { }
    public PhoneNumber(string number)
    {
        if (string.IsNullOrWhiteSpace(number))
            throw new Exception("Phone number cannot be empty.");
        if (!MyRegex().IsMatch(number))
            throw new ArgumentException("Phone number must be exactly 10 digits.", nameof(number));
        Value = number;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    [GeneratedRegex(@"^\d{10}$", RegexOptions.Compiled)]
    private static partial Regex MyRegex();
}
