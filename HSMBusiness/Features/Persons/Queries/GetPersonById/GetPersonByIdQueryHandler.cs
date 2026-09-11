using FluentValidation;
using HSMBusiness.Common;
using HSMBusiness.Errors;
using HSMBusiness.Features.Persons.Dtos;
using HSMBusiness.Features.Persons.Mappers;
using HSMBusiness.Common.Results;
using HSMDataAccess.RepositoryServices;

namespace HSMBusiness.Features.Persons.Queries.GetPersonById
{
    public class GetPersonByIdQueryHandler(
        IValidator<GetPersonByIdQuery> validator,
        PersonRepository personRepository)
    {
        public async Task<Result<PersonDto>> ExecuteAsync(GetPersonByIdQuery query)
        {
            var validationResult = await validator.ValidateAsync(query);
            if (!validationResult.IsValid)
            {
                return validationResult.ToErrors();
            }

            var person = await personRepository.GetByIDAsync(query.Id);
            if (person is null)
            {
                return PersonErrors.NotFound;
            }

            return person.ToDto();
        }
    }
}
