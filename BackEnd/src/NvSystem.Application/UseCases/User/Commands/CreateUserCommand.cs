using FluentValidation;
using MediatR;
using NvSystem.Application.Exceptions;
using NvSystem.Application.Services.Interfaces;
using NvSystem.Application.UserUseCase.Validator;
using NvSystem.Domain.Enums;
using NvSystem.Domain.Interfaces;

namespace NvSystem.Application.UseCases.User.Commands;

public sealed record CreateUserCommand(string Name, string Email, string Password, Role Role) : IRequest<Guid>
{
}

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
{
    private readonly IUserRepository _userRepository;
    private readonly IValidator<CreateUserCommand> _validator;
    private readonly IPasswordHasher _passwordHasher;

    public CreateUserCommandHandler(IUserRepository userRepository, IValidator<CreateUserCommand> validator, IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _validator = validator;
        _passwordHasher = passwordHasher;
    }

    public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var result = await _validator.ValidateAsync(request, cancellationToken);

        if (!result.IsValid)
            throw new ValidationException(result.Errors);
        
        var passwordHash = _passwordHasher.Hash(request.Password);
        
        var user = new Domain.Entities.User
        {
            Name = request.Name,
            Email = request.Email,
            Password = passwordHash,
            Role = request.Role,
            CreatedAt = DateTime.Now,
            IsActive = true
        };

        await _userRepository.CreateUser(user);

        return user.Id;
    }
}