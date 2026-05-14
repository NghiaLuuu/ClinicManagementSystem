namespace AuthService.Domain.Common;

using System;
using System.Collections.Generic;
using AuthService.Domain.Events;

public abstract class AuditableEntity
{
    public Guid Id { get; protected set; }

    // ========================
    // 🔥 AUDIT FIELDS
    // ========================
    public Guid CreatedBy { get; protected set; }
    public Guid? UpdatedBy { get; protected set; }
    public DateTime CreatedAt { get; protected set; }
    public DateTime? UpdatedAt { get; protected set; }

    // ========================
    // 🔥 DOMAIN EVENTS
    // ========================
    private readonly List<IDomainEvent> _domainEvents = new List<IDomainEvent>();
    
    // Chỉ expose IReadOnlyCollection ra ngoài để không ai được Add() bừa bãi
    public IReadOnlyCollection<IDomainEvent> DomainEvents
    {
        get
        {
            // Trả về một bản sao của danh sách sự kiện để đảm bảo tính bất biến
            return _domainEvents.AsReadOnly();
        }
    }

    // Constructor cho EF Core
    protected AuditableEntity() { }

    // Constructor dùng khi tạo mới Entity
    protected AuditableEntity(Guid createdBy)
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.UtcNow;
        CreatedBy = createdBy;
    }

    // Các hàm thao tác với Event
    public void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    // Cập nhật thời gian và người sửa
    protected void Touch(Guid updatedBy)
    {
        UpdatedAt = DateTime.UtcNow;
        UpdatedBy = updatedBy;
    }
}