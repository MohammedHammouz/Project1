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
        public enum enMode { Add, Update }
        public  EmployeeDTO ToDTO(Employee employee)
        {
            return new EmployeeDTO(employee.ID, employee.PersonID, employee.Salary, employee.HireDate, employee.IsActive);
        }
        public  Employee ToEntity(EmployeeDTO employeeDTO,enMode mode =enMode.Add,Employee employee=null)
        {
            
            if (mode == enMode.Add)
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
            else
            {


                employee.PersonID = employeeDTO.PersonID;
                employee.Salary = employeeDTO.Salary;
                employee.HireDate = employeeDTO.HireDate;
                employee.IsActive = employeeDTO.IsActive;
                return employee;
            }
        }
        
    }
}
