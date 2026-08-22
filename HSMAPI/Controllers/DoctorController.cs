using HSMBusiness;
using HSMDataAccess.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HSMAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly DoctorService _doctor;
        public DoctorController(DoctorService doctor)
        {
            _doctor = doctor;
        }
        [HttpGet("AllDoctors", Name = "GetAllDoctors")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetAllDoctors()
        {
            var doctors = await _doctor.GetAll();
            if (doctors == null)
            {
                return NotFound("");
            }
            return Ok(doctors);
        }
        [HttpGet("AddNewDoctor/{ID}", Name = "GetDoctorByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<DoctorDTO>> GetDoctorByID(string ID)
        {
            if (ID == "")
                return BadRequest($"Not accepted id {ID}");
            var doctor = await _doctor.GetDoctorByID(ID);
            if (doctor == null)
            {
                return NotFound("");
            }

            return Ok(doctor);
        }
        [HttpPost("AddNewDoctor", Name = "AddNew")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PersonDTO>> AddNew(DoctorDTO newDoctor)
        {
            if (newDoctor == null)
            {
                return NotFound("");
            }
            if (string.IsNullOrEmpty(newDoctor.DepartmentID) || string.IsNullOrEmpty(newDoctor.UserID) ||
                  string.IsNullOrEmpty(newDoctor.Specialization) || newDoctor.Status==false
                 )
            {
                return BadRequest("Invalid Person data.");
            }
            else
            {
                DoctorService doctor = new DoctorService(_doctor.doctorRepository);
                doctor.DepartmentID = newDoctor.DepartmentID;
                doctor.Status = newDoctor.Status;
                doctor.UserID = newDoctor.UserID;
                doctor.Specialization = newDoctor.Specialization;
                await doctor.Save();
                return CreatedAtRoute("GetDoctorByID", new { id = newDoctor.ID }, newDoctor);
            }

        }
        [HttpDelete("DeleteDoctor/{ID}", Name = "DeleteDoctor")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult>DeleteDoctor(string ID)
        {
            if (ID == "")
                return NotFound("");
            bool IsFound = await _doctor.Delete(ID);
            if (!IsFound)
                return BadRequest("");
            return Ok($"Deleted SuccessFully {ID}");
        }
    }
}
