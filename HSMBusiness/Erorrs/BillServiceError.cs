using HSMDataAccess.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSMBusiness.Erorrs
{
    public class BillServiceError
    {
        public Dictionary<string, string> ValidateBillService(
    BillServiceDTO dto)
        {
            var errors = new Dictionary<string, string>();
            if (string.IsNullOrWhiteSpace(dto.ServiceID))
            {
                errors["ServiceID"] = "ServiceID is required.";
            }
            if (string.IsNullOrWhiteSpace(dto.BillID))
            {
                errors["BillID"] = "BillID is required.";
            }
            if (dto.Quantity == null)
            {
                errors["Quantity"] = "Quantity is required.";
            }
            else if (dto.Quantity <= 0)
            {
                errors["Quantity"] = "Quantity must be greater than zero.";
            }
            if (dto.UnitPrice < 0)
            {
                errors["UnitPrice"] = "UnitPrice cannot be negative.";
            }
            if (dto.TotalPrice < 0)
            {
                errors["TotalPrice"] = "TotalPrice cannot be negative.";
            }
            if (dto.Quantity.HasValue && dto.UnitPrice >= 0)
            {
                decimal calculatedTotal =
                    dto.Quantity.Value * dto.UnitPrice;

                if (dto.TotalPrice.HasValue &&
                    dto.TotalPrice.Value != calculatedTotal)
                {
                    errors["TotalPrice"] =
                        "TotalPrice must equal Quantity multiplied by UnitPrice.";
                }
            }
            return errors;
        }
    }
}
