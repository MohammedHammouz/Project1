using FluentValidation;

namespace HSMBusiness.Features.Patients.Queries.GetPatientById
{
    public class GetPatientByIdQueryValidator : AbstractValidator<GetPatientByIdQuery>
    {
        public GetPatientByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Patient Id is required.");
        }
    }
}
