using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace HSMDataAccess.Entities
{
    public class Patient
    {
        public string ID { get; set; } = null!;
        public string MedicalHistory { get; set; } = null!;
        public bool Status { get; set; } = false;
        public string PersonID { get; set; } = null!;
        public virtual Person Person { get; set; } = null!;
    }
}
