using HSMBusiness.Features.Employees.Commands.CreateEmployee;
using HSMBusiness.Features.Employees.Commands.DeleteEmployee;
using HSMBusiness.Features.Employees.Commands.UpdateEmployee;
using HSMBusiness.Features.Employees.Queries.GetEmployeeById;
using HSMBusiness.Features.Employees.Queries.GetEmployees;
using Microsoft.AspNetCore.Mvc;

namespace HSMAPI.Controllers
{
    [Route("api/[controller]")]
    public class EmployeeController : ApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetAll([FromServices] GetEmployeesQueryHandler handler)
        {
            var result = await handler.ExecuteAsync(new GetEmployeesQuery());
            return result.Match(items => Ok(items), Problem);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id, [FromServices] GetEmployeeByIdQueryHandler handler)
        {
            var result = await handler.ExecuteAsync(new GetEmployeeByIdQuery(id));
            return result.Match(item => Ok(item), Problem);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateEmployeeCommand command, [FromServices] CreateEmployeeCommandHandler handler)
        {
            var result = await handler.ExecuteAsync(command);
            return result.Match(item => CreatedAtAction(nameof(GetById), new { id = item.Id }, item), Problem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, UpdateEmployeeCommand command, [FromServices] UpdateEmployeeCommandHandler handler)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            var result = await handler.ExecuteAsync(command);
            return result.Match(item => Ok(item), Problem);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id, [FromServices] DeleteEmployeeCommandHandler handler)
        {
            var result = await handler.ExecuteAsync(new DeleteEmployeeCommand(id));
            return result.Match(_ => NoContent(), Problem);
        }
    }
}
