using HSMBusiness.Mappers;
using HSMDataAccess.DTOs;
using HSMDataAccess.Entities;
using HSMDataAccess.RepositoryServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSMBusiness
{
    public class Employee
    {
        public int ID { get; set; }
        public string PersonID { get; set; } = null!;
        public decimal Salary { get; set; }
        public DateTime HireDate { get; set; }
        public bool IsActive { get; set; }
        private EmployeeDTO _employeeDTO;
        public EmployeeDTO employeeDTO
        {
            get
            {
                return  EmployeeMapper.ToDTO(this);
            }
            set
            {
               
                EmployeeMapper.FromDTO(value, this);
            }
        }
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
        public Employee( EmployeeRepository employeeRepository, enMode mode = enMode.Add)
        {
            _employeeRepository = employeeRepository;
            Mode = mode;
        }
        
        private async Task<bool> _AddNew()
        {
            EmployeeEntity employeeEntity = EmployeeMapper.ToEntity(employeeDTO);
            var AddNew = await _employeeRepository.AddAsync(employeeEntity);
            ID = AddNew.ID;
            employeeDTO.ID = AddNew.ID;
            return this.ID != -1;
        }
        private async Task<bool> _Update()
        {
            EmployeeEntity CurrentEmployee = new EmployeeEntity();
            CurrentEmployee.PersonID = employeeDTO.PersonID;
            CurrentEmployee.Salary = employeeDTO.Salary;
            CurrentEmployee.IsActive = employeeDTO.IsActive;
            CurrentEmployee.HireDate = employeeDTO.HireDate;

            return await _employeeRepository.UpdateAsync(CurrentEmployee);
        }
        public async Task<bool> Delete(string UserID)
        {
            var user = await _employeeRepository.GetByIDAsync(UserID);
            if (user == null)
            {
                return false;
            }
            return await _employeeRepository.DeleteAsync(user);
        }
        public async Task<EmployeeDTO> GetByID(int ID)
        {
            EmployeeDTO CurrentEmployee = new EmployeeDTO();

            var employee = await _employeeRepository.GetByID(ID);
            if (employee == null)
            {
                return new EmployeeDTO();
            }
            CurrentEmployee.ID = ID;
            CurrentEmployee.PersonID = employee.PersonID;
            CurrentEmployee.Salary = employee.Salary;
            CurrentEmployee.IsActive = employee.IsActive;
            CurrentEmployee.HireDate = employee.HireDate;
            return CurrentEmployee;
        }
        public async Task<List<EmployeeDTO>> GetAll()
        {
            var employees = await _employeeRepository.GetAllAsync();
            return employees.Select(
                e =>
                new EmployeeDTO(e.ID, e.PersonID, e.Salary, e.HireDate, e.IsActive)
                ).ToList();
        }
        public async Task<bool> Save()
        {
            switch (Mode)
            {
                case enMode.Update:
                    return await _Update();
                case enMode.Add:
                    if (await _AddNew())
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

