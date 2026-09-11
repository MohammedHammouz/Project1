using HSMBusiness.Features.Departments.Commands.CreateDepartment;
using HSMBusiness.Features.Departments.Commands.DeleteDepartment;
using HSMBusiness.Features.Departments.Commands.UpdateDepartment;
using HSMBusiness.Features.Departments.Queries.GetDepartmentById;
using HSMBusiness.Features.Departments.Queries.GetDepartments;
using Microsoft.AspNetCore.Mvc;

namespace HSMAPI.Controllers
{
    [Route("api/[controller]")]
    public class DepartmentController : ApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetAll([FromServices] GetDepartmentsQueryHandler handler)
        {
            var result = await handler.ExecuteAsync(new GetDepartmentsQuery());
            return result.Match(items => Ok(items), Problem);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id, [FromServices] GetDepartmentByIdQueryHandler handler)
        {
            var result = await handler.ExecuteAsync(new GetDepartmentByIdQuery(id));
            return result.Match(item => Ok(item), Problem);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateDepartmentCommand command, [FromServices] CreateDepartmentCommandHandler handler)
        {
            var result = await handler.ExecuteAsync(command);
            return result.Match(item => CreatedAtAction(nameof(GetById), new { id = item.Id }, item), Problem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, UpdateDepartmentCommand command, [FromServices] UpdateDepartmentCommandHandler handler)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            var result = await handler.ExecuteAsync(command);
            return result.Match(item => Ok(item), Problem);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id, [FromServices] DeleteDepartmentCommandHandler handler)
        {
            var result = await handler.ExecuteAsync(new DeleteDepartmentCommand(id));
            return result.Match(_ => NoContent(), Problem);
        }
    }
}
