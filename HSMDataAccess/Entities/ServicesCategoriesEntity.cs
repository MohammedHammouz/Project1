using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSMDataAccess.Entities
{
    public class ServicesCategoriesEntity
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; } = null!;
        public string? CategoryDescription { get; set; }
    }
}
