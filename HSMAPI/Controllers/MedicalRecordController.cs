using Azure;
using HSMBusiness;
using HSMBusiness.Erorrs;
using HSMBusiness.Pattern;
using HSMBusiness.Services;
using HSMDataAccess.DTOs;
using HSMDataAccess.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.NetworkInformation;

namespace HSMAPI.Controllers
{
    [Route("api/MedicalRecordController")]
    [ApiController]
    public class MedicalRecordController(MedicalRecordService medicalRecordService) : ControllerBase
    {
        [HttpGet]
        [Route("{Id}",Name = "GetMedicalRecordByIdAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetMedicalRecordByIdAsync(string Id)
        {
            var medicalRecord = await medicalRecordService.GetByID(Id);
            if (Id == "")
            {
                return BadRequest($"{medicalRecord.Status},{medicalRecord.Response},{medicalRecord.IsSuccess}");
            }
            if (medicalRecord.medicalRecordDTO== null)
            {
                return NotFound($"{medicalRecord.Status},{medicalRecord.Response},{medicalRecord.medicalRecordDTO}");
            }

            return Ok(medicalRecord);
        }
        [HttpGet]
        [Route("All", Name = "GetAllMedicalRecords")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllMedicalRecords()
        {
            var MedicalRecordsList = await medicalRecordService.GetAll();
            if (MedicalRecordsList.Item4 == null)
            {
                return NotFound($"{MedicalRecordsList.Status},{MedicalRecordsList.Response}");
            }
            return Ok(new
            {
                status = MedicalRecordsList.Status,
                response = MedicalRecordsList.Response,
                medicalRecords = MedicalRecordsList.ListOfMedicalRecords
            });
        }
        [HttpPost]
        [Route("Create", Name = "CreateMedicalRecord")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateMedicalRecord(MedicalRecordDTO medicalRecord)
        {
            if (string.IsNullOrWhiteSpace(medicalRecord.PatientID) || string.IsNullOrWhiteSpace(medicalRecord.AccessLevel))
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
            medicalRecordService._mode = MedicalRecordService.enMode.Add;
            var result = await medicalRecordService.Save(medicalRecord, medicalRecord.ID);

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
        [Route("{ID}", Name = "UpdateMedicalRecord")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateMedicalRecord(string ID, MedicalRecordDTO medicalRecord)
        {
            if (string.IsNullOrWhiteSpace(medicalRecord.PatientID) || string.IsNullOrWhiteSpace(medicalRecord.AccessLevel))
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
            var CurrMedicalRecord = await medicalRecordService.GetByID(ID);
            if (CurrMedicalRecord.medicalRecordDTO == null)
            {
                return NotFound(new
                {
                    status = CurrMedicalRecord.Status,
                    response = CurrMedicalRecord.Response,
                    isSuccess = CurrMedicalRecord.IsSuccess
                });
            }

           
            medicalRecordService._mode = MedicalRecordService.enMode.Update;
            var result = await medicalRecordService.Save(medicalRecord, ID);
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
        [Route("Delete/{id}", Name = "DeleteMedicalRecord")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteMedicalRecord(string id)
        {
            var response = await new ResultPatern().GiveResponse(400, null);
            if (string.IsNullOrEmpty(id)){
                return BadRequest(new
                {
                    status = response.Status,
                    response = response.Response,
                    isSuccess = response.IsSuccess
                });
            }

            var result = await medicalRecordService.Delete(id);

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
