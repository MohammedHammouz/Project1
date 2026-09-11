using HSMBusiness.Features.Persons.Dtos;
using HSMBusiness.Features.Persons.Mappers;
using HSMBusiness.Common.Results;
using HSMDataAccess.RepositoryServices;

namespace HSMBusiness.Features.Persons.Queries.GetPersons
{
    public class GetPersonsQueryHandler(PersonRepository personRepository)
    {
        public async Task<Result<List<PersonDto>>> ExecuteAsync(GetPersonsQuery query)
        {
            var persons = await personRepository.GetAllAsync();

            return persons.ToDtos();
        }
    }
}
