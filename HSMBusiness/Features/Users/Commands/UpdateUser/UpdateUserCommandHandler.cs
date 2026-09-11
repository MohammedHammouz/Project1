using FluentValidation;
using HSMBusiness.Common;
using HSMBusiness.Errors;
using HSMBusiness.Features.Users.Dtos;
using HSMBusiness.Features.Users.Mappers;
using HSMBusiness.Common.Results;
using HSMDataAccess.RepositoryServices;

namespace HSMBusiness.Features.Users.Commands.UpdateUser
{
    public class UpdateUserCommandHandler(
        IValidator<UpdateUserCommand> validator,
        UserRepository userRepository,
        EmployeeRepository employeeRepository)
    {
        public async Task<Result<UserDto>> ExecuteAsync(UpdateUserCommand command)
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

            var employee = await employeeRepository.GetByIDAsync(command.EmployeeId);
            if (employee is null)
            {
                return UserErrors.EmployeeNotFound;
            }

            user.Name = command.Name;
            user.Role = command.Role;
            user.Status = command.Status;
            user.PasswordHash = command.Password;
            user.EmployeeID = command.EmployeeId;

            await userRepository.UpdateAsync(user);

            return user.ToDto();
        }
    }
}
