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

        public string CreatedBy { get; set; } = null!;
        public string? UpdatedBy { get; set; }
        public string UserID { get; set; } = null!;
        // Properties
        public string Specialization { get; set; } = null!;
        public string Status { get; set; } = null!;
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        

        // Navigation Properties
        public virtual PersonEntity People { get; set; } = null!;
        public virtual DepartmentEntity Department { get; set; } = null!;
        public virtual EmployeeEntity Employee { get; set; } = null!;
        public virtual UserEntity UserCreate { get; set; } = null!;
        public virtual UserEntity? UserUpdate { get; set; }

    }
}
