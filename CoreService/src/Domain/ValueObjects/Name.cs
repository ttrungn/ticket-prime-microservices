namespace CoreService.Domain.ValueObjects
{
    public partial class Name : ValueObject
    {
        public string First { get; private set; } = default!;
        public string Last { get; private set; } = default!;
        public string Full => $"{First} {Last}";
        public Name() { }

        public Name(string first, string last)
        {

            First = NotEmpty(first, nameof(first));
            Last = NotEmpty(last, nameof(last));
        }


        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return First;
            yield return Last;
        }
    }
}
