using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSMDataAccess.DTOs
{
    public class BillServiceDTO
    {
        public string ServiceID { get; set; } = null!;
        public string BillID { get; set; } = null!;
        public int? Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal? TotalPrice { get; set; }
        public BillServiceDTO()
        {
        }

        public BillServiceDTO(
            string serviceID,
            string billID,
            int? quantity,
            decimal unitPrice,
            decimal? totalPrice)
        {
            ServiceID = serviceID;
            BillID = billID;
            Quantity = quantity;
            UnitPrice = unitPrice;
            TotalPrice = totalPrice;
        }
    }
}
