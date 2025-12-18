using AutoMapper;
using FluentValidation;
using NvSystem.Application.Services.Interfaces;
using NvSystem.Domain.DTOs;
using NvSystem.Domain.Entities;
using NvSystem.Domain.Interfaces;

namespace NvSystem.Application.Services;

public class UserService : IUserService
{
    
    private readonly IUserRepository _userRepository;
    private readonly IValidator<User> _userValidator;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository, IValidator<User> userValidator, IMapper mapper)
    {
        _userRepository = userRepository;
        _userValidator = userValidator;
        _mapper = mapper;
    }

    public async Task<Guid> CreateUser(CreateUserRequest user)
    {
        var request = _mapper.Map<User>(user);
        
        var result = await _userValidator.ValidateAsync(request);
        
        if (!result.IsValid)
            throw new ValidationException(result.Errors);
        
        var entity = await _userRepository.CreateUser(request);
        return entity;
    }
}