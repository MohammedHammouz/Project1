using Azure;
using HSMBusiness.Pattern;
using HSMBusiness.Services;
using HSMDataAccess.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.NetworkInformation;

namespace HSMAPI.Controllers
{
    [Route("api/NotificationController")]
    [ApiController]
    public class NotificationController(NotificationService notificationService) : ControllerBase
    {
        [HttpGet]
        [Route("{Id}", Name = "GetNotifictionByIdAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GeNotifictionByIdAsync(string Id)
        {
            var notification = await notificationService.GetByID(Id);
            if (Id == "")
            {
                return BadRequest($"{notification.Status},{notification.Response},{notification.IsSuccess}");
            }
            if (notification.notifictionDTO1 == null)
            {
                return NotFound($"{notification.Status},{notification.Response},{notification.notifictionDTO1}");
            }
            return Ok($"{notification.Status},{notification.Response},{notification.notifictionDTO1}");
        }
        [HttpGet]
        [Route("All", Name = "GetAllNotifictions")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllNotifictions()
        {
            var MedicalRecordsList = await notificationService.GetAll();
            if (MedicalRecordsList.listOfNotifications == null)
            {
                return NotFound($"{MedicalRecordsList.Status},{MedicalRecordsList.Response}");
            }
            return Ok(new
            {
                status = MedicalRecordsList.Status,
                response = MedicalRecordsList.Response,
                medicalRecords = MedicalRecordsList.listOfNotifications
            });
        }
        [HttpPost]
        [Route("Create", Name = "CreateNotifiction")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateNotifiction(NotificationDTO notificationDTO)
        {
            if (string.IsNullOrWhiteSpace(notificationDTO.PatientID) || string.IsNullOrWhiteSpace(notificationDTO.UserID)||string.IsNullOrEmpty(notificationDTO.Type)||string.IsNullOrWhiteSpace(notificationDTO.Message) || string.IsNullOrWhiteSpace(notificationDTO.Status))
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
            notificationService._mode = NotificationService.enMode.Add;
            var result = await notificationService.Save(notificationDTO, notificationDTO.ID);

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
        [Route("{ID}", Name = "UpdateNotifiction")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateNotifiction(string ID, NotificationDTO notificationDTO)
        {
            if (string.IsNullOrWhiteSpace(notificationDTO.PatientID) || string.IsNullOrWhiteSpace(notificationDTO.UserID) || string.IsNullOrEmpty(notificationDTO.Type) || string.IsNullOrWhiteSpace(notificationDTO.Message) || string.IsNullOrWhiteSpace(notificationDTO.Status))
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
            var CurrNotifiction = await notificationService.GetByID(ID);
            if (CurrNotifiction.notifictionDTO1 == null)
            {
                return NotFound(new
                {
                    status = CurrNotifiction.Status,
                    response = CurrNotifiction.Response,
                    isSuccess = CurrNotifiction.IsSuccess
                });
            }

            CurrNotifiction.notifictionDTO1 = notificationDTO;
            notificationService._mode = NotificationService.enMode.Update;
            var result = await notificationService.Save(notificationDTO, ID);
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
        [Route("Delete/{id}", Name = "DeleteNotifiction")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteNotifiction(string id)
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

            var result = await notificationService.Delete(id);

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

