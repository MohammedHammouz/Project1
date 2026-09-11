using HSMBusiness.Common.Results;

namespace HSMBusiness.Errors
{
    public static class MedicalServiceErrors
    {
        public static Error NotFound =>
            Error.NotFound("MedicalService_NotFound", "Medical service was not found.");

        public static Error ServiceNameRequired =>
            Error.Validation("MedicalService_Name_Required", "Service name is required.");

        public static Error CategoryIDRequired =>
            Error.Validation("MedicalService_CategoryID_Required", "Service category is required.");

        public static Error CategoryNotFound =>
            Error.NotFound("MedicalService_Category_NotFound", "The specified service category does not exist.");

        public static Error InvalidPrice =>
            Error.Validation("MedicalService_Price_Invalid", "Default price must be greater than or equal to zero.");
    }
}
