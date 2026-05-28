namespace AuthService.Infrastructure.Repositories;

using AuthService.Domain.Entities;
using AuthService.Domain.Repositories;
using AuthService.Domain.ValueObjects;
using AuthService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

public sealed class UserRepository : IUserRepository
{
    private readonly AuthDbContext _db;

    public UserRepository(AuthDbContext db)
    {
        _db = db;
    }

    public Task<User?> GetByIdAsync(Guid id)
        => _db.Users.FirstOrDefaultAsync(x => x.Id == id);

    public Task<User?> GetByEmailAsync(Email email)
        => _db.Users.FirstOrDefaultAsync(x => x.Email == email);

    public async Task AddAsync(User user)
    {
        _db.Users.Add(user);
        await _db.SaveChangesAsync();
    }

    public async Task UpdateAsync(User user)
    {
        _db.Users.Update(user);
        await _db.SaveChangesAsync();
    }

    public async Task DeleteAsync(User user)
    {
        _db.Users.Remove(user);
        await _db.SaveChangesAsync();
    }
}