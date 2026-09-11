using FluentValidation;

namespace HSMBusiness.Features.Employees.Commands.DeleteEmployee
{
    public class DeleteEmployeeCommandValidator : AbstractValidator<DeleteEmployeeCommand>
    {
        public DeleteEmployeeCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Employee Id is required.");
        }
    }
}
