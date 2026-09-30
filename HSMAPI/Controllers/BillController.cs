using HSMBusiness.Pattern;
using HSMBusiness.Services;
using HSMDataAccess.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HSMAPI.Controllers
{
    [Route("api/BillController")]
    [ApiController]
    public class BillController(BillService billService) : ControllerBase
    {
        [HttpGet]
        [Route("{Id}", Name = "GetBillByIdAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetBillByIdAsync(string Id)
        {
            var bill = await billService.GetByID(Id);
            if (Id == "")
            {
                return BadRequest($"{bill.Status},{bill.Response}");
            }
            if (bill.billDTO1 == null)
            {
                return NotFound($"{bill.Response},{bill.Response}");
            }

            return Ok(new { response = bill.Response, IsSuccess = bill.IsSuccess, employeeDTO = bill.billDTO1 });
        }
        [HttpGet]
        [Route("All", Name = "GetAllbills")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllbills()
        {
            var peopleList = await billService.GetAll();
            if (peopleList.billsList == null)
            {
                return NotFound($"{peopleList.Response},{peopleList.Response}");
            }
            return Ok(new
            {
                status = peopleList.Status,
                response = peopleList.Response,
                people = peopleList.billsList,

            });
        }
        [HttpPost]
        [Route("Create", Name = "CreateBill")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateBill(BillDTO bill)
        {
            var response = await new ResultPatern().GiveResponse(200, null);
            if (bill == null)
            {
                response = await new ResultPatern().GiveResponse(404, null);
                return NotFound(new
                {
                    status = response.Status,
                    response = response.Response,
                    isSuccess = response.IsSuccess,
                    errors = response.Errors
                });
            }
            if ((string.IsNullOrWhiteSpace(bill.PatientID) || string.IsNullOrWhiteSpace(bill.Status) || string.IsNullOrWhiteSpace(bill.Invoice) ||
                bill.GrossAmount < 0 || bill.PatientResponsibility < 0 || bill.ID == ""))
            {

                response = await new ResultPatern().GiveResponse(400, null);
                return BadRequest(new
                {
                    status = response.Status,
                    response = response.Response,
                    isSuccess = response.IsSuccess,
                    errors = response.Errors
                });

            }

            billService.Mode = BillService.enMode.Add;
            var result = await billService.Save( bill,bill.ID);

            if (!result.IsSuccess)
            {
                return BadRequest(new
                {
                    status = result.Status,
                    response = result.Response,
                    isSuccess = result.IsSuccess
                });
            }
            return Ok(CreatedAtRoute("GetBillByIdAsync", new { id = bill.ID }, bill));

        }
        [HttpPut]
        [Route("{ID}", Name = "UpdateBill")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateBill(string ID, BillDTO bill)
        {

            var response = await new ResultPatern().GiveResponse(200, null);
            if ((string.IsNullOrWhiteSpace(bill.PatientID) || string.IsNullOrWhiteSpace(bill.Status) || string.IsNullOrWhiteSpace(bill.Invoice) ||
                bill.GrossAmount<0 || bill.PatientResponsibility < 0 || ID == ""))
            {

                response = await new ResultPatern().GiveResponse(400, null);
                return BadRequest(new
                {
                    status = response.Status,
                    response = response.Response,
                    isSuccess = response.IsSuccess,
                    errors = response.Errors
                });

            }

            var CurrAppointment = await billService.GetByID(ID);
            if (CurrAppointment.billDTO1 == null)
            {

                return NotFound(new
                {
                    status = CurrAppointment.Status,
                    response = CurrAppointment.Response,
                    isSuccess = CurrAppointment.IsSuccess
                });
            }


            billService.Mode = BillService.enMode.Add;
            var result = await billService.Save(bill,ID);

            if (!result.IsSuccess)
                return BadRequest(new
                {
                    status = result.Status,
                    response = result.Response,
                    isSuccess = result.IsSuccess

                });
            return Ok(new
            {
                status = result.Status,
                response = result.Response,
                isSuccess = result.IsSuccess
            });
        }

        [HttpDelete]
        [Route("Delete/{id}", Name = "DeleteBill")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteBill(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest("ID is empty or not found");

            var result = await billService.Delete(id);

            if (!result.IsSuccess)
                return BadRequest(new
                {
                    status = result.Status,
                    response = result.Response,
                    isSuccess = result.IsSuccess
                });

            return Ok(new
            {
                status = result.Status,
                response = result.Response,
                isSuccess = result.IsSuccess
            });
        }
    }
}
