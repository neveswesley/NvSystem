using FluentValidation;
using NvSystem.Domain.Entities;

namespace NvSystem.Application.UserUseCase.Validator;

public class UserValidator : AbstractValidator<User>
{
    public UserValidator()
    {
        RuleFor(x=>x.Email)
            .NotEmpty().WithMessage("Email address cannot be empty.").
            EmailAddress().WithMessage("Invalid e-mail address.");
        
        RuleFor(x=>x.Name)
            .NotEmpty().WithMessage("Name cannot be empty")
            .MinimumLength(3).WithMessage("Name must be at least 3 characters long")
            .MaximumLength(100).WithMessage("Name must be no more than 100 characters");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password cannot be empty")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters long");
    }
}