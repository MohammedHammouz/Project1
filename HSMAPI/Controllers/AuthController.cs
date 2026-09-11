using HSMBusiness.Features.Users.Commands.CreateUser;
using HSMBusiness.Features.Users.Commands.DeleteUser;
using HSMBusiness.Features.Users.Commands.UpdateUser;
using HSMBusiness.Features.Users.Queries.GetUserById;
using HSMBusiness.Features.Users.Queries.GetUsers;
using Microsoft.AspNetCore.Mvc;

namespace HSMAPI.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : ApiController
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id, [FromServices] GetUserByIdQueryHandler handler)
        {
            var result = await handler.ExecuteAsync(new GetUserByIdQuery(id));
            return result.Match(item => Ok(item), Problem);
        }

        [HttpGet("All")]
        public async Task<IActionResult> GetAll([FromServices] GetUsersQueryHandler handler)
        {
            var result = await handler.ExecuteAsync(new GetUsersQuery());
            return result.Match(items => Ok(items), Problem);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(CreateUserCommand command, [FromServices] CreateUserCommandHandler handler)
        {
            var result = await handler.ExecuteAsync(command);
            return result.Match(item => CreatedAtAction(nameof(GetById), new { id = item.Id }, item), Problem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, UpdateUserCommand command, [FromServices] UpdateUserCommandHandler handler)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            var result = await handler.ExecuteAsync(command);
            return result.Match(item => Ok(item), Problem);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id, [FromServices] DeleteUserCommandHandler handler)
        {
            var result = await handler.ExecuteAsync(new DeleteUserCommand(id));
            return result.Match(_ => NoContent(), Problem);
        }
    }
}
