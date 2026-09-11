using HSMBusiness.Features.MedicalRecords.Commands.CreateMedicalRecord;
using HSMBusiness.Features.MedicalRecords.Commands.DeleteMedicalRecord;
using HSMBusiness.Features.MedicalRecords.Commands.UpdateMedicalRecord;
using HSMBusiness.Features.MedicalRecords.Queries.GetMedicalRecordById;
using HSMBusiness.Features.MedicalRecords.Queries.GetMedicalRecords;
using Microsoft.AspNetCore.Mvc;

namespace HSMAPI.Controllers
{
    [Route("api/[controller]")]
    public class MedicalRecordController : ApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetAll([FromServices] GetMedicalRecordsQueryHandler handler)
        {
            var result = await handler.ExecuteAsync(new GetMedicalRecordsQuery());
            return result.Match(items => Ok(items), Problem);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id, [FromServices] GetMedicalRecordByIdQueryHandler handler)
        {
            var result = await handler.ExecuteAsync(new GetMedicalRecordByIdQuery(id));
            return result.Match(item => Ok(item), Problem);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateMedicalRecordCommand command, [FromServices] CreateMedicalRecordCommandHandler handler)
        {
            var result = await handler.ExecuteAsync(command);
            return result.Match(item => CreatedAtAction(nameof(GetById), new { id = item.Id }, item), Problem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, UpdateMedicalRecordCommand command, [FromServices] UpdateMedicalRecordCommandHandler handler)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            var result = await handler.ExecuteAsync(command);
            return result.Match(item => Ok(item), Problem);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id, [FromServices] DeleteMedicalRecordCommandHandler handler)
        {
            var result = await handler.ExecuteAsync(new DeleteMedicalRecordCommand(id));
            return result.Match(_ => NoContent(), Problem);
        }
    }
}
