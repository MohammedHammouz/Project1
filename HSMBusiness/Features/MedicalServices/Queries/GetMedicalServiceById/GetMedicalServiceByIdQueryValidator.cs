using FluentValidation;

namespace HSMBusiness.Features.MedicalServices.Queries.GetMedicalServiceById
{
    public class GetMedicalServiceByIdQueryValidator : AbstractValidator<GetMedicalServiceByIdQuery>
    {
        public GetMedicalServiceByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Medical service Id is required.");
        }
    }
}
