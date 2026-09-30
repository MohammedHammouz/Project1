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
        private readonly EmployeeService? _employeeService;
        
        //public UserError(EmployeeService employeeService)
        //{
        //    _employeeService = employeeService;
        //}
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
            if (string.IsNullOrWhiteSpace(dto.EmployeeID) ) { 
                errors["EmployeeID"] = "EmployeeID is required."; 
            }
            else
            {
               
                var employee = await _employeeService.GetByID(dto.EmployeeID);

                if (employee.Item3 == null)
                {
                    errors["EmployeeID"] = "Employee does not exist.";
                }
            }
            return errors; }
    }
}
