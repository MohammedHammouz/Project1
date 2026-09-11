using HSMBusiness.Common.Results;

namespace HSMBusiness.Errors
{
    public static class MedicalRecordErrors
    {
        public static Error NotFound =>
            Error.NotFound("MedicalRecord_NotFound", "Medical record was not found.");

        public static Error PatientIDRequired =>
            Error.Validation("MedicalRecord_PatientID_Required", "Patient is required.");

        public static Error PatientNotFound =>
            Error.NotFound("MedicalRecord_Patient_NotFound", "The specified patient does not exist.");

        public static Error AccessLevelRequired =>
            Error.Validation("MedicalRecord_AccessLevel_Required", "Access level is required.");

        public static Error AccessDenied =>
            Error.Unauthorized("MedicalRecord_AccessDenied", "You do not have permission to access this medical record.");
    }
}
