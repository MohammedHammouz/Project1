using HSMBusiness.Common.Results;

namespace HSMBusiness.Errors
{
    public static class NotificationErrors
    {
        public static Error NotFound =>
            Error.NotFound("Notification_NotFound", "Notification was not found.");

        public static Error PatientIDRequired =>
            Error.Validation("Notification_PatientID_Required", "Patient is required.");

        public static Error PatientNotFound =>
            Error.NotFound("Notification_Patient_NotFound", "The specified patient does not exist.");

        public static Error UserIDRequired =>
            Error.Validation("Notification_UserID_Required", "User is required.");

        public static Error UserNotFound =>
            Error.NotFound("Notification_User_NotFound", "The specified user does not exist.");

        public static Error TypeRequired =>
            Error.Validation("Notification_Type_Required", "Notification type is required.");

        public static Error MessageRequired =>
            Error.Validation("Notification_Message_Required", "Notification message is required.");
    }
}
