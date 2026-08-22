using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace HSMDataAccess.Entities
{
    public class Person
    {
        public string ID { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string ContactNumber { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public string Address { get; set; } = null!;
        public DateOnly DateOfBirth{ get; set; }
        public virtual Employee Employee { get; set; } = null!;
        public virtual Patient Patient { get; set; } = null!;
    }
}
