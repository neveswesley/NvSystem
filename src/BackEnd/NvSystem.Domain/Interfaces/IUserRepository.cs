using NvSystem.Domain.Entities;

namespace NvSystem.Domain.Interfaces;

public interface IUserRepository 
{
    Task CreateUser(User user);
    Task<User> GetByEmail(string email);
    Task<User?> GetById(Guid id);
    Task<bool> ExistActiveUserWithEmail(string email);
}