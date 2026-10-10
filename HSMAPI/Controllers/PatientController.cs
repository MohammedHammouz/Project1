using HSMBusiness.Pattern;
using HSMBusiness.Services;
using HSMDataAccess.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HSMAPI.Controllers
{
    [Route("api/PatientController")]
    [ApiController]
    public class PatientController(PatientService patientService) : ControllerBase
    {
        [HttpGet]
        [Route("{Id}", Name = "GetPatientByIdAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPatientByIdAsync(string Id)
        {
            var patient = await patientService.GetByID(Id);
            if (Id == "")
            {
                return BadRequest($"{patient.Status},{patient.Response},{patient.IsSuccess}");
            }
            if (patient.patientDTO1 == null)
            {
                return NotFound($"{patient.Status},{patient.Response},{patient.patientDTO1}");
            }
            return Ok(patient.patientDTO1);
        }
        [HttpGet]
        [Route("All", Name = "GetAllPatients")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllPatients()
        {
            var PatientRecordsList = await patientService.GetAll();
            if (PatientRecordsList.ListOfPatients == null)
            {
                return NotFound($"{PatientRecordsList.Status},{PatientRecordsList.Response}");
            }
            return Ok(new
            {
                status = PatientRecordsList.Status,
                response = PatientRecordsList.Response,
                listOfPatients = PatientRecordsList.ListOfPatients
            });
        }
        [HttpPost]
        [Route("Create", Name = "CreatePatient")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreatePatient(PatientDTO patientDTO)
        {
            if (string.IsNullOrWhiteSpace(patientDTO.MedicalHistory) || string.IsNullOrEmpty(patientDTO.PersonID))
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
            patientService._Mode = PatientService.enMode.Add;
            var result = await patientService.Save(patientDTO, patientDTO.ID);

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
        [Route("{ID}", Name = "UpdatePatient")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdatePatient(string ID, PatientDTO patientDTO)
        {
            if (string.IsNullOrWhiteSpace(patientDTO.MedicalHistory) || string.IsNullOrEmpty(patientDTO.PersonID) || string.IsNullOrWhiteSpace(ID) )
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
            var CurrPatient = await patientService.GetByID(ID);
            if (CurrPatient.patientDTO1 == null)
            {
                return NotFound(new
                {
                    status = CurrPatient.Status,
                    response = CurrPatient.Response,
                    isSuccess = CurrPatient.IsSuccess
                });
            }

            CurrPatient.patientDTO1 = patientDTO;
            patientService._Mode = PatientService.enMode.Update;
            var result = await patientService.Save(patientDTO, ID);
            if (!result.IsSuccess)
                return StatusCode(result.Status, new
                {
                    status = result.Status,
                    response = result.Response,
                    isSuccess = result.IsSuccess
                });
            return Ok(

                CreatedAtRoute("GetPatientByIdAsync", new { id = patientDTO.ID }, patientDTO)
            );
        }

        [HttpDelete]
        [Route("Delete/{id}", Name = "DeletePatient")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeletePatient(string id)
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

            var result = await patientService.Delete(id);

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
