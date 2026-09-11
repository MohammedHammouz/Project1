using FluentValidation;

namespace HSMBusiness.Features.Notifications.Commands.DeleteNotification
{
    public class DeleteNotificationCommandValidator : AbstractValidator<DeleteNotificationCommand>
    {
        public DeleteNotificationCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Notification Id is required.");
        }
    }
}
