using FluentValidation;
using HSMBusiness.Common;
using HSMBusiness.Errors;
using HSMBusiness.Common.Results;
using HSMDataAccess.RepositoryServices;

namespace HSMBusiness.Features.Users.Commands.DeleteUser
{
    public class DeleteUserCommandHandler(
        IValidator<DeleteUserCommand> validator,
        UserRepository userRepository)
    {
        public async Task<Result<Deleted>> ExecuteAsync(DeleteUserCommand command)
        {
            var validationResult = await validator.ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                return validationResult.ToErrors();
            }

            var user = await userRepository.GetByIDAsync(command.Id);
            if (user is null)
            {
                return UserErrors.NotFound;
            }

            var isDeleted = await userRepository.DeleteAsync(user);
            if (!isDeleted)
            {
                return Error.Failure("User_DeleteFailed", "Failed to delete user.");
            }

            return Result.Deleted;
        }
    }
}
