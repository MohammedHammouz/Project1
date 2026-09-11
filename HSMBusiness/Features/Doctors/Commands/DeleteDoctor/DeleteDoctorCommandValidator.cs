using FluentValidation;

namespace HSMBusiness.Features.Doctors.Commands.DeleteDoctor
{
    public class DeleteDoctorCommandValidator : AbstractValidator<DeleteDoctorCommand>
    {
        public DeleteDoctorCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Doctor Id is required.");
        }
    }
}
