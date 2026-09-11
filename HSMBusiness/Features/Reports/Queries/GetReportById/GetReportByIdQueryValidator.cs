using FluentValidation;

namespace HSMBusiness.Features.Reports.Queries.GetReportById
{
    public class GetReportByIdQueryValidator : AbstractValidator<GetReportByIdQuery>
    {
        public GetReportByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Report Id is required.");
        }
    }
}
