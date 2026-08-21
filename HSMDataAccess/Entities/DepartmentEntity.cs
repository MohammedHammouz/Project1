using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSMDataAccess.Entities
{
    public class DepartmentEntity
    {
        public string ID { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? HeadOf { get; set; }
        public virtual DoctorEntity? Doctor { get; set; }
    }
}
