using NvSystem.Domain.Entities;

namespace NvSystem.Domain.Interfaces;

public interface IRefreshTokenRepository
{
    Task Create(RefreshToken refreshToken);
    Task<RefreshToken?> GetByToken(string token);
    Task Update(RefreshToken token);
    Task <IEnumerable<RefreshToken>>GetAllValid();
}