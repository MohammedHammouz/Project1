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
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public DateTime? UpdatedOn { get; set; } = null;
        public string PasswordHash { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public int EmployeeID { get; set; }
        public UserDTO()
        {

        }
        public UserDTO(string UserID, string Name, string Role,bool Status, 
            DateTime CreatedOn, DateTime UpdatedOn,
            string PasswordHash,string CreatedBy,string UpdatedBy)
        {
            this.UserID = UserID;
            this.Name = Name;
            this.Role = Role;
            this.Status = Status;
            this.CreatedOn = CreatedOn;
            this.UpdatedOn = UpdatedOn;
            this.PasswordHash = PasswordHash;
            this.CreatedBy = CreatedBy;
            this.UpdatedBy = UpdatedBy;
        }
    }
}
