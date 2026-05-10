namespace AuthService.Domain.Events;

using System;
using AuthService.Domain.ValueObjects;
using AuthService.Domain.Entities; 
using AuthService.Domain.Enums;

public sealed record UserRegisteredEvent(Guid UserId, Email Email, UserRole Role) : DomainEvent; // <-- Kế thừa từ class cha