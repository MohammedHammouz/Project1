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
        // Foreign Keys
        public string DepartmentID { get; set; } = null!;
        public string? UserID { get; set; }
        // Properties
        public string Specialization { get; set; } = null!;
        public bool Status { get; set; }

        // Navigation Properties
        public virtual DepartmentEntity Department { get; set; } = null!;
        
        public virtual  ICollection <UserEntity> User { get; set; } = new List<UserEntity>();
      

    }
}
