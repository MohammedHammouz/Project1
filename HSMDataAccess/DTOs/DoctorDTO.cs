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
        // Properties
        public string Specialization { get; set; } = null!;
        public bool Status { get; set; };
        public string UserID { get; set; }
        public DoctorDTO(string id,string departmentID,string specialization,
        bool status,string UserID)
        {
            ID = id;
            DepartmentID = departmentID;
            Specialization = specialization;
            Status = status;
            this.UserID = UserID;
        }
    }
}
