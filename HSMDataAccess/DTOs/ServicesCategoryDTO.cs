using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace HSMDataAccess.DTOs
{
    public class ServicesCategoryDTO
    {
       public string CategoryID { get; set; }
        public string CategoryName { get; set; } = null!;
        public string? CategoryDescription { get; set; }
        public ServicesCategoryDTO()
        {

        }
        public ServicesCategoryDTO(string CategoryID, string CategoryName, string? CategoryDescription)
        {
            this.CategoryID = CategoryID;
            this.CategoryName = CategoryName;
            this.CategoryDescription = CategoryDescription;
        }
    }
}
