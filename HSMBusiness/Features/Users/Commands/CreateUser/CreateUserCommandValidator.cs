using FluentValidation;

namespace HSMBusiness.Features.Users.Commands.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.");

            RuleFor(x => x.Role)
                .NotEmpty().WithMessage("Role is required.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.");

            RuleFor(x => x.EmployeeId)
                .NotEmpty().WithMessage("Employee is required.");
        }
    }
}
