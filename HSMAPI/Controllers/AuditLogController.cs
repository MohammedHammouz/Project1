using HSMBusiness.Pattern;
using HSMBusiness.Services;
using HSMDataAccess.DTOs;
using HSMDataAccess.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HSMAPI.Controllers
{
    [Route("api/AuditLogController")]
    [ApiController]
    public class AuditLogController(AuditLogService auditLogService) : ControllerBase
    {
        [HttpGet]
        [Route("{Id}", Name = "GetAuditLogByIdAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAuditLogByIdAsync(string Id)
        {
            var auditlog = await auditLogService.GetByID(Id);
            if (Id == "")
            {
                return BadRequest($"{auditlog.Status},{auditlog.Response}");
            }
            if (auditlog.auditLogDTO1 == null)
            {
                return NotFound($"{auditlog.Response},{auditlog.Response}");
            }

            return Ok(new { response = auditlog.Response, IsSuccess = auditlog.IsSuccess, employeeDTO = auditlog.auditLogDTO1 });
        }
        [HttpGet]
        [Route("All", Name = "GetAllAuditLogs")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllAuditLogs()
        {
            var ListAuditLogs = await auditLogService.GetAll();
            if (ListAuditLogs.listOfAuditLogs == null)
            {
                return NotFound($"{ListAuditLogs.Response},{ListAuditLogs.Response}");
            }
            return Ok(new
            {
                status = ListAuditLogs.Status,
                response = ListAuditLogs.Response,
                listOfAuditLogs = ListAuditLogs.listOfAuditLogs,

            });
        }
        [HttpPost]
        [Route("Create", Name = "CreateAuditLog")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateDepartment(AuditLogDTO auditLog)
        {
            var response = await new ResultPatern().GiveResponse(200, null);
            if (auditLog == null)
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
            if (string.IsNullOrWhiteSpace(auditLog.UserID) || auditLog.ID == "" || string.IsNullOrWhiteSpace(auditLog.Entity) || string.IsNullOrWhiteSpace(auditLog.Action) )
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

            auditLogService._mode = AuditLogService.enMode.Add;
            var result = await auditLogService.Save(auditLog, auditLog.ID);

            if (!result.IsSuccess)
            {
                return BadRequest(new
                {
                    status = result.Status,
                    response = result.Response,
                    isSuccess = result.IsSuccess
                });
            }
            return Ok(CreatedAtRoute("GetAuditLogByIdAsync", new { id = auditLog.ID }, auditLog));

        }
        [HttpPut]
        [Route("{ID}", Name = "UpdateAuditLog")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateAuditLog(string ID, AuditLogDTO auditLog)
        {

            var response = await new ResultPatern().GiveResponse(200, null);
            if (string.IsNullOrWhiteSpace(auditLog.UserID) || auditLog.ID == "" || string.IsNullOrWhiteSpace(auditLog.Entity) || string.IsNullOrWhiteSpace(auditLog.Action))
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

            var CurrAuditLog = await auditLogService.GetByID(ID);
            if (CurrAuditLog.auditLogDTO1 == null)
            {

                return NotFound(new
                {
                    status = CurrAuditLog.Status,
                    response = CurrAuditLog.Response,
                    isSuccess = CurrAuditLog.IsSuccess
                });
            }


            auditLogService._mode = AuditLogService.enMode.Add;
            var result = await auditLogService.Save(auditLog, ID);

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
        [Route("Delete/{id}", Name = "DeleteAuditLog")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAuditLog(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest("ID is empty or not found");

            var result = await auditLogService.Delete(id);

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
