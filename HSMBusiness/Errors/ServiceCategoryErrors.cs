using HSMBusiness.Common.Results;

namespace HSMBusiness.Errors
{
    public static class ServiceCategoryErrors
    {
        public static Error NotFound =>
            Error.NotFound("ServiceCategory_NotFound", "Service category was not found.");

        public static Error CategoryNameRequired =>
            Error.Validation("ServiceCategory_Name_Required", "Category name is required.");

        public static Error AlreadyExists =>
            Error.Conflict("ServiceCategory_AlreadyExists", "A service category with this name already exists.");

        public static Error CannotDeleteWithServices =>
            Error.Conflict("ServiceCategory_CannotDelete_Services", "Category cannot be deleted due to existing medical services.");
    }
}
