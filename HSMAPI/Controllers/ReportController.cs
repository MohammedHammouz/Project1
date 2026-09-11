using HSMBusiness.Features.Reports.Commands.CreateReport;
using HSMBusiness.Features.Reports.Commands.DeleteReport;
using HSMBusiness.Features.Reports.Commands.UpdateReport;
using HSMBusiness.Features.Reports.Queries.GetReportById;
using HSMBusiness.Features.Reports.Queries.GetReports;
using Microsoft.AspNetCore.Mvc;

namespace HSMAPI.Controllers
{
    [Route("api/[controller]")]
    public class ReportController : ApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetAll([FromServices] GetReportsQueryHandler handler)
        {
            var result = await handler.ExecuteAsync(new GetReportsQuery());
            return result.Match(items => Ok(items), Problem);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id, [FromServices] GetReportByIdQueryHandler handler)
        {
            var result = await handler.ExecuteAsync(new GetReportByIdQuery(id));
            return result.Match(item => Ok(item), Problem);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateReportCommand command, [FromServices] CreateReportCommandHandler handler)
        {
            var result = await handler.ExecuteAsync(command);
            return result.Match(item => CreatedAtAction(nameof(GetById), new { id = item.Id }, item), Problem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, UpdateReportCommand command, [FromServices] UpdateReportCommandHandler handler)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            var result = await handler.ExecuteAsync(command);
            return result.Match(item => Ok(item), Problem);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id, [FromServices] DeleteReportCommandHandler handler)
        {
            var result = await handler.ExecuteAsync(new DeleteReportCommand(id));
            return result.Match(_ => NoContent(), Problem);
        }
    }
}
