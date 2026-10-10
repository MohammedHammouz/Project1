using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSMDataAccess.Entities
{
    public class ServiceCategories
    {
        public string ID { get; set; } = null!;
        public string CategoryName { get; set; } = null!;
        public string? CategoryDescription { get; set; }
        public virtual ICollection<MedicalServices> medicalService { get; set; }
        = new List<MedicalServices>();
    }
}
