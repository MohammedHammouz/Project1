using FluentValidation.Results;
using HSMBusiness.Common.Results;

namespace HSMBusiness.Common
{
    public static class ValidationResultExtensions
    {
        public static List<Error> ToErrors(this ValidationResult validationResult)
        {
            return validationResult.Errors
                .ConvertAll(error => Error.Validation(error.PropertyName, error.ErrorMessage));
        }
    }
}
