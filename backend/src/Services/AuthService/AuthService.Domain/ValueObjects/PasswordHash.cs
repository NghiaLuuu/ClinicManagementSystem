namespace AuthService.Domain.ValueObjects;
using System;

public sealed record PasswordHash
{
    public string Value { get; }

    private PasswordHash() { Value = null!; }

    public PasswordHash(string hashValue)
    {
        // Chuỗi Hash do hệ thống sinh ra thì chỉ cần không rỗng là được
        if (string.IsNullOrWhiteSpace(hashValue))
        {
            throw new ArgumentException("Mã băm mật khẩu không được để trống.", nameof(hashValue));
        }

        Value = hashValue;
    }

    public override string ToString()
    { 
        return Value; 
    }
}