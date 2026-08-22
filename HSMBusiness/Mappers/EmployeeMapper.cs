using HSMBusiness.Services;
using HSMDataAccess.DTOs;
using HSMDataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSMBusiness.Mappers
{
    public class EmployeeMapper
    {
        public  EmployeeDTO ToDTO(Employee employee)
        {
            return new EmployeeDTO(employee.ID, employee.PersonID, employee.Salary, employee.HireDate, employee.IsActive);
        }
        public  Employee ToEntity(EmployeeDTO employeeDTO)
        {
            return new Employee
            {
                ID = employeeDTO.ID,
                PersonID = employeeDTO.PersonID,
                Salary = employeeDTO.Salary,
                HireDate = employeeDTO.HireDate,
                IsActive = employeeDTO.IsActive
            };
        }
        
    }
}
