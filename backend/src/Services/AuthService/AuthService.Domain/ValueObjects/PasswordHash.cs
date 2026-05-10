public sealed partial record PasswordHash
{
    public string Value { get; }

    private PasswordHash(string value)
    {
        Value = value;
    }

    public static PasswordHash Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException(
                "Password hash cannot be null or whitespace."
            );
        }

        if (value.Contains(" "))
        {
            throw new ArgumentException(
                "Password hash cannot contain whitespace."
            );
        }

        return new PasswordHash(value);
    }
}