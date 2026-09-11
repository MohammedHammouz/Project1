using HSMBusiness.Features.Persons.Dtos;
using HSMDataAccess.Entities;

namespace HSMBusiness.Features.Persons.Mappers
{
    public static class PersonMapper
    {
        public static PersonDto ToDto(this Person entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            return new PersonDto
            {
                Id = entity.ID,
                Name = entity.Name,
                ContactNumber = entity.ContactNumber,
                Email = entity.Email,
                Gender = entity.Gender,
                Address = entity.Address,
                DateOfBirth = entity.DateOfBirth
            };
        }

        public static List<PersonDto> ToDtos(this IEnumerable<Person> entities)
            => entities.Select(e => e.ToDto()).ToList();
    }
}
