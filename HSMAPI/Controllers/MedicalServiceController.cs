using HSMBusiness.Features.MedicalServices.Commands.CreateMedicalService;
using HSMBusiness.Features.MedicalServices.Commands.DeleteMedicalService;
using HSMBusiness.Features.MedicalServices.Commands.UpdateMedicalService;
using HSMBusiness.Features.MedicalServices.Queries.GetMedicalServiceById;
using HSMBusiness.Features.MedicalServices.Queries.GetMedicalServices;
using Microsoft.AspNetCore.Mvc;

namespace HSMAPI.Controllers
{
    [Route("api/[controller]")]
    public class MedicalServiceController : ApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetAll([FromServices] GetMedicalServicesQueryHandler handler)
        {
            var result = await handler.ExecuteAsync(new GetMedicalServicesQuery());
            return result.Match(items => Ok(items), Problem);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id, [FromServices] GetMedicalServiceByIdQueryHandler handler)
        {
            var result = await handler.ExecuteAsync(new GetMedicalServiceByIdQuery(id));
            return result.Match(item => Ok(item), Problem);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateMedicalServiceCommand command, [FromServices] CreateMedicalServiceCommandHandler handler)
        {
            var result = await handler.ExecuteAsync(command);
            return result.Match(item => CreatedAtAction(nameof(GetById), new { id = item.Id }, item), Problem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, UpdateMedicalServiceCommand command, [FromServices] UpdateMedicalServiceCommandHandler handler)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            var result = await handler.ExecuteAsync(command);
            return result.Match(item => Ok(item), Problem);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id, [FromServices] DeleteMedicalServiceCommandHandler handler)
        {
            var result = await handler.ExecuteAsync(new DeleteMedicalServiceCommand(id));
            return result.Match(_ => NoContent(), Problem);
        }
    }
}
