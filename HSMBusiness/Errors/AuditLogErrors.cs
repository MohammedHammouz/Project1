using HSMBusiness.Common.Results;

namespace HSMBusiness.Errors
{
    public static class AuditLogErrors
    {
        public static Error NotFound =>
            Error.NotFound("AuditLog_NotFound", "Audit log was not found.");

        public static Error UserIDRequired =>
            Error.Validation("AuditLog_UserID_Required", "User is required.");

        public static Error UserNotFound =>
            Error.NotFound("AuditLog_User_NotFound", "The specified user does not exist.");

        public static Error EntityRequired =>
            Error.Validation("AuditLog_Entity_Required", "Entity name is required.");

        public static Error ActionRequired =>
            Error.Validation("AuditLog_Action_Required", "Action is required.");
    }
}
