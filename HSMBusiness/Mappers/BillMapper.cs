using HSMDataAccess.DTOs;
using HSMDataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSMBusiness.Mappers
{
    public class BillMapper
    {
        public enum enMode { Add,Update}
        public Bill ToEntity(BillDTO billDTO,enMode mode=enMode.Add)
        {
            if(mode==enMode.Add)
                return new Bill
                {
                    ID = billDTO.BillID,
                    PatientID=billDTO.PatientID,
                    Status =billDTO.Status,
                    Date=billDTO.Date,
                    PartialPaymentAmount=billDTO.PartialPaymentAmount,
                    DueDate=billDTO.DueDate,
                    Invoice= billDTO.Invoice,
                    GrossAmount = billDTO.GrossAmount,
                    Discount = billDTO.Discount,
                    InsuranceCoverage = billDTO.InsuranceCoverage,
                    PatientResponsibility = billDTO.PatientResponsibility,
              
                };
            else
            {
                return new Bill
                {
                    PatientID = billDTO.PatientID,
                    Status = billDTO.Status,
                    Date = billDTO.Date,
                    PartialPaymentAmount = billDTO.PartialPaymentAmount,
                    DueDate = billDTO.DueDate,
                    Invoice = billDTO.Invoice,
                    GrossAmount = billDTO.GrossAmount,
                    Discount = billDTO.Discount,
                    InsuranceCoverage = billDTO.InsuranceCoverage,
                    PatientResponsibility = billDTO.PatientResponsibility,

                };
            }
        }
        public BillDTO ToDTO(Bill bill)
        {
            return new BillDTO
            (
                bill.ID,
               bill.PatientID,
                bill.Status,
                bill.Date,
                bill.PartialPaymentAmount,
                bill.DueDate,
                bill.Invoice,
                bill.GrossAmount,
                bill.Discount,
               bill.InsuranceCoverage,
                 bill.PatientResponsibility

            );
        }
    }
}
