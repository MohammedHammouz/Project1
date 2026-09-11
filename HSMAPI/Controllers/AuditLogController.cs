using HSMBusiness.Features.AuditLogs.Commands.CreateAuditLog;
using HSMBusiness.Features.AuditLogs.Commands.DeleteAuditLog;
using HSMBusiness.Features.AuditLogs.Commands.UpdateAuditLog;
using HSMBusiness.Features.AuditLogs.Queries.GetAuditLogById;
using HSMBusiness.Features.AuditLogs.Queries.GetAuditLogs;
using Microsoft.AspNetCore.Mvc;

namespace HSMAPI.Controllers
{
    [Route("api/[controller]")]
    public class AuditLogController : ApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetAll([FromServices] GetAuditLogsQueryHandler handler)
        {
            var result = await handler.ExecuteAsync(new GetAuditLogsQuery());
            return result.Match(items => Ok(items), Problem);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id, [FromServices] GetAuditLogByIdQueryHandler handler)
        {
            var result = await handler.ExecuteAsync(new GetAuditLogByIdQuery(id));
            return result.Match(item => Ok(item), Problem);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAuditLogCommand command, [FromServices] CreateAuditLogCommandHandler handler)
        {
            var result = await handler.ExecuteAsync(command);
            return result.Match(item => CreatedAtAction(nameof(GetById), new { id = item.Id }, item), Problem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, UpdateAuditLogCommand command, [FromServices] UpdateAuditLogCommandHandler handler)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            var result = await handler.ExecuteAsync(command);
            return result.Match(item => Ok(item), Problem);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id, [FromServices] DeleteAuditLogCommandHandler handler)
        {
            var result = await handler.ExecuteAsync(new DeleteAuditLogCommand(id));
            return result.Match(_ => NoContent(), Problem);
        }
    }
}
