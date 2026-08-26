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
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        //private readonly PersonService _person;
        //public PersonController(PersonService person)
        //{
        //    _person = person;
        //}
        //[HttpGet("AllPeople", Name = "GetAllPeople")]

        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public async Task<ActionResult<PersonDTO>> GetAllStudents()
        //{
        //    var people =await _person.GetAll();
        //    if (people == null)
        //    {
        //        return NotFound("");
        //    }
        //    return Ok(people);
        //}
        //[HttpGet("{ID}", Name = "GetPersonByID")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public async Task<ActionResult<PersonDTO>> GetPersonByID(string ID)
        //{
        //    if (ID=="")
        //        return BadRequest($"Not accepted id {ID}");
        //    var person =await _person.GetByID(ID);
        //    if (person == null)
        //    {
        //        return NotFound("");
        //    }
            
        //    return Ok(person);
        //}
        
        //[HttpPost("AddNew",Name = "AddNewPerson")]
        //[ProducesResponseType(StatusCodes.Status201Created)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public async Task<ActionResult<PersonDTO>> AddNewPerson(PersonDTO newPerson)
        //{
        //    if (string.IsNullOrEmpty(newPerson.Name) || string.IsNullOrEmpty(newPerson.ContactNumber)||
        //        string.IsNullOrEmpty(newPerson.Email)|| string.IsNullOrEmpty(newPerson.Gender)|| 
        //         newPerson.DateOfBirth==null)
        //    {
        //        return BadRequest("Invalid Person data.");
        //    }
        //    else
        //    {   
        //        PersonService person = new PersonService(_person.personRepository1);
        //        person.Name = newPerson.Name;
        //        person.Address = newPerson.Address;
        //        person.DateOfBirth = newPerson.DateOfBirth;
        //        person.Email = newPerson.Email;
        //        person.Gender = newPerson.Gender;
        //        person.ContactNumber = newPerson.ContactNumber;
        //        await person.Save();
        //        return CreatedAtRoute("GetPersonByID", new { id = newPerson.ID }, newPerson);
        //    }

        //}
        //[HttpPut("{ID}", Name = "UpdatePerson")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public async Task<ActionResult<PersonDTO>> UpdatePerson(string ID, PersonDTO updatedPerson)
        //{
        //    if (string.IsNullOrEmpty(updatedPerson.Name) || string.IsNullOrEmpty(updatedPerson.ContactNumber) ||
        //        string.IsNullOrEmpty(updatedPerson.Email) || string.IsNullOrEmpty(updatedPerson.Gender) ||
        //         updatedPerson.DateOfBirth == null)
        //    {
        //        return BadRequest("");
        //    }
        //    //var student = StudentDataSimulation.students.FirstOrDefault(student => student.studentID==id);
        //    PersonService person = new PersonService(_person.personRepository1,PersonService.enMode.Update);
        //    PersonDTO person1 =await _person.GetByID(ID);
        //    if (person1 == null)
        //    {
        //        return NotFound("Person is not found");
        //    }
        //    person.ID = ID;
        //    person.Name = updatedPerson.Name;
        //    person.Address = updatedPerson.Address;
        //    person.DateOfBirth = updatedPerson.DateOfBirth;
        //    person.Email = updatedPerson.Email;
        //    person.Gender = updatedPerson.Gender;
        //    person.ContactNumber = updatedPerson.ContactNumber;
            
        //    await person.Save();
           
        //    return Ok(person.personDTO);
        //}
        //[HttpDelete("DeletePerson/{ID}", Name = "DeletePerson")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public async Task<ActionResult> DeletePerson(string ID)
        //{
        //    if (ID == "")
        //    {
        //        return NotFound("Personid is not found");
        //    }
        //    bool person =await _person.Delete(ID);
        //    if (!person)
        //    {
        //        return BadRequest("Person is not found");
        //    }
        //    return Ok($"Person with ID {ID} has been deleted.");
        //}
    }
}
