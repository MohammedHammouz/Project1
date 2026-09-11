using HSMBusiness.Features.Users.Dtos;
using HSMBusiness.Features.Users.Mappers;
using HSMBusiness.Common.Results;
using HSMDataAccess.RepositoryServices;

namespace HSMBusiness.Features.Users.Queries.GetUsers
{
    public class GetUsersQueryHandler(UserRepository userRepository)
    {
        public async Task<Result<List<UserDto>>> ExecuteAsync(GetUsersQuery query)
        {
            var users = await userRepository.GetAllAsync();

            return users.ToDtos();
        }
    }
}
