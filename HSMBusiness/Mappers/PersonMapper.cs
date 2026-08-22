using HSMDataAccess.DTOs;
using HSMDataAccess.Entities;
using HSMDataAccess.RepositoryServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HSMBusiness.dto
{

    public class PersonMapper
    {

        public  PersonDTO ToDTO(HSMDataAccess.Entities.Person person)
        {
            return new PersonDTO(person.ID, person.Name,
                person.ContactNumber, person.Email,
                person.Gender, person.Address, person.DateOfBirth);
        }
        public  Person ToEntity(PersonDTO personDTO)
        {
           
            return new HSMDataAccess.Entities.Person
            {
                ID = Guid.NewGuid().ToString("N").Substring(0, 10),
                Name = personDTO.Name,
                ContactNumber = personDTO.ContactNumber,
                Email = personDTO.Email,
                Gender = personDTO.Gender,
                Address= personDTO.Address,
                DateOfBirth= personDTO.DateOfBirth
            };
        }
       
    }
}
