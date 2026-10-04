using HSMBusiness.Erorrs;
using HSMBusiness.Pattern;
using HSMBusiness.Services;
using HSMDataAccess.DTOs;
using HSMDataAccess.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HSMAPI.Controllers
{
    [Route("api/EmployeeController")]
    [ApiController]
    public class EmployeeController(EmployeeService employeeService) : ControllerBase
    {
        [HttpGet]
        [Route("{Id}", Name = "GetEmployeeByIdAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetEmployeeByIdAsync(string Id)
        {
            var employee = await employeeService.GetByID(Id);
            if (Id == "")
            {
                return BadRequest($"{employee.Status},{employee.Response}");
            }
            if (employee.employeeDTO1 == null)
            {
                return NotFound($"{employee.Status},{employee.Response}");
            }

            return Ok(new {response= employee.Response,IsSuccess= employee.IsSuccess,employeeDTO= employee.employeeDTO1 });
        }
        [HttpGet]
        [Route("All", Name = "GetAllEmployees")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employeesList = await employeeService.GetAll();
            if (employeesList.employeesList == null)
            {
                return NotFound($"{employeesList.Response},{employeesList.Response}");
            }
            return Ok(new
            {
                status = employeesList.Status,
                response = employeesList.Response,
                employees = employeesList.employeesList,
                
            });
        }
        [HttpPost]
        [Route("Create", Name = "CreateEmployee")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateEmployee(EmployeeDTO employee)
        {
            var response = await new ResultPatern().GiveResponse(200, null);
            if (employee == null)
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
            if (string.IsNullOrWhiteSpace(employee.PersonID) || employee.Salary<0 || employee.ID=="")
            {
               
                    response = await new ResultPatern().GiveResponse(400,null);
                    return BadRequest(new
                    {
                        status = response.Status,
                        response = response.Response,
                        isSuccess = response.IsSuccess,
                        errors = response.Errors
                    });
                
            }

            employeeService.Mode = EmployeeService.enMode.Add;
            var result = await employeeService.Save(employee, employee.ID);

            if (!result.IsSuccess)
            {
                return StatusCode(result.Status, new
                {
                    status = result.Status,
                    response = result.Response,
                    isSuccess = result.IsSuccess
                });
            }
            return Ok(CreatedAtRoute("GetEmployeeByIdAsync", new { id = employee.ID }, employee));

        }
        [HttpPut]
        [Route("{ID}",Name = "UpdateEmployee")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateEmployee(string ID, EmployeeDTO employee)
        {
            
                var response = await new ResultPatern().GiveResponse(200, null);
            if (string.IsNullOrWhiteSpace(employee.PersonID) || employee.Salary < 0 || ID == "")
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

            var CurrAppointment = await employeeService.GetByID(ID);
            if (CurrAppointment.employeeDTO1== null)
            {

                return NotFound(new
                {
                    status = CurrAppointment.Status,
                    response = CurrAppointment.Response,
                    isSuccess = CurrAppointment.IsSuccess
                });
            }
            if (string.IsNullOrWhiteSpace(employee.PersonID) || employee.Salary < 0)
            {
               
                    response = await new ResultPatern().GiveResponse(400, null);
                    return BadRequest(new
                    {
                        status = response.Status,
                        response = response.Response,
                        isSuccess = response.IsSuccess,
                        
                    });
                
            }

           employeeService.Mode = EmployeeService.enMode.Update;
            var result = await employeeService.Save(employee, ID);

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
        [Route("Delete/{id}", Name = "DeleteEmployee")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteEmployee(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest("ID is empty or not found");

            var result = await employeeService.Delete(id);

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

