using AutoMapper;
using MediatR;
using NvSystem.Domain.DTOs;
using NvSystem.Domain.Interfaces;

namespace NvSystem.Application.UseCases.User.Query;

public sealed record GetByIdQuery(Guid Id) : IRequest<UserResponse>;


public class GetByIdQueryHandler : IRequestHandler<GetByIdQuery, UserResponse>
{
    public GetByIdQueryHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    
    public async Task<UserResponse> Handle(GetByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _userRepository.GetById(request.Id);
        
        var result = new UserResponse()
        {
            Name = entity.Name,
            Email = entity.Email,
            Role = entity.Role
        };
        return _mapper.Map<UserResponse>(await _userRepository.GetById(request.Id));
    }
}