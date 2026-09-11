using HSMBusiness.Common.Results;

namespace HSMBusiness.Errors
{
    public static class AppointmentErrors
    {
        public static Error NotFound =>
            Error.NotFound("Appointment_NotFound", "Appointment was not found.");

        public static Error PatientIDRequired =>
            Error.Validation("Appointment_PatientID_Required", "Patient is required.");

        public static Error DoctorIDRequired =>
            Error.Validation("Appointment_DoctorID_Required", "Doctor is required.");

        public static Error DateRequired =>
            Error.Validation("Appointment_Date_Required", "Appointment date is required.");

        public static Error DateInPast =>
            Error.Validation("Appointment_Date_InPast", "Appointment date cannot be in the past.");

        public static Error TimeRequired =>
            Error.Validation("Appointment_Time_Required", "Appointment time is required.");

        public static Error InvalidDuration =>
            Error.Validation("Appointment_Duration_Invalid", "Duration must be greater than zero.");

        public static Error StatusRequired =>
            Error.Validation("Appointment_Status_Required", "Appointment status is required.");

        public static Error PatientNotFound =>
            Error.NotFound("Appointment_Patient_NotFound", "The specified patient does not exist.");

        public static Error DoctorNotFound =>
            Error.NotFound("Appointment_Doctor_NotFound", "The specified doctor does not exist.");

        public static Error DoctorNotAvailable =>
            Error.Conflict("Appointment_Doctor_NotAvailable", "The doctor already has an appointment at this date and time.");
    }
}
