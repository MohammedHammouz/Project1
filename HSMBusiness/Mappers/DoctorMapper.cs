using HSMBusiness.Mappers;
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
        public enum enMode { Add,Update}

        public DoctorDTO ToDTO(Doctor doctor)
        {
            return new DoctorDTO(doctor.ID,
                 doctor.DepartmentID,
                    doctor.Specialization,
                    doctor.Status,
                    doctor.UserID
                );
        }
        public Doctor ToEntity(DoctorDTO doctorDTO,enMode mode=enMode.Add,Doctor doctor=null)
        {
            if (mode == enMode.Add)
                return new Doctor
                {
                    ID = doctorDTO.ID,
                    DepartmentID = doctorDTO.DepartmentID,
                    Specialization = doctorDTO.Specialization,
                    Status = doctorDTO.Status,
                    UserID = doctorDTO.UserID
                };
            else
            {
                doctor.DepartmentID = doctorDTO.DepartmentID;
                doctor.Specialization = doctorDTO.Specialization;
                doctor.Status = doctorDTO.Status;
                doctor.UserID = doctorDTO.UserID;
                return doctor;
            }

        }
        
    }
}
