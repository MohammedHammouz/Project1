using HSMBusiness.Pattern;
using HSMBusiness.Services;
using HSMDataAccess.DTOs;
using HSMDataAccess.RepositoryServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
namespace HSMAPI.Controllers
{
    [Route("api/PersonController")]
    [ApiController]
    public class PersonController(PersonService personService) : ControllerBase
    {
        [HttpGet]
        [Route("{Id}", Name = "GetPersonByIdAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPersonByIdAsync(string Id)
        {
            var person = await personService.GetByID(Id);
            if (string.IsNullOrEmpty(Id))
            {
                return BadRequest($"{person.Status},{person.Response}");
            }
            if (person.personDTO1 == null)
            {
                return NotFound($"{person.Status},{person.Response}");
            }

            return Ok(new { response = person.Response, IsSuccess = person.IsSuccess, employeeDTO = person.personDTO1 });
        }
        [HttpGet]
        [Route("All", Name = "GetAllPeople")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllPeople()
        {
            var peopleList = await personService.GetAll();
            if (peopleList.peopleList == null)
            {
                return NotFound($"{peopleList.Response},{peopleList.Response}");
            }
            return Ok(new
            {
                status = peopleList.Status,
                response = peopleList.Response,
                people = peopleList.peopleList,

            });
        }
        [HttpPost]
        [Route("Create", Name = "CreatePerson")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreatePerson(PersonDTO people)
        {
            var response = await new ResultPatern().GiveResponse(200, null);
            if (people == null)
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
            if (string.IsNullOrWhiteSpace(people.Name) || string.IsNullOrWhiteSpace(people.ContactNumber)|| string.IsNullOrWhiteSpace(people.Email)|| 
                string.IsNullOrWhiteSpace(people.Gender) || people.ID == "")
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

            personService.Mode = PersonService.enMode.Add;
            var result = await personService.Save(people.ID,people);

            if (!result.IsSuccess)
            {
                
                    return StatusCode(result.Status, new
                    {
                        status = result.Status,
                        response = result.Response,
                        isSuccess = result.IsSuccess
                    });
                
                
            }
            return Ok(CreatedAtRoute("GetPersonByIdAsync", new { id = people.ID }, people));

        }
        [HttpPut]
        [Route("{ID}", Name = "UpdatePerson")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdatePerson(string ID, PersonDTO people)
        {

            var response = await new ResultPatern().GiveResponse(200, null);
            if ((string.IsNullOrWhiteSpace(people.Name) || string.IsNullOrWhiteSpace(people.ContactNumber) || string.IsNullOrWhiteSpace(people.Email) ||
                string.IsNullOrWhiteSpace(people.Gender) || ID == ""))
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

            var CurrAppointment = await personService.GetByID(ID);
            if (CurrAppointment.personDTO1 == null)
            {

                return NotFound(new
                {
                    status = CurrAppointment.Status,
                    response = CurrAppointment.Response,
                    isSuccess = CurrAppointment.IsSuccess
                });
            }
           

           personService.Mode = PersonService.enMode.Update;
            var result = await personService.Save(ID,people);

            if (!result.IsSuccess){
                
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

        [HttpDelete]
        [Route("Delete/{id}", Name = "DeletePerson")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeletePerson(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest("ID is empty or not found");

            var result = await personService.Delete(id);

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
