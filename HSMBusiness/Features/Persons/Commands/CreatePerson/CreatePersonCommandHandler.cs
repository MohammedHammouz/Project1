using FluentValidation;
using HSMBusiness.Common;
using HSMBusiness.Features.Persons.Dtos;
using HSMBusiness.Features.Persons.Mappers;
using HSMBusiness.Common.Results;
using HSMDataAccess.Entities;
using HSMDataAccess.RepositoryServices;

namespace HSMBusiness.Features.Persons.Commands.CreatePerson
{
    public class CreatePersonCommandHandler(
        IValidator<CreatePersonCommand> validator,
        PersonRepository personRepository)
    {
        public async Task<Result<PersonDto>> ExecuteAsync(CreatePersonCommand command)
        {
            var validationResult = await validator.ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                return validationResult.ToErrors();
            }

            var person = new Person
            {
                Name = command.Name,
                ContactNumber = command.ContactNumber,
                Email = command.Email,
                Gender = command.Gender,
                Address = command.Address,
                DateOfBirth = command.DateOfBirth
            };

            var created = await personRepository.AddAsync(person);

            return created.ToDto();
        }
    }
}
