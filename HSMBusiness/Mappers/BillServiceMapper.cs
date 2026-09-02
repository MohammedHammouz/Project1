using HSMDataAccess.DTOs;
using HSMDataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSMBusiness.Mappers
{
    public class BillServiceMapper
    {
        public enum enMode { Add, Update }

        public BillService ToEntity(
            BillServiceDTO billServiceDTO,
            enMode mode = enMode.Add)
        {
            if (mode == enMode.Add)
            {
                return new BillService
                {
                    ServiceID = billServiceDTO.ServiceID,
                    BillID = billServiceDTO.BillID,
                    Quantity = billServiceDTO.Quantity,
                    UnitPrice = billServiceDTO.UnitPrice,
                    TotalPrice = billServiceDTO.TotalPrice
                };
            }
            else
            {
                return new BillService
                {
                    ServiceID = billServiceDTO.ServiceID,
                    BillID = billServiceDTO.BillID,
                    Quantity = billServiceDTO.Quantity,
                    UnitPrice = billServiceDTO.UnitPrice,
                    TotalPrice = billServiceDTO.TotalPrice
                };
            }
        }

        public BillServiceDTO ToDTO(BillService billService)
        {
            return new BillServiceDTO
            (
                billService.ServiceID,
                billService.BillID,
                billService.Quantity,
                billService.UnitPrice,
                billService.TotalPrice
            );
        }
    }
}
