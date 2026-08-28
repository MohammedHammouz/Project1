using HSMBusiness.Mappers;
using HSMBusiness.Pattern;
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
        ResultPatern resultPattern = new ResultPatern();
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
            return employeeEntity.ID != "";
        }
        private async Task<(int,string?,bool)> _Update(int ID,EmployeeDTO employeeDTO)
        {
            var employee = await _employeeRepository.GetByID(ID);
            var response = await resultPattern.GiveResponse(200);
            if (employee == null){
                response = await resultPattern.GiveResponse(404);
                return (response.Status, response.Response, response.IsSuccess);
            }
            employee = new EmployeeMapper().ToEntity(employeeDTO,EmployeeMapper.enMode.Update,employee);
            return (response.Status, response.Response, await _employeeRepository.UpdateAsync(employee));
        }
        public async Task<(int,string?,bool)> Delete(int ID)
        {
            var employee = await _employeeRepository.GetByID(ID);
            var response = await resultPattern.GiveResponse(200);
            if (employee == null)
            {
                response = await resultPattern.GiveResponse(404);
                return (response.Status,response.Response,response.IsSuccess);
            }
            bool IsDeleted= await _employeeRepository.DeleteAsync(employee);
            if (!IsDeleted)
            {
                response = await resultPattern.GiveResponse(500);
                return (response.Status, response.Response, response.IsSuccess);
            }
            return (response.Status, response.Response, response.IsSuccess);
        }
        public async Task<(int, string? ,EmployeeDTO)> GetByID(string ID)
        {
           
            var employee = await _employeeRepository.GetByIDAsync(ID);
            var response = await resultPattern.GiveResponse(200);
            if (employee == null)
            {
                response = await resultPattern.GiveResponse(404);
                return (response.Status, response.Response, new EmployeeDTO());
            }
            
            return (response.Status, response.Response, new EmployeeDTO(ID, employee.PersonID, employee.Salary,employee.HireDate,employee.IsActive));
        }
       
        public async Task<(int, string?, List<EmployeeDTO>)> GetAll()
        {
            var employees = await _employeeRepository.GetAllAsync();
            var response = await resultPattern.GiveResponse(200);
            if (employees == null)
            {
                response = await resultPattern.GiveResponse(404);
                return (response.Status, response.Response, null);
            }
            return (response.Status, response.Response, employees.Select(
                e =>
                new EmployeeMapper().ToDTO(e)).ToList());
        }
        public async Task<(int, string?, bool)> Save(EmployeeDTO employeeDTO=null,int ID=-1)
        {
            var response = await resultPattern.GiveResponse(200);
            switch (Mode)
            {
                case enMode.Update:
                    return await _Update(ID,employeeDTO);
                case enMode.Add:
                    if (await _AddNew(employeeDTO))
                    {

                        Mode = enMode.Update;
                        return (response.Status, response.Response, response.IsSuccess);
                    }

                    else{
                        response = await resultPattern.GiveResponse(500);
                        return (response.Status, response.Response, response.IsSuccess);
                    }
            }
            response = await resultPattern.GiveResponse(500);
            return (response.Status, response.Response, response.IsSuccess);
        }
    }
}

