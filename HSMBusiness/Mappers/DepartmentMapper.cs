using HSMDataAccess.DTOs;
using HSMDataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSMBusiness.Mappers
{
    public class DepartmentMapper
    {
       
            public enum enMode { Add, Update }

            public Department ToEntity(
                DepartmentDTO departmentDTO,
                enMode mode = enMode.Add)
            {
                if (mode == enMode.Add)
                {
                    return new Department
                    {
                        ID = departmentDTO.ID,
                        Name = departmentDTO.Name,
                        HeadOf = departmentDTO.HeadOf
                    };
                }
                else
                {
                    return new Department
                    {
                        ID = departmentDTO.ID,
                        Name = departmentDTO.Name,
                        HeadOf = departmentDTO.HeadOf
                    };
                }
            }

            public DepartmentDTO ToDTO(Department department)
            {
                return new DepartmentDTO
                (
                    department.ID,
                    department.Name,
                    department.HeadOf
                );
            }
        }
}
