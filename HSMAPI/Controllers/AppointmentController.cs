using HSMBusiness.Features.Appointments.Commands.CreateAppointment;
using HSMBusiness.Features.Appointments.Commands.DeleteAppointment;
using HSMBusiness.Features.Appointments.Commands.UpdateAppointment;
using HSMBusiness.Features.Appointments.Queries.GetAppointmentById;
using HSMBusiness.Features.Appointments.Queries.GetAppointments;
using Microsoft.AspNetCore.Mvc;

namespace HSMAPI.Controllers
{
    [Route("api/[controller]")]
    public class AppointmentController : ApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetAll([FromServices] GetAppointmentsQueryHandler handler)
        {
            var result = await handler.ExecuteAsync(new GetAppointmentsQuery());
            return result.Match(items => Ok(items), Problem);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id, [FromServices] GetAppointmentByIdQueryHandler handler)
        {
            var result = await handler.ExecuteAsync(new GetAppointmentByIdQuery(id));
            return result.Match(item => Ok(item), Problem);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAppointmentCommand command, [FromServices] CreateAppointmentCommandHandler handler)
        {
            var result = await handler.ExecuteAsync(command);
            return result.Match(item => CreatedAtAction(nameof(GetById), new { id = item.Id }, item), Problem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, UpdateAppointmentCommand command, [FromServices] UpdateAppointmentCommandHandler handler)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            var result = await handler.ExecuteAsync(command);
            return result.Match(item => Ok(item), Problem);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id, [FromServices] DeleteAppointmentCommandHandler handler)
        {
            var result = await handler.ExecuteAsync(new DeleteAppointmentCommand(id));
            return result.Match(_ => NoContent(), Problem);
        }
    }
}
