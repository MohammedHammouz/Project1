using HSMDataAccess.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSMBusiness.dto
{

    public class PersonDto
    {
        
        public string Name { get; set; } = null!;
        public string ContactNumber { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public string? Address { get; set; }
        public DateOnly DateOfBirth { get; set; }
        
        
        public PersonDto(string name, string contactNumber,
        string email, string gender, string address, DateOnly dateOfBirth)
        {
           
            Name = name;
            ContactNumber = contactNumber;
            Email = email;
            Gender = gender;
            Address = address;
            DateOfBirth = dateOfBirth;
        }
    }
}
