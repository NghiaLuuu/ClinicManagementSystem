namespace AuthService.Domain.Events;

using System;

// Dùng abstract record để làm class cha cho mọi Event
public abstract record DomainEvent : IDomainEvent
{
    // Tự động gán thời gian lúc sự kiện được tạo ra (UTC)
    public DateTime OccurredOn { get; protected init; } = DateTime.UtcNow;
}