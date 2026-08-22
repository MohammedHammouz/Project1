using HSMBusiness.Mappers;
using HSMDataAccess.DTOs;
using HSMDataAccess.Entities;
using HSMDataAccess.RepositoryServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSMBusiness.Services
{
    public class EmployeeService
    {
        //public int ID { get; set; }
        //public string PersonID { get; set; } = null!;
        //public decimal Salary { get; set; }
        //public DateTime HireDate { get; set; }
        //public bool IsActive { get; set; }
        //private EmployeeDTO _employeeDTO;
        
        public enum enMode { Add = 0, Update }
        public enMode Mode = enMode.Add;
        private readonly EmployeeRepository _employeeRepository;
        public EmployeeRepository employeeRepository
        {
            get
            {
                return _employeeRepository;
            }
        }
        public EmployeeService( EmployeeRepository employeeRepository, enMode mode = enMode.Add)
        {
            _employeeRepository = employeeRepository;
            Mode = mode;
        }
        
        private async Task<bool> _AddNew(EmployeeDTO employeeDTO)
        {
            var employeeEntity = new EmployeeMapper().ToEntity(employeeDTO);
            var AddNew = await _employeeRepository.AddAsync(employeeEntity);
            employeeEntity.ID = AddNew.ID;
            return employeeEntity.ID != -1;
        }
        private async Task<bool> _Update(int ID)
        {
            var employee = await _employeeRepository.GetByID(ID);

            if (employee == null)
                return false;
            return await _employeeRepository.UpdateAsync(employee);
        }
        public async Task<bool> Delete(int ID)
        {
            var employee = await _employeeRepository.GetByID(ID);
            if (employee == null)
            {
                return false;
            }
            return await _employeeRepository.DeleteAsync(employee);
        }
        public async Task<EmployeeDTO> GetByID(int ID)
        {
            var employee = await _employeeRepository.GetByID(ID);
            if (employee == null)
            {
                return new EmployeeDTO();
            }
            
            return new EmployeeDTO(ID, employee.PersonID, employee.Salary,employee.HireDate,employee.IsActive);
        }
       
        public async Task<List<EmployeeDTO>> GetAll()
        {
            var employees = await _employeeRepository.GetAllAsync();
            if (employees == null)
            {
                return null;
            }
            return employees.Select(
                e =>
                new EmployeeDTO(e.ID, e.PersonID, e.Salary, e.HireDate, e.IsActive)
                ).ToList();
        }
        public async Task<bool> Save(EmployeeDTO employeeDTO=null,int ID=-1)
        {
            switch (Mode)
            {
                case enMode.Update:
                    return await _Update(ID);
                case enMode.Add:
                    if (await _AddNew(employeeDTO))
                    {

                        Mode = enMode.Update;
                        return true;
                    }

                    else
                        return false;
            }
            return false;
        }
    }
}

