using HSMBusiness.Services;
using HSMDataAccess.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HSMAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeService _employee;
        public EmployeeController(EmployeeService employee)
        {
            _employee = employee;
        }
        [HttpGet("AddNewEmployee/{ID}", Name = "GetEmployeeByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<DoctorDTO>> GetEmployeeByID(int ID)
        {
            if (ID <1)
                return BadRequest($"Not accepted id {ID}");
            var employee = await _employee.GetByID(ID);
            if (employee == null)
            {
                return NotFound("");
            }

            return Ok(employee);
        }
        [HttpPost("AddNewEmployee", Name = "AddNewEmployee")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PersonDTO>> AddNewEmployee(EmployeeDTO newEmployee)
        {
            if (newEmployee == null)
            {
                return NotFound("");
            }
            if (string.IsNullOrEmpty(newEmployee.PersonID)
                 )
            {
                return BadRequest("Invalid Person data.");
            }
            else
            {
                EmployeeService employee = new EmployeeService(_employee.employeeRepository);
                
                
                await employee.Save();
                return CreatedAtRoute("GetEmployeeByID", new { id = newEmployee.ID }, newEmployee);
            }

        }
    }
}
