using FluentValidation;

namespace HSMBusiness.Features.Doctors.Commands.UpdateDoctor
{
    public class UpdateDoctorCommandValidator : AbstractValidator<UpdateDoctorCommand>
    {
        public UpdateDoctorCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Doctor Id is required.");

            RuleFor(x => x.DepartmentId)
                .NotEmpty().WithMessage("Department is required.");

            RuleFor(x => x.Specialization)
                .NotEmpty().WithMessage("Specialization is required.")
                .MaximumLength(50).WithMessage("Specialization must not exceed 50 characters.");
        }
    }
}
