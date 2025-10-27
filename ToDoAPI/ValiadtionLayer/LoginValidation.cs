using Dtos;
using FluentValidation;

namespace ValiadtionLayer
{
    public class LoginValidation: AbstractValidator<LoginDto>
    {
        public LoginValidation() 
        {
            RuleFor(x => x.userName)
           .NotEmpty().WithMessage("User name is required")
           .MinimumLength(3).WithMessage("User name must be at least 3 characters");

            RuleFor(x => x.password)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters");
        }

    }
}
