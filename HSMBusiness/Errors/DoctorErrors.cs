using HSMBusiness.Common.Results;

namespace HSMBusiness.Errors
{
    public static class DoctorErrors
    {
        public static Error NotFound =>
            Error.NotFound("Doctor_NotFound", "Doctor was not found.");

        public static Error DepartmentIDRequired =>
            Error.Validation("Doctor_DepartmentID_Required", "Department is required.");

        public static Error DepartmentNotFound =>
            Error.NotFound("Doctor_Department_NotFound", "The specified department does not exist.");

        public static Error SpecializationRequired =>
            Error.Validation("Doctor_Specialization_Required", "Specialization is required.");

        public static Error UserNotFound =>
            Error.NotFound("Doctor_User_NotFound", "The specified user does not exist.");

        public static Error CannotDeleteWithAppointments =>
            Error.Conflict("Doctor_CannotDelete_Appointments", "Doctor cannot be deleted due to existing appointments.");
    }
}
