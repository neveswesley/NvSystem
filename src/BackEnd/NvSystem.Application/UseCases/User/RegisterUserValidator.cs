using FluentValidation;
using NvSystem.Communications;
using NvSystem.Communications.Requests;

namespace NvSystem.Application.UseCases.User;

public class RegisterUserValidator : AbstractValidator<RequestRegisterUserJson>
{
    public RegisterUserValidator()
    {
        RuleFor(u => u.Name)
            .NotEmpty().WithMessage(ResourceMessagesException.NAME_EMPTY)
            .MinimumLength(2).WithMessage(ResourceMessagesException.NAME_MIN_LENGTH)
            .MaximumLength(100).WithMessage(ResourceMessagesException.NAME_MAX_LENGTH);
        
        RuleFor(u=>u.Email)
            .NotEmpty().WithMessage(ResourceMessagesException.EMAIL_EMPTY)
            .EmailAddress().WithMessage(ResourceMessagesException.EMAIL_ADDRESS);

        RuleFor(u => u.Password)
            .NotEmpty().WithMessage(ResourceMessagesException.PASSWORD_EMPTY)
            .MinimumLength(6).WithMessage(ResourceMessagesException.PASSWORD_MIN_LENGTH);


    }
}