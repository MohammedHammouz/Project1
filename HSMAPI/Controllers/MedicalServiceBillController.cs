using HSMBusiness.Features.MedicalServiceBills.Commands.CreateMedicalServiceBill;
using HSMBusiness.Features.MedicalServiceBills.Commands.DeleteMedicalServiceBill;
using HSMBusiness.Features.MedicalServiceBills.Commands.UpdateMedicalServiceBill;
using HSMBusiness.Features.MedicalServiceBills.Queries.GetMedicalServiceBillById;
using HSMBusiness.Features.MedicalServiceBills.Queries.GetMedicalServiceBills;
using Microsoft.AspNetCore.Mvc;

namespace HSMAPI.Controllers
{
    [Route("api/[controller]")]
    public class MedicalServiceBillController : ApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetAll([FromServices] GetMedicalServiceBillsQueryHandler handler)
        {
            var result = await handler.ExecuteAsync(new GetMedicalServiceBillsQuery());
            return result.Match(items => Ok(items), Problem);
        }

        [HttpGet("{serviceId}/{billId}")]
        public async Task<IActionResult> GetById(string serviceId, string billId, [FromServices] GetMedicalServiceBillByIdQueryHandler handler)
        {
            var result = await handler.ExecuteAsync(new GetMedicalServiceBillByIdQuery(serviceId, billId));
            return result.Match(item => Ok(item), Problem);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateMedicalServiceBillCommand command, [FromServices] CreateMedicalServiceBillCommandHandler handler)
        {
            var result = await handler.ExecuteAsync(command);
            return result.Match(
                item => CreatedAtAction(nameof(GetById), new { serviceId = item.ServiceId, billId = item.BillId }, item),
                Problem);
        }

        [HttpPut("{serviceId}/{billId}")]
        public async Task<IActionResult> Update(string serviceId, string billId, UpdateMedicalServiceBillCommand command, [FromServices] UpdateMedicalServiceBillCommandHandler handler)
        {
            if (serviceId != command.ServiceId || billId != command.BillId)
            {
                return BadRequest();
            }

            var result = await handler.ExecuteAsync(command);
            return result.Match(item => Ok(item), Problem);
        }

        [HttpDelete("{serviceId}/{billId}")]
        public async Task<IActionResult> Delete(string serviceId, string billId, [FromServices] DeleteMedicalServiceBillCommandHandler handler)
        {
            var result = await handler.ExecuteAsync(new DeleteMedicalServiceBillCommand(serviceId, billId));
            return result.Match(_ => NoContent(), Problem);
        }
    }
}
