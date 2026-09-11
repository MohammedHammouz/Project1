using FluentValidation;

namespace HSMBusiness.Features.Doctors.Queries.GetDoctorById
{
    public class GetDoctorByIdQueryValidator : AbstractValidator<GetDoctorByIdQuery>
    {
        public GetDoctorByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Doctor Id is required.");
        }
    }
}
