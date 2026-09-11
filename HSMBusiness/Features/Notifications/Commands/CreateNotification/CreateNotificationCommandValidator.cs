using FluentValidation;

namespace HSMBusiness.Features.Notifications.Commands.CreateNotification
{
    public class CreateNotificationCommandValidator : AbstractValidator<CreateNotificationCommand>
    {
        public CreateNotificationCommandValidator()
        {
            RuleFor(x => x.PatientId)
                .NotEmpty().WithMessage("Patient is required.");

            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("User is required.");

            RuleFor(x => x.Type)
                .NotEmpty().WithMessage("Notification type is required.");

            RuleFor(x => x.Message)
                .NotEmpty().WithMessage("Notification message is required.");

            RuleFor(x => x.Status)
                .NotEmpty().WithMessage("Notification status is required.");
        }
    }
}
