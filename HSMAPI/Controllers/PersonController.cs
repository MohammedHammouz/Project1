using HSMBusiness.Features.Persons.Commands.CreatePerson;
using HSMBusiness.Features.Persons.Commands.DeletePerson;
using HSMBusiness.Features.Persons.Commands.UpdatePerson;
using HSMBusiness.Features.Persons.Queries.GetPersonById;
using HSMBusiness.Features.Persons.Queries.GetPersons;
using Microsoft.AspNetCore.Mvc;

namespace HSMAPI.Controllers
{
    [Route("api/[controller]")]
    public class PersonController : ApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetAll([FromServices] GetPersonsQueryHandler handler)
        {
            var result = await handler.ExecuteAsync(new GetPersonsQuery());
            return result.Match(items => Ok(items), Problem);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id, [FromServices] GetPersonByIdQueryHandler handler)
        {
            var result = await handler.ExecuteAsync(new GetPersonByIdQuery(id));
            return result.Match(item => Ok(item), Problem);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePersonCommand command, [FromServices] CreatePersonCommandHandler handler)
        {
            var result = await handler.ExecuteAsync(command);
            return result.Match(item => CreatedAtAction(nameof(GetById), new { id = item.Id }, item), Problem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, UpdatePersonCommand command, [FromServices] UpdatePersonCommandHandler handler)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            var result = await handler.ExecuteAsync(command);
            return result.Match(item => Ok(item), Problem);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id, [FromServices] DeletePersonCommandHandler handler)
        {
            var result = await handler.ExecuteAsync(new DeletePersonCommand(id));
            return result.Match(_ => NoContent(), Problem);
        }
    }
}
