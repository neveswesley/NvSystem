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

    public async Task<RefreshToken?> GetByToken(string hashedToken)
    {
        return await _db.RefreshTokens
            .FirstOrDefaultAsync(x=>
                x.Token == hashedToken &&
                !x.Revoked &&
                x.ExpiresAt >= DateTime.Now);
    }

    public async Task Update(RefreshToken token)
    {
        _db.RefreshTokens.Update(token);
        await _db.SaveChangesAsync();
    }

    public async Task<IEnumerable<RefreshToken>> GetAllValid()
    {
        var refreshTokens = await _db.RefreshTokens
            .AsNoTracking()
            .Where(x=> x.Revoked == false && x.ExpiresAt >= DateTime.Now)
            .ToListAsync();
        
        return refreshTokens;
    }
}