namespace AuthService.Domain.Entities;

using System;
using AuthService.Domain.ValueObjects;
using AuthService.Domain.Events;
using AuthService.Domain.Common;

public class User : AuditableEntity 
{
    public string Username { get; private set; }
    public Email Email { get; private set; }
    public string PasswordHash { get; private set; }
    public UserRole Role { get; private set; }
    public bool IsActive { get; private set; }

    // ========================
    // 1. EF Core constructor
    // ========================
    private User()
    {
        Username = null!;
        Email = null!;
        PasswordHash = null!;
    }

    // ========================
    // 2. Private constructor
    // ========================
    // Gọi : base(createdBy) để class Cha (AuditableEntity) khởi tạo Id và thời gian trước
    private User(string username, Email email, string passwordHash, UserRole role, Guid createdBy) : base(createdBy) 
    {
        if (string.IsNullOrWhiteSpace(username))
        {
            throw new ArgumentException("Username không được để trống.", nameof(username));
        }

        if (string.IsNullOrWhiteSpace(passwordHash))
        {
            throw new ArgumentException("Password không được để trống.", nameof(passwordHash));
        }

        // Gán dữ liệu sau khi đã kiểm tra an toàn
        Username = username.Trim().ToLowerInvariant();
        Email = email;
        PasswordHash = passwordHash;
        Role = role;
        IsActive = true;
    }

    // ========================
    // 3. FACTORY METHODS (Nơi tạo object)
    // ========================
    public static User CreatePatient(string username, string email, string passwordHash, Guid createdBy)
    {
        var user = new User(username, new Email(email), passwordHash, UserRole.Patient, createdBy);
        
        user.AddDomainEvent(new UserRegisteredEvent(user.Id, user.Email, user.Role));
        
        return user;
    }

    public static User CreateDentist(string username, string email, string passwordHash, Guid createdBy)
    {
        var user = new User(username, new Email(email), passwordHash, UserRole.Dentist, createdBy);
        
        user.AddDomainEvent(new UserRegisteredEvent(user.Id, user.Email, user.Role));
        
        return user;
    }

    // ========================
    // 4. BEHAVIOR (Hành động của User)
    // ========================
    public void ChangePassword(string newPasswordHash, Guid updatedBy)
    {
        if (string.IsNullOrWhiteSpace(newPasswordHash))
        {
            throw new ArgumentException("Password mới không được để trống.");
        }

        PasswordHash = newPasswordHash;
        Touch(updatedBy); // Hàm Touch có sẵn từ class Cha (AuditableEntity)
    }

    public void ChangeRole(UserRole newRole, Guid updatedBy)
    {
        if (IsActive == false)
        {
            throw new InvalidOperationException("Không thể đổi Role cho User đang bị khóa.");
        }

        if (Role == newRole) 
        {
            return; // Nếu role mới giống role cũ thì không cần làm gì cả
        }

        if (newRole != UserRole.Patient && newRole != UserRole.Dentist)
        {
            throw new ArgumentException("Role không hợp lệ.");
        }

        Role = newRole;
        Touch(updatedBy);
    }

    public void Deactivate(Guid updatedBy)
    {
        if (IsActive == false) 
        {
            return;
        }

        IsActive = false;
        Touch(updatedBy);
    }

    public void Activate(Guid updatedBy)
    {
        if (IsActive == true) 
        {
            return;
        }

        IsActive = true;
        Touch(updatedBy);
    }
}