using FluentValidation;
using HSMBusiness.Common;
using HSMBusiness.Errors;
using HSMBusiness.Features.Users.Dtos;
using HSMBusiness.Features.Users.Mappers;
using HSMBusiness.Common.Results;
using HSMDataAccess.RepositoryServices;

namespace HSMBusiness.Features.Users.Queries.GetUserById
{
    public class GetUserByIdQueryHandler(
        IValidator<GetUserByIdQuery> validator,
        UserRepository userRepository)
    {
        public async Task<Result<UserDto>> ExecuteAsync(GetUserByIdQuery query)
        {
            var validationResult = await validator.ValidateAsync(query);
            if (!validationResult.IsValid)
            {
                return validationResult.ToErrors();
            }

            var user = await userRepository.GetByIDAsync(query.Id);
            if (user is null)
            {
                return UserErrors.NotFound;
            }

            return user.ToDto();
        }
    }
}
