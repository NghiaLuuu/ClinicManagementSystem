namespace AuthService.Domain.Events;

using System;
using AuthService.Domain.ValueObjects;
using AuthService.Domain.Entities; 

public sealed record UserRegisteredEvent(Guid UserId, Email Email, UserRole Role) : DomainEvent; // <-- Kế thừa từ class cha