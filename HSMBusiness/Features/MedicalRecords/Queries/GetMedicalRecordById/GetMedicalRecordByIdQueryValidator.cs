using FluentValidation;

namespace HSMBusiness.Features.MedicalRecords.Queries.GetMedicalRecordById
{
    public class GetMedicalRecordByIdQueryValidator : AbstractValidator<GetMedicalRecordByIdQuery>
    {
        public GetMedicalRecordByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Medical record Id is required.");
        }
    }
}
