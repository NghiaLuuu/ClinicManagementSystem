namespace AuthService.Domain.ValueObjects;

using System;
using System.Text.RegularExpressions;

public sealed partial record Username
{
    public string Value { get; }

    // Dành cho EF Core
    private Username()
    {
        Value = null!;
    }

    public Username(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Username cannot be null or whitespace.", nameof(value));
        }

        // Chuẩn hóa: Cắt khoảng trắng 2 đầu và đưa các khoảng trắng ở giữa về 1 dấu cách duy nhất
        value = Regex.Replace(value.Trim(), @"\s+", " ");

        // Kiểm tra Regex: Không số, không ký tự đặc biệt, ít nhất 2 từ
        if (!UsernameRegex().IsMatch(value))
        {
            throw new ArgumentException("Username is invalid. It must contain only letters and have at least 2 words.", nameof(value));
        }

        Value = value;
    }

    // Giải thích Regex:
    // ^         : Bắt đầu chuỗi
    // [\p{L}]+  : Ít nhất 1 chữ cái (\p{L} hỗ trợ mọi ngôn ngữ, bao gồm Tiếng Việt có dấu)
    // (?:\s+[\p{L}]+)+ : Có ít nhất 1 cụm (dấu cách + chữ cái) lặp lại => Đảm bảo có từ thứ 2 trở lên
    // $         : Kết thúc chuỗi
    [GeneratedRegex(@"^[\p{L}]+(?:\s+[\p{L}]+)+$", RegexOptions.Compiled)]
    private static partial Regex UsernameRegex();

    public override string ToString() 
    {
        return Value;
    }   
}