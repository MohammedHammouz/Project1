using HSMBusiness.Common.Results;

namespace HSMBusiness.Errors
{
    public static class UserErrors
    {
        public static Error NotFound =>
            Error.NotFound("User_NotFound", "User was not found.");

        public static Error NameRequired =>
            Error.Validation("User_Name_Required", "Name is required.");

        public static Error RoleRequired =>
            Error.Validation("User_Role_Required", "Role is required.");

        public static Error PasswordRequired =>
            Error.Validation("User_Password_Required", "Password is required.");

        public static Error EmployeeIDRequired =>
            Error.Validation("User_EmployeeID_Required", "Employee is required.");

        public static Error EmployeeNotFound =>
            Error.NotFound("User_Employee_NotFound", "The specified employee does not exist.");

        public static Error AlreadyExists =>
            Error.Conflict("User_AlreadyExists", "A user account already exists for this employee.");

        public static Error InvalidCredentials =>
            Error.Unauthorized("User_InvalidCredentials", "Invalid username or password.");
    }
}
