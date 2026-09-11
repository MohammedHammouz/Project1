using FluentValidation;

namespace HSMBusiness.Features.MedicalServiceBills.Queries.GetMedicalServiceBillById
{
    public class GetMedicalServiceBillByIdQueryValidator : AbstractValidator<GetMedicalServiceBillByIdQuery>
    {
        public GetMedicalServiceBillByIdQueryValidator()
        {
            RuleFor(x => x.ServiceId)
                .NotEmpty().WithMessage("Medical service is required.");

            RuleFor(x => x.BillId)
                .NotEmpty().WithMessage("Bill is required.");
        }
    }
}
