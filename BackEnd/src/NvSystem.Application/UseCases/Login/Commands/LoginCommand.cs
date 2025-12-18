using MediatR;
using NvSystem.Application.Exceptions;
using NvSystem.Application.Services.Interfaces;
using NvSystem.Domain.DTOs;
using NvSystem.Domain.Entities;
using NvSystem.Domain.Interfaces;

namespace NvSystem.Application.UseCases.Login.Commands;

public record LoginCommand (string Email, string Password) : IRequest<LoginResponse>;

public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponse>
{
    
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IPasswordHasher _passwordHasher;

    public LoginCommandHandler(IUserRepository userRepository, ITokenService tokenService, IRefreshTokenRepository refreshTokenRepository, IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
        _refreshTokenRepository = refreshTokenRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmail(request.Email)
                   ?? throw new UnauthorizedException("Invalid credentials");

        if (!_passwordHasher.Verify(request.Password, user.Password))
            throw new UnauthorizedException("Invalid credentials");

        var accessToken = _tokenService.GenerateAccessToken(user.Id, user.Email);
        var refreshToken = _tokenService.GenerateRefreshToken();

        await _refreshTokenRepository.Create(new RefreshToken
        {
            Token = refreshToken,
            UserId = user.Id,
            ExpiresAt = DateTime.UtcNow.AddDays(7)
        });

        return new LoginResponse(
            accessToken,
            refreshToken,
            DateTime.UtcNow.AddMinutes(15)
        );
    }
}