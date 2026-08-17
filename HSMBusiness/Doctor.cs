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
    public class Doctor
    {
        public string ID { get; set; } = null!;
        public string DepartmentID { get; set; } = null!;
        public string CreatedBy { get; set; } = null!;
        public string? UpdatedBy { get; set; }
        public string Specialization { get; set; } = null!;
        public bool Status { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string UserID { get; set; } = null!;
        public DoctorDTO doctorDTO { 
            get {
                return new DoctorDTO(ID, DepartmentID,
            Specialization, Status,UserID);
            } 
        }
        public enum enMode { Add=0,Update}
        public enMode mode = enMode.Add;
        private readonly DoctorRepository _doctor;
        public Doctor(DoctorDTO doctorDTO, DoctorRepository doctor, enMode mode=enMode.Add)
        {
            this.ID = doctorDTO.ID;
            this.DepartmentID = doctorDTO.DepartmentID;
            this.Specialization = doctorDTO.Specialization;
            this.Status = doctorDTO.Status;
           
            this.UserID = doctorDTO.UserID;
            _doctor = doctor;
            this.mode = mode;
        }
        public async Task<DoctorDTO> GetDoctorByID(string DoctorID)
        {
            DoctorEntity doctor =await _doctor.GetByIDAsync(DoctorID);
            if (doctor == null)
            {
                return null;
            }
            return new DoctorDTO(DoctorID, doctor.DepartmentID, doctor.Specialization, doctor.Status, 
                 doctor.UserID);
        }
        public async Task<bool> Delete(string DoctorID)
        {
            DoctorEntity doctor = await _doctor.GetByIDAsync(DoctorID);
            if (doctor == null)
            {
                return false;
            }
            return await _doctor.DeleteAsync(doctor);
        }
        private async Task<bool> Add()
        {
            DoctorEntity? doctorEntity = null;
            doctorEntity.UserID = doctorDTO.UserID;
            doctorEntity.DepartmentID = doctorDTO.DepartmentID;
            doctorEntity.Specialization = doctorEntity.Specialization;
            doctorEntity.Status = doctorEntity.Status;

            this.ID = await _doctor.AddAsync(doctorEntity);
            return this.ID!="";
        }
        private async Task<bool> Update()
        {
            DoctorEntity? doctorEntity = null;
            doctorEntity.UserID = doctorDTO.UserID;
            doctorEntity.DepartmentID = doctorDTO.DepartmentID;
            doctorEntity.Specialization = doctorEntity.Specialization;
            doctorEntity.Status = doctorEntity.Status;
            return await _doctor.UpdateAsync(doctorEntity);
        }
        public async Task<bool> Save()
        {
            switch (mode)
            {
                case enMode.Add:
                    if (await Add())
                    {
                        mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return await Update();
            }
            return false;
        }
    }
}
