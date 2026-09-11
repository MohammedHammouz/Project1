using HSMBusiness.Features.Users.Dtos;
using HSMDataAccess.Entities;

namespace HSMBusiness.Features.Users.Mappers
{
    public static class UserMapper
    {
        public static UserDto ToDto(this User entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            return new UserDto
            {
                Id = entity.ID,
                Name = entity.Name,
                Role = entity.Role,
                Status = entity.Status,
                EmployeeId = entity.EmployeeID
            };
        }

        public static List<UserDto> ToDtos(this IEnumerable<User> entities)
            => entities.Select(e => e.ToDto()).ToList();
    }
}
