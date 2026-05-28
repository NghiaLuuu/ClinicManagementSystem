namespace AuthService.Domain.Repositories;

using System;
using AuthService.Domain.Entities;
using AuthService.Domain.ValueObjects;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid id);
    Task<User?> GetByEmailAsync(Email email);
    Task AddAsync(User user);
    Task UpdateAsync(User user);
    Task DeleteAsync(User user);
}