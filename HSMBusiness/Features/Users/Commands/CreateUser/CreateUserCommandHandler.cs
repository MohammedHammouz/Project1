using FluentValidation;
using HSMBusiness.Common;
using HSMBusiness.Errors;
using HSMBusiness.Features.Users.Dtos;
using HSMBusiness.Features.Users.Mappers;
using HSMBusiness.Common.Results;
using HSMDataAccess.Entities;
using HSMDataAccess.RepositoryServices;

namespace HSMBusiness.Features.Users.Commands.CreateUser
{
    public class CreateUserCommandHandler(
        IValidator<CreateUserCommand> validator,
        UserRepository userRepository,
        EmployeeRepository employeeRepository)
    {
        public async Task<Result<UserDto>> ExecuteAsync(CreateUserCommand command)
        {
            var validationResult = await validator.ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                return validationResult.ToErrors();
            }

            var employee = await employeeRepository.GetByIDAsync(command.EmployeeId);
            if (employee is null)
            {
                return UserErrors.EmployeeNotFound;
            }

            var user = new User
            {
                Name = command.Name,
                Role = command.Role,
                Status = command.Status,
                PasswordHash = command.Password,
                EmployeeID = command.EmployeeId
            };

            var created = await userRepository.AddAsync(user);

            return created.ToDto();
        }
    }
}
