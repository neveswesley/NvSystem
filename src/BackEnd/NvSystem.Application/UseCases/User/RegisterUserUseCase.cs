using NvSystem.Application.Services.Interfaces;
using NvSystem.Communications;
using NvSystem.Communications.Requests;
using NvSystem.Communications.Responses;
using NvSystem.Domain.DTOs;
using NvSystem.Domain.Interfaces;
using NvSystem.Exceptions.ExceptionsBase;

namespace NvSystem.Application.UseCases.User;

public class RegisterUserUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterUserUseCase(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ResponseRegisterUserJson> Execute(RequestRegisterUserJson request)
    {
        await Validate(request);

        var user = new Domain.Entities.User()
        {
            Name = request.Name,
            Email = request.Email,
            Password = request.Password,
            Role = request.Role
        };

        await _userRepository.CreateUser(user);
        await _unitOfWork.Commit();

        var response = new ResponseRegisterUserJson()
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Role = user.Role
        };
        
        return response;
    }

    private async Task Validate(RequestRegisterUserJson request)
    {
        var validator = new RegisterUserValidator();
        var result = validator.Validate(request);

        if (result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }
        
        var emailExists = await _userRepository.ExistActiveUserWithEmail(request.Email);
        if (emailExists)
        {
            throw new EmailAlreadyExistsException(ResourceMessagesException.EMAIL_EXIST);
        }
    }
}