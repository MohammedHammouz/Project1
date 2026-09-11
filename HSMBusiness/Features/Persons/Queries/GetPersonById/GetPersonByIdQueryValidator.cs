using FluentValidation;

namespace HSMBusiness.Features.Persons.Queries.GetPersonById
{
    public class GetPersonByIdQueryValidator : AbstractValidator<GetPersonByIdQuery>
    {
        public GetPersonByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Person Id is required.");
        }
    }
}
