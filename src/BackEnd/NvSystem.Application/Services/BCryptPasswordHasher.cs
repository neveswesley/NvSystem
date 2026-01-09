using NvSystem.Application.Services.Interfaces;

namespace NvSystem.Application.Services;

public class BCryptPasswordHasher : IPasswordHasher
{
    private const int WorkFactor = 11;
    
    public string Hash(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password, WorkFactor);
    }

    public bool Verify(string password, string hash)
    {
        return BCrypt.Net.BCrypt.Verify(password, hash);
    }
}