using HSMBusiness.Pattern;
using HSMBusiness.Services;
using HSMDataAccess.DTOs;
using HSMDataAccess.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HSMAPI.Controllers
{
    [Route("api/BillServiceController")]
    [ApiController]
    public class BillServicesController(BillServiceService billServiceService) : ControllerBase
    {
        [HttpGet]
        [Route("{ServiceID}/{BillID}", Name = " GetBillServiceByIdAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetBillServiceByIdAsync(string ServiceID, string BillID)
        {
            var billService = await billServiceService.GetByID(ServiceID,BillID);
            if (ServiceID == "")
            {
                return BadRequest($"{billService.Status},{billService.Response}");
            }
            if (billService.billServiceDTO1 == null)
            {
                return NotFound($"{billService.Status},{billService.Response}");
            }

            return Ok(new { response = billService.Response, IsSuccess = billService.IsSuccess, employeeDTO = billService.billServiceDTO1 });
        }
        [HttpGet]
        [Route("All", Name = "GetAllBillServices")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllBillServices()
        {
            var BillServiceList = await billServiceService.GetAll();
            if (BillServiceList.ListOfBillService == null)
            {
                return NotFound($"{BillServiceList.Status},{BillServiceList.Response}");
            }
            return Ok(new
            {
                status = BillServiceList.Status,
                response = BillServiceList.Response,
                people = BillServiceList.ListOfBillService,

            });
        }
        [HttpPost]
        [Route("Create", Name = "CreateGetAllBillServices")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateGetAllBillServices(BillServiceDTO billservice)
        {
            var response = await new ResultPatern().GiveResponse(200, null);
            if (billservice == null)
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
            if ((string.IsNullOrWhiteSpace(billservice.BillID) || string.IsNullOrWhiteSpace(billservice.ServiceID) || billservice.UnitPrice<1))
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

            billServiceService._mode = BillServiceService.enMode.Add;
            var result = await billServiceService.Save(billservice, billservice.ServiceID,billservice.BillID);

            if (!result.IsSuccess)
            {
                return StatusCode(result.Status, new
                {
                    status = result.Status,
                    response = result.Response,
                    isSuccess = result.IsSuccess
                });
            }
            return Ok(CreatedAtRoute(" GetBillServiceByIdAsync", new { ServiceID = billservice.ServiceID ,BillID=billservice.BillID}, billservice));

        }
        [HttpPut]
        [Route("{ServiceID}/{BillID}", Name = "UpdateBillService")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateBillService(string ServiceID,string BillID, BillServiceDTO billserviceDTO)
        {

            var response = await new ResultPatern().GiveResponse(200, null);
            if ((string.IsNullOrWhiteSpace(BillID) || string.IsNullOrWhiteSpace(ServiceID) || billserviceDTO.UnitPrice < 1))
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

            var CurrAppointment = await billServiceService.GetByID(ServiceID,BillID);
            if (CurrAppointment.billServiceDTO1== null)
            {

                return NotFound(new
                {
                    status = CurrAppointment.Status,
                    response = CurrAppointment.Response,
                    isSuccess = CurrAppointment.IsSuccess
                });
            }


            billServiceService._mode = BillServiceService.enMode.Update;
            var result = await billServiceService.Save(billserviceDTO, ServiceID,BillID);

            if (!result.IsSuccess)
                return StatusCode(result.Status, new
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
        [Route("Delete/{ServiceID}/{BillID}", Name = "DeleteBillService")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteBillService(string ServiceID,string BillID)
        {
            var response = await new ResultPatern().GiveResponse(200, null);
            if (string.IsNullOrEmpty(ServiceID) || string.IsNullOrEmpty(BillID)){
                response = await new ResultPatern().GiveResponse(400, null);
                return BadRequest(new
                {
                    status = response.Status,
                    response = response.Response,
                    isSuccess = response.IsSuccess,
                    errors = response.Errors
                });
            }

            var result = await billServiceService.Delete(ServiceID,BillID);

            if (!result.IsSuccess)
                return StatusCode(result.Status, new
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
