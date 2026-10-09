using HSMBusiness.Pattern;
using HSMBusiness.Services;
using HSMDataAccess.DTOs;
using HSMDataAccess.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HSMAPI.Controllers
{
    [Route("api/ReportController")]
    [ApiController]
    public class ReportController(ReportService reportService) : ControllerBase
    {
        [HttpGet]
        [Route("{Id}", Name = "GetReportByIdAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetReportByIdAsync(string Id)
        {
            var report = await reportService.GetByID(Id);
            if (Id == "")
            {
                return BadRequest($"{report.Status},{report.Response},{report.IsSuccess}");
            }
            if (report.reportDTO1 == null)
            {
                return NotFound($"{report.Status},{report.Response},{report.reportDTO1}");
            }
            return Ok(report.reportDTO1);
        }
        [HttpGet]
        [Route("All", Name = "GetAllReports")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllReports()
        {
            var MedicalRecordsList = await reportService.GetAll();
            if (MedicalRecordsList.ListOfReports == null)
            {
                return NotFound($"{MedicalRecordsList.Status},{MedicalRecordsList.Response}");
            }
            return Ok(new
            {
                status = MedicalRecordsList.Status,
                response = MedicalRecordsList.Response,
                listOfReports = MedicalRecordsList.ListOfReports
            });
        }
        [HttpPost]
        [Route("Create", Name = "CreateReport")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateReport(ReportDTO reportDTO)
        {
            if (string.IsNullOrWhiteSpace(reportDTO.GeneratedBy)||string.IsNullOrEmpty(reportDTO.ExportFormat))
            {
                var response = await new ResultPatern().GiveResponse(400, null);
                return BadRequest(new
                {
                    status = response.Status,
                    response = response.Response,
                    isSuccess = response.IsSuccess,
                    errors = response.Errors
                });
            }
            reportService._mode = ReportService.enMode.Add;
            var result = await reportService.Save(reportDTO, reportDTO.ID);

            if (!result.IsSuccess)
            {
                return StatusCode(result.Status, new
                {
                    status = result.Status,
                    response = result.Response,
                    isSuccess = result.IsSuccess
                });
            }
            return Ok(new
            {
                status = result.Status,
                response = result.Response,
                isSuccess = result.IsSuccess
            });

        }
        [HttpPut]
        [Route("{ID}", Name = "UpdateReport")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateReport(string ID, ReportDTO reportDTO)
        {
            if (string.IsNullOrWhiteSpace(reportDTO.GeneratedBy) || string.IsNullOrWhiteSpace(ID) || string.IsNullOrEmpty(reportDTO.ExportFormat))
            {
                var response = await new ResultPatern().GiveResponse(400, null);
                return BadRequest(new
                {
                    status = response.Status,
                    response = response.Response,
                    isSuccess = response.IsSuccess,
                    errors = response.Errors
                });
            }
            var CurrReport = await reportService.GetByID(ID);
            if (CurrReport.reportDTO1 == null)
            {
                return NotFound(new
                {
                    status = CurrReport.Status,
                    response = CurrReport.Response,
                    isSuccess = CurrReport.IsSuccess
                });
            }

            CurrReport.reportDTO1 = reportDTO;
            reportService._mode = ReportService.enMode.Update;
            var result = await reportService.Save(reportDTO, ID);
            if (!result.IsSuccess)
                return StatusCode(result.Status, new
                {
                    status = result.Status,
                    response = result.Response,
                    isSuccess = result.IsSuccess
                });
            return Ok(
            
                CreatedAtRoute("GetReportByIdAsync", new { id = reportDTO.ID }, reportDTO)
            );
        }

        [HttpDelete]
        [Route("Delete/{id}", Name = "DeleteReport")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteReport(string id)
        {
            var response = await new ResultPatern().GiveResponse(400, null);
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest(new
                {
                    status = response.Status,
                    response = response.Response,
                    isSuccess = response.IsSuccess
                });
            }

            var result = await reportService.Delete(id);

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
