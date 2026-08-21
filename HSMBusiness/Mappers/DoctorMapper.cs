using HSMDataAccess.DTOs;
using HSMDataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HSMBusiness.dto
{
    public class DoctorMapper
    {  
        public static DoctorDTO ToDTO(Doctor doctor)
        {
            return new DoctorDTO(doctor.ID,
                 doctor.DepartmentID,
                    doctor.Specialization,
                    doctor.Status,
                    doctor.UserID
                );
        }
        public static DoctorEntity ToEntity(DoctorDTO doctorDTO)
        {
            return new DoctorEntity
            {
                ID = doctorDTO.ID,
                DepartmentID = doctorDTO.DepartmentID,
                Specialization = doctorDTO.Specialization,
                Status = doctorDTO.Status,
                UserID = doctorDTO.UserID
            };
        }
        public static void FromDTO(DoctorDTO dto, Doctor doctor)
        {
            doctor.ID = dto.ID;
            doctor.DepartmentID = dto.DepartmentID;
            doctor.Specialization = dto.Specialization;
            doctor.Status = dto.Status;
            doctor.UserID = dto.UserID;
        }
    }
}
