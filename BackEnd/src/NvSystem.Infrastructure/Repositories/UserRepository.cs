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

    public async Task<Guid> CreateUser(User user)
    {
        await _db.Users.AddAsync(user);
        await _db.SaveChangesAsync();

        return user.Id;
    }

    public async Task<User> GetByEmail(string email)
    {
        var user = await _db.Users.FirstOrDefaultAsync(x=>x.Email == email);
        return user;
    }
}