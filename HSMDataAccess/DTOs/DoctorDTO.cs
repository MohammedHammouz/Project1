using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSMDataAccess.DTOs
{
    public class DoctorDTO
    {
        public string ID { get; set; } = null!;

        // Foreign Keys
        public string DepartmentID { get; set; } = null!;
        public string CreatedBy { get; set; } = null!;
        public string? UpdatedBy { get; set; }

        // Properties
        public string Specialization { get; set; } = null!;
        public string Status { get; set; } = null!;
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        
        public string UserID { get; set; }
        public DoctorDTO(string id,string departmentID,
        string createdBy,string? updatedBy,string specialization,
        string status,DateTime createdOn,DateTime? updatedOn,string UserID)
        {
            ID = id;
            DepartmentID = departmentID;
           
            CreatedBy = createdBy;
            UpdatedBy = updatedBy;
            Specialization = specialization;
            Status = status;
            CreatedOn = createdOn;
            UpdatedOn = updatedOn;
           
            this.UserID = UserID;
        }
    }
}
