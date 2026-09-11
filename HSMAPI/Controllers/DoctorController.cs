using HSMBusiness.Features.Doctors.Commands.CreateDoctor;
using HSMBusiness.Features.Doctors.Commands.DeleteDoctor;
using HSMBusiness.Features.Doctors.Commands.UpdateDoctor;
using HSMBusiness.Features.Doctors.Queries.GetDoctorById;
using HSMBusiness.Features.Doctors.Queries.GetDoctors;
using Microsoft.AspNetCore.Mvc;

namespace HSMAPI.Controllers
{
    [Route("api/[controller]")]
    public class DoctorController : ApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetAll([FromServices] GetDoctorsQueryHandler handler)
        {
            var result = await handler.ExecuteAsync(new GetDoctorsQuery());

            return result.Match(doctors => Ok(doctors), Problem);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id, [FromServices] GetDoctorByIdQueryHandler handler)
        {
            var result = await handler.ExecuteAsync(new GetDoctorByIdQuery(id));

            return result.Match(doctor => Ok(doctor), Problem);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateDoctorCommand command, [FromServices] CreateDoctorCommandHandler handler)
        {
            var result = await handler.ExecuteAsync(command);

            return result.Match(
                doctor => CreatedAtAction(nameof(GetById), new { id = doctor.Id }, doctor),
                Problem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, UpdateDoctorCommand command, [FromServices] UpdateDoctorCommandHandler handler)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            var result = await handler.ExecuteAsync(command);

            return result.Match(doctor => Ok(doctor), Problem);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id, [FromServices] DeleteDoctorCommandHandler handler)
        {
            var result = await handler.ExecuteAsync(new DeleteDoctorCommand(id));

            return result.Match(_ => NoContent(), Problem);
        }
    }
}
