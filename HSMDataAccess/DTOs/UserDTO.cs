using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSMDataAccess.DTOs
{
    public class UserDTO
    {
        public string UserID { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Role { get; set; } = null!;
        public bool Status { get; set; }
        public string PasswordHash { get; set; }
        
        public string EmployeeID { get; set; }
        public UserDTO()
        {

        }
        public UserDTO(string UserID, string Name, string Role,bool Status,
            string PasswordHash,string EmployeeID)
        {
            this.UserID = UserID;
            this.Name = Name;
            this.Role = Role;
            this.Status = Status;
            this.EmployeeID = EmployeeID;
            this.PasswordHash = PasswordHash;
            
        }
    }
}
