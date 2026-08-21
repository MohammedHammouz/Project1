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
        public static EmployeeDTO ToDTO(Employee employee)
        {
            return new EmployeeDTO(employee.ID, employee.PersonID, employee.Salary, employee.HireDate, employee.IsActive);
        }
        public static EmployeeEntity ToEntity(EmployeeDTO employeeDTO)
        {
            return new EmployeeEntity
            {
                ID = employeeDTO.ID,
                PersonID = employeeDTO.PersonID,
                Salary = employeeDTO.Salary,
                HireDate = employeeDTO.HireDate,
                IsActive = employeeDTO.IsActive
            };
        }
        public static void FromDTO(EmployeeDTO dto, Employee employee)
        {
            employee.ID = dto.ID;
            employee.PersonID = dto.PersonID;
            employee.Salary = dto.Salary;
            employee.HireDate = dto.HireDate;
            employee.IsActive = dto.IsActive;
        }
    }
}
