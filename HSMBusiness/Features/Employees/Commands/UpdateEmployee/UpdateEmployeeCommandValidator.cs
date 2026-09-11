using FluentValidation;

namespace HSMBusiness.Features.Employees.Commands.UpdateEmployee
{
    public class UpdateEmployeeCommandValidator : AbstractValidator<UpdateEmployeeCommand>
    {
        public UpdateEmployeeCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Employee Id is required.");

            RuleFor(x => x.PersonId)
                .NotEmpty().WithMessage("Person is required.");

            RuleFor(x => x.Salary)
                .GreaterThan(0).WithMessage("Salary must be greater than zero.");
        }
    }
}
