namespace AuthService.Domain.ValueObjects;

using System;
using System.Text.RegularExpressions;

// Bắt buộc phải có từ khóa 'partial' nếu sử dụng [GeneratedRegex]
public sealed partial record Email
{
    public string Value { get; }

    // Dành cho EF Core (Constructor không tham số)
    private Email()
    {
        Value = null!;
    }

    public Email(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Email is required.", nameof(value));

        // Chuẩn hóa chuỗi TRƯỚC khi validate
        value = value.Trim().ToLowerInvariant();

        if (!EmailRegex().IsMatch(value))
            throw new ArgumentException($"Invalid email format: {value}", nameof(value));

        Value = value;
    }

    // Sử dụng tính năng GeneratedRegex của .NET để compile Regex lúc build time
    // Giúp tăng tối đa hiệu năng và tránh cấp phát bộ nhớ (memory allocation) dư thừa
    [GeneratedRegex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled | RegexOptions.CultureInvariant)]
    private static partial Regex EmailRegex();

    public override string ToString()
    {
        return Value;
    }
}