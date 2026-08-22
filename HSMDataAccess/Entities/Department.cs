using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSMDataAccess.Entities
{
    public class Department
    {
        public string ID { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? HeadOf { get; set; }
        public virtual Doctor? Doctor { get; set; }
    }
}
