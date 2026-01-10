using Microsoft.EntityFrameworkCore;
using NvSystem.Domain.Entities;
using NvSystem.Domain.Interfaces;
using NvSystem.Infrastructure.Database;

namespace NvSystem.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _db;

    public UserRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task CreateUser(User user) => await _db.Users.AddAsync(user);

    public async Task<User> GetByEmail(string email)
    {
        var user = await _db.Users.FirstOrDefaultAsync(x => x.Email == email);
        return user;
    }

    public async Task<User?> GetById(Guid id)
    {
        return await _db.Users.FirstOrDefaultAsync(x => x.Id == id);
    }

    public Task<bool> ExistActiveUserWithEmail(string email)
    {
        return _db.Users.AnyAsync(x => x.Email.Equals(email));
    }
}