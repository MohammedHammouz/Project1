using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSMDataAccess.Entities
{
    public class Doctor
    {
        public string ID { get; set; } = null!;
        
        public string DepartmentID { get; set; } = null!;
        public string Specialization { get; set; } = null!;
        public bool Status { get; set; }
        public string? UserID { get; set; }
        
       

        // Navigation Properties
        public virtual Department Department { get; set; } = null!;
        
        public virtual  User User { get; set; } = null!;
      

    }
}
