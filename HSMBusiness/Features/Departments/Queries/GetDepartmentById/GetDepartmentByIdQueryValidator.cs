using FluentValidation;

namespace HSMBusiness.Features.Departments.Queries.GetDepartmentById
{
    public class GetDepartmentByIdQueryValidator : AbstractValidator<GetDepartmentByIdQuery>
    {
        public GetDepartmentByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Department Id is required.");
        }
    }
}
