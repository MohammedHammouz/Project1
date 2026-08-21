using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSMDataAccess.Entities
{
    public class DoctorEntity
    {
        public string ID { get; set; } = null!;
        
        public string DepartmentID { get; set; } = null!;
        public string Specialization { get; set; } = null!;
        public bool Status { get; set; }
        public string? UserID { get; set; }
        
       

        // Navigation Properties
        public virtual DepartmentEntity Department { get; set; } = null!;
        
        public virtual  UserEntity User { get; set; } = null!;
      

    }
}
