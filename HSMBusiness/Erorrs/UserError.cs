using HSMBusiness.Services;
using HSMDataAccess.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSMBusiness.Erorrs
{
    public class UserError
    {
        private readonly PersonService? _personService;

        public UserError(PersonService personService)
        {
            _personService = personService;
        }
        public async Task<Dictionary<string, string>> ValidateUser(UserDTO dto) { 
            var errors = new Dictionary<string, string>(); 
            if (string.IsNullOrWhiteSpace(dto.UserID)) { 
                errors["ID"] = "ID is required."; 
            } 
            if (string.IsNullOrWhiteSpace(dto.Name)) { 
                errors["Name"] = "Name is required."; 
            } 
            if (string.IsNullOrWhiteSpace(dto.Role)) { 
                errors["Role"] = "Role is required."; 
            }
            else
            {
                string[] allowedRoles =
                      {
                "Admin",
                "Doctor",
                "Nurse",
                "User"
            };

                if (!allowedRoles.Contains(dto.Role) )
                {
                    errors["Role"] =
                        "Role must be Admin,or Doctor";
                }
            }
            if (string.IsNullOrWhiteSpace(dto.PasswordHash)) { 
                errors["PasswordHash"] = "PasswordHash is required."; 
            } 
            if (string.IsNullOrWhiteSpace(dto.PersonID) ) { 
                errors["PersonID"] = "PersonID is required."; 
            }
            else
            {
               
                var person = await _personService.GetByID(dto.PersonID);

                if (person.Item3 == null)
                {
                    errors["PersonID"] = "Person does not exist.";
                }
            }
            return errors; }
    }
}
