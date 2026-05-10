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
                "Password hash không được rỗng."
            );
        }

        if (value.Contains(" "))
        {
            throw new ArgumentException(
                "Password hash không được chứa khoảng trắng."
            );
        }

        return new PasswordHash(value);
    }
}