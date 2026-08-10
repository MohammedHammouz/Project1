using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSMDataAccess.Entities
{
    public class DepartmentEntity
    {
        public virtual DoctorEntity Doctor { get; set; }
    }
}
