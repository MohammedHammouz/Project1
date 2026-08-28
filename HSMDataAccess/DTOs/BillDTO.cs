using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSMDataAccess.DTOs
{
    public class BillDTO
    {
        public string BillID { get; set; } = null!;
        public string PatientID { get; set; } = null!;
        public string Status { get; set; } = null!;
        public DateTime? Date { get; set; }
        public decimal? PartialPaymentAmount { get; set; }
        public DateTime? DueDate { get; set; }
        public string Invoice { get; set; } = null!;
        public decimal GrossAmount { get; set; }
        public decimal? Discount { get; set; }
        public decimal? InsuranceCoverage { get; set; }
        public decimal PatientResponsibility { get; set; }
        public BillDTO()
        {

        }
        public BillDTO(string billID,string patientID,string status,DateTime? date,
    decimal? partialPaymentAmount,DateTime? dueDate,string invoice,decimal grossAmount,
    decimal? discount,decimal? insuranceCoverage,decimal patientResponsibility)
        {
            BillID = billID;
            PatientID = patientID;
            Status = status;
            Date = date;
            PartialPaymentAmount = partialPaymentAmount;
            DueDate = dueDate;
            Invoice = invoice;
            GrossAmount = grossAmount;
            Discount = discount;
            InsuranceCoverage = insuranceCoverage;
            PatientResponsibility = patientResponsibility;
        }
    }
}