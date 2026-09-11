using FluentValidation;
using HSMBusiness.Common;
using HSMBusiness.Errors;
using HSMBusiness.Features.Persons.Dtos;
using HSMBusiness.Features.Persons.Mappers;
using HSMBusiness.Common.Results;
using HSMDataAccess.RepositoryServices;

namespace HSMBusiness.Features.Persons.Commands.UpdatePerson
{
    public class UpdatePersonCommandHandler(
        IValidator<UpdatePersonCommand> validator,
        PersonRepository personRepository)
    {
        public async Task<Result<PersonDto>> ExecuteAsync(UpdatePersonCommand command)
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

            person.Name = command.Name;
            person.ContactNumber = command.ContactNumber;
            person.Email = command.Email;
            person.Gender = command.Gender;
            person.Address = command.Address;
            person.DateOfBirth = command.DateOfBirth;

            await personRepository.UpdateAsync(person);

            return person.ToDto();
        }
    }
}
