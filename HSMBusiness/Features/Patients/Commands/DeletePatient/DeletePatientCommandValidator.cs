using FluentValidation;

namespace HSMBusiness.Features.Patients.Commands.DeletePatient
{
    public class DeletePatientCommandValidator : AbstractValidator<DeletePatientCommand>
    {
        public DeletePatientCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Patient Id is required.");
        }
    }
}
