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
        //public int EmployeeID { get; set; }
        //public string PersonID { get; set; } = null!;
        //public decimal Salary { get; set; }
        //public DateTime HireDate { get; set; }
        //public bool IsActive { get; set; }
        //public EmployeeDTO employeeDTO { get
        //    {
        //        return new EmployeeDTO(EmployeeID, PersonID, Salary, HireDate, IsActive);
        //    } 
        //}
        //public enum enMode { Add = 0, Update }
        //public enMode Mode = enMode.Add;
        //private readonly EmployeeRepository _employeeRepository;
        //public Employee(EmployeeDTO employeeDTO, EmployeeRepository employeeRepository,enMode mode=enMode.Add)
        //{
        //    this.EmployeeID = employeeDTO.EmployeeID;
        //    this.PersonID = employeeDTO.PersonID;
        //    this.Salary = employeeDTO.Salary;
        //    this.HireDate = employeeDTO.HireDate;
        //    this.IsActive = employeeDTO.IsActive;
        //    _employeeRepository = employeeRepository;
        //    Mode = mode;
        //}
        //private async Task<bool> _AddNew()
        //{
        //    EmployeeEntity employeeEntity=new EmployeeEntity();
        //    employeeEntity.EmployeeID = employeeDTO.EmployeeID;
        //    employeeEntity.PersonID = employeeDTO.PersonID;
        //    employeeEntity.Salary = employeeDTO.Salary;
        //    employeeEntity.HireDate = employeeDTO.HireDate;
        //    employeeEntity.IsActive = employeeDTO.IsActive;
        //    var AddNew = await _employeeRepository.AddAsync(employeeEntity);
        //    return this.EmployeeID != -1;
        //}
        //private async Task<bool> _Update()
        //{
        //    EmployeeEntity CurrentEmployee = new EmployeeEntity();
        //    CurrentEmployee.PersonID = employeeDTO.PersonID;
        //    CurrentEmployee.Salary = employeeDTO.Salary;
        //    CurrentEmployee.IsActive = employeeDTO.IsActive;
        //    CurrentEmployee.HireDate = employeeDTO.HireDate;
        //    return await _employeeRepository.UpdateAsync(CurrentEmployee);
        //}
        //public async Task<bool> Delete(string UserID)
        //{
        //    var user = await _employeeRepository.GetByIDAsync(UserID);
        //    if (user == null)
        //    {
        //        return false;
        //    }
        //    return await _employeeRepository.DeleteAsync(user);
        //}
        //public async Task<EmployeeDTO> GetByID(int ID)
        //{
        //    EmployeeDTO CurrentEmployee = new EmployeeDTO();
           
        //    var employee = await _employeeRepository.GetByID(ID);
        //    if (employee == null)
        //    {
        //        return new EmployeeDTO();
        //    }
        //    CurrentEmployee.EmployeeID = ID;
        //    CurrentEmployee.PersonID = employee.PersonID;
        //    CurrentEmployee.Salary = employee.Salary;
        //    CurrentEmployee.IsActive = employee.IsActive;
        //    CurrentEmployee.HireDate = employee.HireDate;
        //    return CurrentEmployee;
        //}
        //public async Task<List<EmployeeDTO>> GetAll()
        //{
        //    var employees= await _employeeRepository.GetAllAsync();
        //    return employees.Select(
        //        e =>
        //        new EmployeeDTO(e.EmployeeID, e.PersonID, e.Salary, e.HireDate, e.IsActive)
        //        ).ToList();
        //}
        //public async Task<bool> Save()
        //{
        //    switch (Mode)
        //    {
        //        case enMode.Update:
        //            return await _Update();
        //        case enMode.Add:
        //            if (await _AddNew())
        //            {

        //                Mode = enMode.Update;
        //                return true;
        //            }

        //            else
        //                return false;
        //    }
        //    return false;
        //}
    }
}

