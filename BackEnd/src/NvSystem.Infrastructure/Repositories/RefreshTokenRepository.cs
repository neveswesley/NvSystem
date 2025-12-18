using Microsoft.EntityFrameworkCore;
using NvSystem.Domain.Entities;
using NvSystem.Domain.Interfaces;
using NvSystem.Infrastructure.Database;

namespace NvSystem.Infrastructure.Repositories;

public class RefreshTokenRepository : IRefreshTokenRepository
{

    private readonly AppDbContext _db;

    public RefreshTokenRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task Create(RefreshToken refreshToken)
    {
        _db.RefreshTokens.Add(refreshToken);
        await _db.SaveChangesAsync();
    }

    public async Task<RefreshToken?> GetByToken(string token)
    {
        return await _db.RefreshTokens.AsNoTracking().FirstOrDefaultAsync(x=>x.Token == token);
    }

    public async Task Update(RefreshToken token)
    {
        _db.RefreshTokens.Update(token);
        await _db.SaveChangesAsync();
    }
}