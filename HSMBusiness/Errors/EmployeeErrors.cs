using HSMBusiness.Common.Results;

namespace HSMBusiness.Errors
{
    public static class EmployeeErrors
    {
        public static Error NotFound =>
            Error.NotFound("Employee_NotFound", "Employee was not found.");

        public static Error PersonIDRequired =>
            Error.Validation("Employee_PersonID_Required", "Person is required.");

        public static Error PersonNotFound =>
            Error.NotFound("Employee_Person_NotFound", "The specified person does not exist.");

        public static Error InvalidSalary =>
            Error.Validation("Employee_Salary_Invalid", "Salary must be greater than zero.");

        public static Error AlreadyExists =>
            Error.Conflict("Employee_AlreadyExists", "An employee record already exists for this person.");
    }
}
