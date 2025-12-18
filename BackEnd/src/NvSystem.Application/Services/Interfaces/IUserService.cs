using NvSystem.Domain.DTOs;
using NvSystem.Domain.Entities;

namespace NvSystem.Application.Services.Interfaces;

public interface IUserService
{
    public Task<Guid> CreateUser(CreateUserRequest user);
}