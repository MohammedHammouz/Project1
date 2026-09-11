using HSMBusiness.Common.Results;

namespace HSMBusiness.Errors
{
    public static class PatientErrors
    {
        public static Error NotFound =>
            Error.NotFound("Patient_NotFound", "Patient was not found.");

        public static Error PersonIDRequired =>
            Error.Validation("Patient_PersonID_Required", "Person is required.");

        public static Error PersonNotFound =>
            Error.NotFound("Patient_Person_NotFound", "The specified person does not exist.");

        public static Error MedicalHistoryRequired =>
            Error.Validation("Patient_MedicalHistory_Required", "Medical history is required.");

        public static Error AlreadyExists =>
            Error.Conflict("Patient_AlreadyExists", "A patient record already exists for this person.");

        public static Error CannotDeleteWithAppointments =>
            Error.Conflict("Patient_CannotDelete_Appointments", "Patient cannot be deleted due to existing appointments.");

        public static Error CannotDeleteWithBills =>
            Error.Conflict("Patient_CannotDelete_Bills", "Patient cannot be deleted due to existing bills.");
    }
}
