namespace AuthService.Domain.Events;

using System;
// using MediatR; // Bỏ comment dòng này nếu dùng MediatR

// Nếu dùng MediatR, hãy đổi thành: public interface IDomainEvent : INotification
public interface IDomainEvent 
{
    // Bắt buộc mọi Event đều phải có thời gian xảy ra
    DateTime OccurredOn { get; }
}