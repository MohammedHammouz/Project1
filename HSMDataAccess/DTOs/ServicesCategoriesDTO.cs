using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace HSMDataAccess.DTOs
{
    public class ServicesCategoriesDTO
    {
       public int CategoryID { get; set; }
        public string CategoryName { get; set; } = null!;
        public string? CategoryDescription { get; set; }
        public ServicesCategoriesDTO()
        {

        }
        public ServicesCategoriesDTO(int CategoryID, string CategoryName, string? CategoryDescription)
        {
            this.CategoryID = CategoryID;
            this.CategoryName = CategoryName;
            this.CategoryDescription = CategoryDescription;
        }
    }
}
