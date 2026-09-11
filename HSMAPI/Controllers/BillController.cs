using HSMBusiness.Features.Bills.Commands.CreateBill;
using HSMBusiness.Features.Bills.Commands.DeleteBill;
using HSMBusiness.Features.Bills.Commands.UpdateBill;
using HSMBusiness.Features.Bills.Queries.GetBillById;
using HSMBusiness.Features.Bills.Queries.GetBills;
using Microsoft.AspNetCore.Mvc;

namespace HSMAPI.Controllers
{
    [Route("api/[controller]")]
    public class BillController : ApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetAll([FromServices] GetBillsQueryHandler handler)
        {
            var result = await handler.ExecuteAsync(new GetBillsQuery());
            return result.Match(items => Ok(items), Problem);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id, [FromServices] GetBillByIdQueryHandler handler)
        {
            var result = await handler.ExecuteAsync(new GetBillByIdQuery(id));
            return result.Match(item => Ok(item), Problem);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateBillCommand command, [FromServices] CreateBillCommandHandler handler)
        {
            var result = await handler.ExecuteAsync(command);
            return result.Match(item => CreatedAtAction(nameof(GetById), new { id = item.Id }, item), Problem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, UpdateBillCommand command, [FromServices] UpdateBillCommandHandler handler)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            var result = await handler.ExecuteAsync(command);
            return result.Match(item => Ok(item), Problem);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id, [FromServices] DeleteBillCommandHandler handler)
        {
            var result = await handler.ExecuteAsync(new DeleteBillCommand(id));
            return result.Match(_ => NoContent(), Problem);
        }
    }
}
