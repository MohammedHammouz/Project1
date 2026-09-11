using FluentValidation;
using HSMBusiness.Common;
using HSMBusiness.Errors;
using HSMBusiness.Common.Results;
using HSMDataAccess.RepositoryServices;

namespace HSMBusiness.Features.Persons.Commands.DeletePerson
{
    public class DeletePersonCommandHandler(
        IValidator<DeletePersonCommand> validator,
        PersonRepository personRepository)
    {
        public async Task<Result<Deleted>> ExecuteAsync(DeletePersonCommand command)
        {
            var validationResult = await validator.ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                return validationResult.ToErrors();
            }

            var person = await personRepository.GetByIDAsync(command.Id);
            if (person is null)
            {
                return PersonErrors.NotFound;
            }

            var isDeleted = await personRepository.DeleteAsync(person);
            if (!isDeleted)
            {
                return Error.Failure("Person_DeleteFailed", "Failed to delete person.");
            }

            return Result.Deleted;
        }
    }
}
