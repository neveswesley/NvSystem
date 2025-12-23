using MediatR;
using NvSystem.Application.Exceptions;
using NvSystem.Application.Services.Interfaces;
using NvSystem.Domain.DTOs;
using NvSystem.Domain.Entities;
using NvSystem.Domain.Interfaces;

namespace NvSystem.Application.UseCases.Login.Commands;

public record RefreshTokenCommand (string RefreshToken) : IRequest<AuthResponse>
{
    
}

public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, AuthResponse>
{
    
    private readonly IPasswordHasher _passwordHasher;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly ITokenService _tokenService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;

    public RefreshTokenCommandHandler(IPasswordHasher passwordHasher, IRefreshTokenRepository refreshTokenRepository, ITokenService tokenService, IUnitOfWork unitOfWork, IUserRepository userRepository)
    {
        _passwordHasher = passwordHasher;
        _refreshTokenRepository = refreshTokenRepository;
        _tokenService = tokenService;
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
    }

    public async Task<AuthResponse> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var tokens = await _refreshTokenRepository.GetAllValid();

        var storedToken = tokens.FirstOrDefault(t=> _passwordHasher.Verify(request.RefreshToken, t.Token));
        
        if (storedToken == null)
            throw new UnauthorizedException("Refresh token inválido");

        if (storedToken.Revoked || storedToken.ExpiresAt < DateTime.UtcNow)
            throw new UnauthorizedException("Refresh token expirado");

        var userId = storedToken.UserId;
        var user = await _userRepository.GetById(userId);

        storedToken.Revoked = true;

        var newAccessToken = _tokenService.GenerateAccessToken(userId, user.Email);
        var newRefreshToken = _tokenService.GenerateRefreshToken();

        await _refreshTokenRepository.Create(new RefreshToken
        {
            UserId = user.Id,
            Token = _passwordHasher.Hash(newRefreshToken),
            ExpiresAt = DateTime.UtcNow.AddDays(7)
        });

        await _unitOfWork.Commit();

        return new AuthResponse
        {
            AccessToken = newAccessToken,
            RefreshToken = newRefreshToken
        };
    }
}