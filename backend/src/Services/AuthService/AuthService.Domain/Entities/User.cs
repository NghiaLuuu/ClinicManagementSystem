namespace AuthService.Domain.Entities;

using System;
using AuthService.Domain.ValueObjects;
using AuthService.Domain.Events;
using AuthService.Domain.Common;
using AuthService.Domain.Enums;

public class User : AuditableEntity 
{
    public Username Username { get; private set; }
    public Email Email { get; private set; }
    public PasswordHash PasswordHash { get; private set; }
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
    private User(Username username, Email email, PasswordHash passwordHash, UserRole role, Guid createdBy) : base(createdBy) 
    {
        if (username == null)
        {
            throw new ArgumentNullException(nameof(username));
        }

        if (passwordHash == null)
        {
            throw new ArgumentNullException(nameof(passwordHash));
        }

        // Gán dữ liệu sau khi đã kiểm tra an toàn
        Username = username;
        Email = email;
        PasswordHash = passwordHash;
        Role = role;
        IsActive = true;
    }

    // ========================
    // 3. FACTORY METHODS (Nơi tạo object)
    // ========================
    public static User CreatePatient(Username username, Email email, PasswordHash passwordHash, Guid createdBy)
    {
        var user = new User(username, email, passwordHash, UserRole.Patient, createdBy);
        
        user.AddDomainEvent(new UserRegisteredEvent(user.Id, user.Email, UserRole.Patient));
        
        return user;
    }

    public static User CreateDentist(Username username, Email email, PasswordHash passwordHash, Guid createdBy)
    {
        var user = new User(username, email, passwordHash, UserRole.Dentist, createdBy);
        
        user.AddDomainEvent(new UserRegisteredEvent(user.Id, user.Email, UserRole.Dentist));
        
        return user;
    }

    // ========================
    // 4. BEHAVIOR (Hành động của User)
    // ========================
    public void ChangePassword(PasswordHash newPasswordHash, Guid updatedBy)
    {
        ArgumentNullException.ThrowIfNull(newPasswordHash);
        PasswordHash = newPasswordHash;
        Touch(updatedBy);
    }

    public void ChangeRole(UserRole newRole, Guid updatedBy)
    {
        if (IsActive == false)
        {
            throw new InvalidOperationException("Cannot change role of an inactive user.");
        }

        if (Role == newRole) 
        {
            return; // Nếu role mới giống role cũ thì không cần làm gì cả
        }

        if (newRole != UserRole.Patient && newRole != UserRole.Dentist)
        {
            throw new ArgumentException("Role is invalid.");
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