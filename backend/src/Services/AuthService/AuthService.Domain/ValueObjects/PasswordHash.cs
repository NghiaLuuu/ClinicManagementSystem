namespace AuthService.Domain.ValueObjects;

public sealed record PasswordHash
{
    public string Value { get; }

    // Dành cho EF Core
    private PasswordHash()
    {
        Value = null!;
    }

    private PasswordHash(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException(
                "Password hash cannot be null or whitespace."
            );
        }

        if (value.Contains(' '))
        {
            throw new ArgumentException(
                "Password hash cannot contain whitespace."
            );
        }

        Value = value;
    }

    public static PasswordHash Create(string value)
    {
        return new PasswordHash(value);
    }

    public override string ToString()
    {
        return Value;
    }
}