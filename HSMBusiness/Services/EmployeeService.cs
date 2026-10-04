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
        private readonly PersonService _personService;
        public EmployeeRepository employeeRepository
        {
            get
            {
                return _employeeRepository;
            }
        }
       
        public EmployeeService( EmployeeRepository employeeRepository,PersonService personService, enMode mode = enMode.Add)
        {
            _employeeRepository = employeeRepository;
            _personService = personService;
            Mode = mode;
        }
        public async Task<bool> CountNumberOfPersonIDIsOne(string PersonID)
        {
            int result =await _employeeRepository.CountNumberOfPersonIDIsOne(PersonID);
            return result == 1;
        }
        private async Task<(int Status,bool IsSuccess)> _AddNew(EmployeeDTO employeeDTO)
        {
            var employeeEntity = new EmployeeMapper().ToEntity(employeeDTO);
            if (!await _personService.IsPersonExist(employeeEntity.PersonID))
            {
                return (500, false);
            }
            if (await CountNumberOfPersonIDIsOne(employeeDTO.PersonID))
            {
                return (500, false);
            }
            
            var AddNew = await _employeeRepository.AddAsync(employeeEntity);
            employeeEntity.ID = AddNew.ID;
            if(employeeEntity.ID == "")
            {
                return (500, false);
            }
            return (200, false);
        }
        private async Task<(int,string?,bool)> _Update(string ID,EmployeeDTO employeeDTO)
        {
            var employee = await _employeeRepository.GetByIDAsync(ID);
            var response = await resultPattern.GiveResponse(200, new Dictionary<string, string>());
            if (employee == null){
                response = await resultPattern.GiveResponse(404, new Dictionary<string, string>());
                return (response.Status, response.Response, response.IsSuccess);
            }
            if (!await _personService.IsPersonExist(employee.PersonID))
            {
                response = await resultPattern.GiveResponse(500, new Dictionary<string, string>());
                return (response.Status, response.Response, response.IsSuccess);
            }
            
            employee = new EmployeeMapper().ToEntity(employeeDTO,EmployeeMapper.enMode.Update,employee);
            await _employeeRepository.UpdateAsync(employee);
            
            return (response.Status, response.Response, response.IsSuccess);
        }
        public async Task<(int Status,string? Response,bool IsSuccess)> Delete(string ID)
        {
            var employee = await _employeeRepository.GetByIDAsync(ID);
            var response = await resultPattern.GiveResponse(200, new Dictionary<string, string>());
            if (employee == null)
            {
                response = await resultPattern.GiveResponse(404, new Dictionary<string, string>());
                return (response.Status,response.Response,response.IsSuccess);
            }
            bool IsDeleted= await _employeeRepository.DeleteAsync(employee);
            if (!IsDeleted)
            {
                response = await resultPattern.GiveResponse(500, new Dictionary<string, string>());
                return (response.Status, response.Response, response.IsSuccess);
            }
            return (response.Status, response.Response, response.IsSuccess);
        }
        public async Task<(int Status, string? Response ,EmployeeDTO? employeeDTO1,bool IsSuccess)> GetByID(string ID)
        {
           
            var employee = await _employeeRepository.GetByIDAsync(ID);

            var response = await resultPattern.GiveResponse(200, new Dictionary<string, string>());
            if (employee == null)
            {
                response = await resultPattern.GiveResponse(404, new Dictionary<string, string>());
                return (response.Status, response.Response,null,response.IsSuccess);
            }
            
            return (response.Status, response.Response, new EmployeeDTO(ID, employee.PersonID, employee.Salary,employee.HireDate,employee.IsActive), response.IsSuccess);
        }
       
        public async Task<(int Status, string? Response, List<EmployeeDTO>? employeesList)> GetAll()
        {
            var employees = await _employeeRepository.GetAllAsync();
            var response = await resultPattern.GiveResponse(200, new Dictionary<string, string>());
            if (employees == null)
            {
                response = await resultPattern.GiveResponse(404, new Dictionary<string, string>());
                return (response.Status, response.Response, null);
            }
            return (response.Status, response.Response, employees.Select(
                e =>
                new EmployeeMapper().ToDTO(e)).ToList());
        }
        public async Task<(int Status, string? Response, bool IsSuccess)> Save(EmployeeDTO employeeDTO=null,string ID="")
        {
            var response = await resultPattern.GiveResponse(200, new Dictionary<string, string>());
            switch (Mode)
            {
                case enMode.Update:
                    return await _Update(ID,employeeDTO);
                case enMode.Add:
                    var AddNew = await _AddNew(employeeDTO);
                    if (AddNew.IsSuccess)
                    {

                        Mode = enMode.Update;
                        return (response.Status, response.Response, response.IsSuccess);
                    }

                    else{
                        response = await resultPattern.GiveResponse(AddNew.Status, new Dictionary<string, string>());
                        return (response.Status, response.Response, response.IsSuccess);
                    }
            }
            response = await resultPattern.GiveResponse(500, new Dictionary<string, string>());
            return (response.Status, response.Response, response.IsSuccess);
        }
    }
}

