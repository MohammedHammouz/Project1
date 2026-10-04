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
    public class DepartmentService
    {
        ResultPatern resultPattern = new ResultPatern();

        public enum enMode { Add = 0, Update }

        public enMode _mode = enMode.Add;

        private readonly DepartmentRepository _department;

        public DepartmentRepository departmentRepository
        {
            get { return _department; }
        }

        public DepartmentService(
            DepartmentRepository department,
            enMode mode = enMode.Add)
        {
            _department = department;
            _mode = mode;
        }

        private async Task<(int Status,bool IsSuccess)> Add(DepartmentDTO departmentDTO)
        {
            var departmentEntity = new DepartmentMapper().ToEntity(departmentDTO);
            var response = await resultPattern.GiveResponse(200, new Dictionary<string, string>());
            var AddNew = await _department.AddAsync(departmentEntity);
            int Status = 200;
            departmentEntity.ID = AddNew.ID;
            if(string.IsNullOrEmpty(departmentEntity.ID))
            {
                Status = 400;
                return (Status, false);
            }
            return (Status,true);
        }

        private async Task<(int, string?, bool)> Update( string ID, DepartmentDTO departmentDTO)
        {
            var CurrentDepartment = await _department.GetByIDAsync(ID);

            var response = await resultPattern.GiveResponse(200, new Dictionary<string, string>());

            if (CurrentDepartment == null)
            {
                response = await resultPattern.GiveResponse(404, new Dictionary<string, string>());

                return (
                    response.Status,
                    response.Response,
                    response.IsSuccess
                );
            }
            await _department.UpdateAsync(CurrentDepartment);
            CurrentDepartment =
                new DepartmentMapper().ToEntity(
                    departmentDTO,
                    DepartmentMapper.enMode.Update
                );
           
            return (
                response.Status,
                response.Response,
                response.IsSuccess
            );
        }
        public async Task<bool> IsDepartmentExist(Doctor doctorEntity, DoctorDTO doctorDTO)
        {

            var department = await GetByID(doctorEntity.DepartmentID);
            if (department.departmentDTO1.Name == "")
            {
                return false;
            }

            return true;
        }
        public async Task<(int Status, string?Response, bool IsSuccess)> Save(
            DepartmentDTO departmentDTO,
            string ID = "")
        {
            var response =
                await resultPattern.GiveResponse(200, new Dictionary<string, string>());

            switch (_mode)
            {
                case enMode.Add:
                    var NewDepartment = await Add(departmentDTO);
                    if (NewDepartment.IsSuccess)
                    {
                        _mode = enMode.Update;
                    }
                    else
                    {
                        response =
                            await resultPattern.GiveResponse(NewDepartment.Status, new Dictionary<string, string>());
                    }

                    return (
                        response.Status,
                        response.Response,
                        response.IsSuccess
                    );

                case enMode.Update:

                    return await Update(ID, departmentDTO);
            }

            response =
                await resultPattern.GiveResponse(500, new Dictionary<string, string>());

            return (
                response.Status,
                response.Response,
                response.IsSuccess
            );
        }

        public async Task<(
            int Status,
            string? Response,
            bool IsSuccess,
            List<DepartmentDTO>? ListDepartments
        )> GetAll()
        {
            var departments =
                await _department.GetAllAsync();

            var response =
                await resultPattern.GiveResponse(200, new Dictionary<string, string>());

            if (departments == null)
            {
                response =
                    await resultPattern.GiveResponse(404, new Dictionary<string, string>());

                return (
                    response.Status,
                    response.Response,
                    response.IsSuccess,
                    null
                );
            }

            return (
                response.Status,
                response.Response,
                response.IsSuccess,
                departments.Select(d =>
                    new DepartmentDTO(
                        d.ID,
                        d.Name,
                        d.HeadOf
                    )
                ).ToList()
            );
        }

        public async Task<(
            int Status,
            string? Response,
            bool IsSuccess,
            DepartmentDTO departmentDTO1
        )> GetByID(string ID)
        {
            var department =
                await _department.GetByIDAsync(ID);

            var response =
                await resultPattern.GiveResponse(200, new Dictionary<string, string>());

            if (department == null)
            {
                response =
                    await resultPattern.GiveResponse(404, new Dictionary<string, string>());

                return (
                    response.Status,
                    response.Response,
                    response.IsSuccess,
                    new DepartmentDTO(
                        "",
                        "",
                        null
                    )
                );
            }

            return (
                response.Status,
                response.Response,
                response.IsSuccess,
                new DepartmentDTO(
                    department.ID,
                    department.Name,
                    department.HeadOf
                )
            );
        }

        public async Task<(int Status, string?Response, bool IsSuccess)> Delete(string ID)
        {
            var department =
                await _department.GetByIDAsync(ID);

            var response =
                await resultPattern.GiveResponse(200, new Dictionary<string, string>());

            if (department == null)
            {
                response =
                    await resultPattern.GiveResponse(404, new Dictionary<string, string>());

                return (
                    response.Status,
                    response.Response,
                    response.IsSuccess
                );
            }

            bool IsDeleted =
                await _department.DeleteAsync(department);

            if (!IsDeleted)
            {
                response =
                    await resultPattern.GiveResponse(400, new Dictionary<string, string>());

                return (
                    response.Status,
                    response.Response,
                    response.IsSuccess
                );
            }

            return (
                response.Status,
                response.Response,
                response.IsSuccess
            );
        }
    }
}
