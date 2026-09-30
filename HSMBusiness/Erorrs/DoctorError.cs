using HSMBusiness.Services;
using HSMDataAccess.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSMBusiness.Erorrs
{
    public class DoctorError
    {
        private readonly DepartmentService? _departmentService;
        private readonly UserService? _userService;
        public async Task<Dictionary<string, string>> ValidateDoctor(
                    DoctorDTO dto)
        {
            var errors = new Dictionary<string, string>();

            // DoctorID
            if (string.IsNullOrWhiteSpace(dto.ID))
            {
                errors["DoctorID"] = "DoctorID is required.";
            }

            // DepartmentID
            if (string.IsNullOrWhiteSpace(dto.DepartmentID))
            {
                errors["DepartmentID"] = "DepartmentID is required.";
            }
            else
            {
                var department = await _departmentService
                    .GetByID(dto.DepartmentID);

                if (department.Item4 == null)
                {
                    errors["DepartmentID"] =
                        "Department does not exist.";
                }
            }

            // Specialization
            if (string.IsNullOrWhiteSpace(dto.Specialization))
            {
                errors["Specialization"] =
                    "Specialization is required.";
            }
            else if (dto.Specialization.Length > 50)
            {
                errors["Specialization"] =
                    "Specialization cannot exceed 50 characters.";
            }

            // Status
            // SQL: bit NOT NULL
            if (dto.Status != true && dto.Status != false)
            {
                errors["Status"] =
                    "Status must be either true or false.";
            }

            // UserID
            if (string.IsNullOrWhiteSpace(dto.UserID))
            {
                errors["UserID"] = "UserID is required.";
            }

            return errors;
        }
    }
}
