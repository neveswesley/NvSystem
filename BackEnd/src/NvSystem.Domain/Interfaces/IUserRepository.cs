using NvSystem.Domain.Entities;

namespace NvSystem.Domain.Interfaces;

public interface IUserRepository 
{
    Task<Guid> CreateUser(User user);
    Task<User> GetByEmail(string email);
    Task<User?> GetById(Guid id);
}