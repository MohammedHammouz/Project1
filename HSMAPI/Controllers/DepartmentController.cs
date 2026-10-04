using HSMBusiness.Pattern;
using HSMBusiness.Services;
using HSMDataAccess.DTOs;
using HSMDataAccess.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HSMAPI.Controllers
{
    [Route("api/DepartmentController")]
    [ApiController]
    public class DepartmentController(DepartmentService departmentService) : ControllerBase
    {
        [HttpGet]
        [Route("{Id}", Name = "GetDepartmentByIdAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetDepartmentByIdAsync(string Id)
        {
            var department = await departmentService.GetByID(Id);
            if (Id == "")
            {
                return BadRequest($"{department.Status},{department.Response}");
            }
            if (department.departmentDTO1 == null)
            {
                return NotFound($"{department.Response},{department.Response}");
            }

            return Ok(new { response = department.Response, IsSuccess = department.IsSuccess, employeeDTO = department.departmentDTO1 });
        }
        [HttpGet]
        [Route("All", Name = "GetAlldepartments")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAlldepartments()
        {
            var ListDepartments = await departmentService.GetAll();
            if (ListDepartments.ListDepartments == null)
            {
                return NotFound($"{ListDepartments.Response},{ListDepartments.Response}");
            }
            return Ok(new
            {
                status = ListDepartments.Status,
                response = ListDepartments.Response,
                people = ListDepartments.ListDepartments,

            });
        }
        [HttpPost]
        [Route("Create", Name = "CreateDepartment")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateDepartment(DepartmentDTO department)
        {
            var response = await new ResultPatern().GiveResponse(200, null);
            if (department == null)
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
            if ((string.IsNullOrWhiteSpace(department.Name)  || department.ID == ""))
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

            departmentService._mode = DepartmentService.enMode.Add;
            var result = await departmentService.Save(department, department.ID);

            if (!result.IsSuccess)
            {
                return StatusCode(result.Status, new
                {
                    status = result.Status,
                    response = result.Response,
                    isSuccess = result.IsSuccess
                });
            }
            return Ok(CreatedAtRoute("GetDepartmentByIdAsync", new { id = department.ID }, department));

        }
        [HttpPut]
        [Route("{ID}", Name = "UpdateDepartment")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateDepartment(string ID, DepartmentDTO department)
        {

            var response = await new ResultPatern().GiveResponse(200, null);
            if ((string.IsNullOrWhiteSpace(department.Name) || ID == ""))
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

            var CurrAppointment = await departmentService.GetByID(ID);
            if (CurrAppointment.departmentDTO1 == null)
            {

                return NotFound(new
                {
                    status = CurrAppointment.Status,
                    response = CurrAppointment.Response,
                    isSuccess = CurrAppointment.IsSuccess
                });
            }


            departmentService._mode = DepartmentService.enMode.Add;
            var result = await departmentService.Save(department, ID);

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
        [Route("Delete/{id}", Name = "DeleteDepartment")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteDepartment(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest("ID is empty or not found");

            var result = await departmentService.Delete(id);

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
