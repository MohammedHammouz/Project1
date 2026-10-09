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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HSMAPI.Controllers
{
    [Route("api/AppointmentController")]
    [ApiController]
    public class AppointmentController(AppointmentService appointmentService,DoctorService doctorService,PatientService patientService,AppointmentError appointmentError) : ControllerBase
    {
        [HttpGet]
        [Route("{Id}",Name = "GetAppointmentByIdAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAppointmentByIdAsync(string Id)
        {
            var appointment = await appointmentService.GetByID(Id);
            if (Id == "")
            {
                return BadRequest($"{appointment.Status},{appointment.Response},{appointment.IsSuccess},{appointment.errors}");
            }
            if (appointment.appointmentDTO == null)
            {
                return NotFound($"{appointment.Response},{appointment.Response},{appointment.errors}");
            }

            return Ok($"{appointment.Response},{appointment.Response},{appointment.errors}");
        }
        [HttpGet]
        [Route("All",Name = "GetAllAppointments")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllAppointments()
        {
            var appointmentsList = await appointmentService.GetAll();
            if (appointmentsList.Item4 == null)
            {
                return NotFound($"{appointmentsList.Response},{appointmentsList.Response},{appointmentsList.errors}");
            }
            return Ok(new
            {
                status = appointmentsList.Status,
                response = appointmentsList.Response,
                appointments = appointmentsList.appointmentList,
                errors = appointmentsList.errors
            });
        }
        [HttpPost]
        [Route("Create",Name ="CreateAppointment")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateAppointment(AppointmentDTO appointment)
        {
            if (string.IsNullOrWhiteSpace(appointment.DoctorID) || string.IsNullOrWhiteSpace(appointment.PatientID) || string.IsNullOrWhiteSpace(appointment.Status) || appointment.Duration <= 0)
            {
                var errors = await appointmentError.ValidateAppointment(appointment);
                if (errors.Count > 0) { 
                    var response = await new ResultPatern().GiveResponse(400, errors); 
                    return BadRequest(new { status = response.Status, response = response.Response, isSuccess = response.IsSuccess, errors = response.Errors
                    }); 
                }
            }
            
            appointmentService._mode = AppointmentService.enMode.Add;
            var result = await appointmentService.Save(appointment,appointment.ID);

            if (!result.IsSuccess)
            {
                return StatusCode(result.Status, new
                {
                    status = result.Status,
                    response = result.Response,
                    isSuccess = result.IsSuccess
                });
            }
            return Ok(CreatedAtRoute("GetAppointmentByIdAsync", new {id=appointment.ID}, appointment));

        }
        [HttpPut]
        [Route("{ID}",Name = "UpdateAppointment")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateAppointment(string ID, AppointmentDTO appointment)
        {
            if (string.IsNullOrEmpty(ID))
            {
                var errors = await appointmentError.ValidateAppointment(appointment);
                var response = await new ResultPatern().GiveResponse(400,errors);
                return BadRequest(new
                {
                    status = response.Status,
                    response = response.Response,
                    isSuccess = response.IsSuccess,
                    errors = response.Errors
                });
            }
           var CurrAppointment =await appointmentService.GetByID(ID);
            if (CurrAppointment.appointmentDTO == null)
            {

                return NotFound(new
                {
                    status = CurrAppointment.Status,
                    response = CurrAppointment.Response,
                    isSuccess = CurrAppointment.IsSuccess,
                    errors = CurrAppointment.errors
                });
            }
            if (string.IsNullOrWhiteSpace(appointment.DoctorID) || string.IsNullOrWhiteSpace(appointment.PatientID) || string.IsNullOrWhiteSpace(appointment.Status) || appointment.Duration <= 0)
            {
                var errors = await appointmentError.ValidateAppointment(appointment);
                if (errors.Count > 0)
                {
                    var response = await new ResultPatern().GiveResponse(400, errors);
                    return BadRequest(new
                    {
                        status = response.Status,
                        response = response.Response,
                        isSuccess = response.IsSuccess,
                        errors = response.Errors
                    });
                }
            }

            appointmentService._mode = AppointmentService.enMode.Update;
            var result = await appointmentService.Save(appointment, ID);

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
                isSuccess = result.IsSuccess,
                errors = result.errors
            });
        }

        [HttpDelete]
        [Route("Delete/{id}", Name = "DeleteAppointment")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAppointment(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest("ID is empty or not found");
                
            var result = await appointmentService.Delete(id);

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
