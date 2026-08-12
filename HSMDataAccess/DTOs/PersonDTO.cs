using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSMDataAccess.DTOs
{
    public class PersonDTO
    {
        public string ID { get; set; } = null!;
        public string? Name { get; set; }
        public string? ContactNumber { get; set; }
        public string? Email { get; set; }
        public string? Gender { get; set; }
        public string? Address { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public PersonDTO(string id,string? name,string? contactNumber,
        string? email,string? gender,string? address,DateOnly dateOfBirth)
        {
            ID = id;
            Name = name;
            ContactNumber = contactNumber;
            Email = email;
            Gender = gender;
            Address = address;
            DateOfBirth = dateOfBirth;
        }
    }
}
