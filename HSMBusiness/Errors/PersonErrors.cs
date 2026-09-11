using HSMBusiness.Common.Results;

namespace HSMBusiness.Errors
{
    public static class PersonErrors
    {
        public static Error NotFound =>
            Error.NotFound("Person_NotFound", "Person was not found.");

        public static Error NameRequired =>
            Error.Validation("Person_Name_Required", "Name is required.");

        public static Error ContactNumberRequired =>
            Error.Validation("Person_ContactNumber_Required", "Contact number is required.");

        public static Error EmailRequired =>
            Error.Validation("Person_Email_Required", "Email is required.");

        public static Error EmailInvalid =>
            Error.Validation("Person_Email_Invalid", "Email is invalid.");

        public static Error EmailAlreadyExists =>
            Error.Conflict("Person_Email_Exists", "A person with this email already exists.");

        public static Error GenderRequired =>
            Error.Validation("Person_Gender_Required", "Gender is required.");

        public static Error AddressRequired =>
            Error.Validation("Person_Address_Required", "Address is required.");

        public static Error InvalidDateOfBirth =>
            Error.Validation("Person_DateOfBirth_Invalid", "Date of birth is invalid.");
    }
}
