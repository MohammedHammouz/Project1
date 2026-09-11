using HSMBusiness.Common.Results;

namespace HSMBusiness.Errors
{
    public static class ReportErrors
    {
        public static Error NotFound =>
            Error.NotFound("Report_NotFound", "Report was not found.");

        public static Error TypeRequired =>
            Error.Validation("Report_Type_Required", "Report type is required.");

        public static Error GeneratedByRequired =>
            Error.Validation("Report_GeneratedBy_Required", "GeneratedBy user is required.");

        public static Error UserNotFound =>
            Error.NotFound("Report_User_NotFound", "The specified user does not exist.");
    }
}
