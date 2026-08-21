using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSMDataAccess.DTOs
{
    public class EmployeeDTO
    {
        public int ID { get; set; }
        public string PersonID { get; set; } = null!;
        public decimal Salary { get; set; }
        public DateTime HireDate { get; set; }
        public bool IsActive { get; set; }
        public EmployeeDTO()
        {

        }
        public EmployeeDTO(int ID, string PersonID, decimal Salary, DateTime HireDate, bool IsActive)
        {
            this.ID = ID;
            this.PersonID = PersonID;
            this.Salary = Salary;
            this.HireDate = HireDate;
            this.IsActive = IsActive;
        }
    }
}
