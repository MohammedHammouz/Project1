using HSMBusiness.Features.Patients.Commands.CreatePatient;
using HSMBusiness.Features.Patients.Commands.DeletePatient;
using HSMBusiness.Features.Patients.Commands.UpdatePatient;
using HSMBusiness.Features.Patients.Queries.GetPatientById;
using HSMBusiness.Features.Patients.Queries.GetPatients;
using Microsoft.AspNetCore.Mvc;

namespace HSMAPI.Controllers
{
    [Route("api/[controller]")]
    public class PatientController : ApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetAll([FromServices] GetPatientsQueryHandler handler)
        {
            var result = await handler.ExecuteAsync(new GetPatientsQuery());
            return result.Match(items => Ok(items), Problem);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id, [FromServices] GetPatientByIdQueryHandler handler)
        {
            var result = await handler.ExecuteAsync(new GetPatientByIdQuery(id));
            return result.Match(item => Ok(item), Problem);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePatientCommand command, [FromServices] CreatePatientCommandHandler handler)
        {
            var result = await handler.ExecuteAsync(command);
            return result.Match(item => CreatedAtAction(nameof(GetById), new { id = item.Id }, item), Problem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, UpdatePatientCommand command, [FromServices] UpdatePatientCommandHandler handler)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            var result = await handler.ExecuteAsync(command);
            return result.Match(item => Ok(item), Problem);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id, [FromServices] DeletePatientCommandHandler handler)
        {
            var result = await handler.ExecuteAsync(new DeletePatientCommand(id));
            return result.Match(_ => NoContent(), Problem);
        }
    }
}
