using HSMBusiness.Mappers;
using HSMBusiness.Pattern;
using HSMDataAccess.DTOs;
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

        private async Task<bool> Add(DepartmentDTO departmentDTO)
        {
            var departmentEntity =
                new DepartmentMapper().ToEntity(departmentDTO);

            var AddNew =
                await _department.AddAsync(departmentEntity);

            departmentEntity.ID = AddNew.ID;

            return departmentEntity.ID != "";
        }

        private async Task<(int, string?, bool)> Update(
            string ID,
            DepartmentDTO departmentDTO)
        {
            var CurrentDepartment =
                await _department.GetByIDAsync(ID);

            var response =
                await resultPattern.GiveResponse(200);

            if (CurrentDepartment == null)
            {
                response =
                    await resultPattern.GiveResponse(404);

                return (
                    response.Status,
                    response.Response,
                    response.IsSuccess
                );
            }

            CurrentDepartment =
                new DepartmentMapper().ToEntity(
                    departmentDTO,
                    DepartmentMapper.enMode.Update
                );

            return (
                response.Status,
                response.Response,
                await _department.UpdateAsync(CurrentDepartment)
            );
        }

        public async Task<(int, string?, bool)> Save(
            DepartmentDTO departmentDTO,
            string ID = "")
        {
            var response =
                await resultPattern.GiveResponse(200);

            switch (_mode)
            {
                case enMode.Add:

                    if (await Add(departmentDTO))
                    {
                        _mode = enMode.Update;
                    }
                    else
                    {
                        response =
                            await resultPattern.GiveResponse(400);
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
                await resultPattern.GiveResponse(500);

            return (
                response.Status,
                response.Response,
                response.IsSuccess
            );
        }

        public async Task<(
            int,
            string?,
            bool,
            List<DepartmentDTO>
        )> GetAll()
        {
            var departments =
                await _department.GetAllAsync();

            var response =
                await resultPattern.GiveResponse(200);

            if (departments == null)
            {
                response =
                    await resultPattern.GiveResponse(404);

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
            int,
            string?,
            bool,
            DepartmentDTO
        )> GetByID(string ID)
        {
            var department =
                await _department.GetByIDAsync(ID);

            var response =
                await resultPattern.GiveResponse(200);

            if (department == null)
            {
                response =
                    await resultPattern.GiveResponse(404);

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

        public async Task<(int, string?, bool)> Delete(string ID)
        {
            var department =
                await _department.GetByIDAsync(ID);

            var response =
                await resultPattern.GiveResponse(200);

            if (department == null)
            {
                response =
                    await resultPattern.GiveResponse(404);

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
                    await resultPattern.GiveResponse(400);

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
