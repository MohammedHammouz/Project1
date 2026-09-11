using HSMBusiness.Common.Results;

namespace HSMBusiness.Errors
{
    public static class DepartmentErrors
    {
        public static Error NotFound =>
            Error.NotFound("Department_NotFound", "Department was not found.");

        public static Error NameRequired =>
            Error.Validation("Department_Name_Required", "Department name is required.");

        public static Error AlreadyExists =>
            Error.Conflict("Department_AlreadyExists", "A department with this name already exists.");

        public static Error CannotDeleteWithDoctors =>
            Error.Conflict("Department_CannotDelete_Doctors", "Department cannot be deleted due to existing doctors.");
    }
}
