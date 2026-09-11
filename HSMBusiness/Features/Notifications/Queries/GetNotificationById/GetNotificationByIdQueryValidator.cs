using FluentValidation;

namespace HSMBusiness.Features.Notifications.Queries.GetNotificationById
{
    public class GetNotificationByIdQueryValidator : AbstractValidator<GetNotificationByIdQuery>
    {
        public GetNotificationByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Notification Id is required.");
        }
    }
}
