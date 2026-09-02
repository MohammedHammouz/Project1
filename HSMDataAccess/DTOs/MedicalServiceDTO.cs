using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSMDataAccess.DTOs
{
    public class MedicalServiceDTO
    {
        public string ID { get; set; } = null!;
        public string ServiceName { get; set; } = null!;
        public string CategoryID { get; set; } = null!;
        public decimal DefaultPrice { get; set; }
        public string? Description { get; set; }
        public int? DurationMinutes { get; set; }
        public MedicalServiceDTO()
        {

        }
        public MedicalServiceDTO(string id,string serviceName,string categoryID,decimal defaultPrice,
      string? description,int? durationMinutes)
        {
            ID = id;
            ServiceName = serviceName;
            CategoryID = categoryID;
            DefaultPrice = defaultPrice;
            Description = description;
            DurationMinutes = durationMinutes;
        }
    }
}
