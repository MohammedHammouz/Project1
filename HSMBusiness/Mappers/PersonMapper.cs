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

        public static PersonDTO ToDTO(Person person)
        {
            return new PersonDTO(person.ID, person.Name, person.ContactNumber, person.Email, person.Gender,person.Address,person.DateOfBirth);
        }
        public static PersonEntity ToEntity(PersonDTO personDTO)
        {
           
            return new PersonEntity
            {
                ID = Guid.NewGuid().ToString("N").Substring(0, 10),
                Name = personDTO.Name,
                ContactNumber = personDTO.ContactNumber,
                Email = personDTO.Email,
                Gender = personDTO.Gender,
                Address=personDTO.Address,
                DateOfBirth=personDTO.DateOfBirth
            };
        }
        public static void FromDTO(PersonDTO dto, Person person)
        {
            dto.ID = person.ID;
            dto.Name = person.Name;
            dto.ContactNumber = person.ContactNumber;
            dto.Email = person.Email;
            dto.Gender = person.Gender;
            dto.Address = person.Address;
            dto.DateOfBirth = person.DateOfBirth;
        }
    }
}
