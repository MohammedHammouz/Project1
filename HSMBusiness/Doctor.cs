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
        public string Status { get; set; } = null!;
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string UserID { get; set; } = null!;
        public DoctorDTO doctorDTO { 
            get {
                return new DoctorDTO(ID, DepartmentID, CreatedBy, UpdatedBy,
            Specialization, Status, CreatedOn, UpdatedOn,UserID);
            } 
        }
        public enum enMode { Add=0,Update}
        public enMode mode = enMode.Add;
        private readonly DoctorRepository _doctor;
        public Doctor(DoctorDTO doctorDTO, DoctorRepository doctor, enMode mode=enMode.Add)
        {
            this.ID = doctorDTO.ID;
            this.DepartmentID = doctorDTO.DepartmentID;
            this.CreatedBy = doctorDTO.CreatedBy;
            this.UpdatedBy = doctorDTO.UpdatedBy;
            this.Specialization = doctorDTO.Specialization;
            this.Status = doctorDTO.Status;
            this.CreatedOn = doctorDTO.CreatedOn;
            this.UpdatedOn = doctorDTO.UpdatedOn;
            this.UserID = doctorDTO.UserID;
            _doctor = doctor;
            mode = enMode.Update;
        }
        public async Task<DoctorDTO> GetDoctorByID(string DoctorID)
        {
            DoctorEntity doctor =await _doctor.GetByID(DoctorID);
            if (doctor == null)
            {
                return null;
            }
            return new DoctorDTO(DoctorID, doctor.DepartmentID, doctor.CreatedBy,
                doctor.UpdatedBy, doctor.Specialization, doctor.Status, 
                doctor.CreatedOn, doctor.UpdatedOn, doctor.UserID);
        }
        public async Task<bool> Delete(string DoctorID)
        {
            DoctorEntity doctor = await _doctor.GetByID(DoctorID);
            if (doctor == null)
            {
                return false;
            }
            return await _doctor.Delete(DoctorID);
        }
        private async Task<bool> Add()
        {
            this.ID = await _doctor.AddNew(doctorDTO);
            return this.ID!="";
        }
        private async Task<bool> Update()
        {
            return await _doctor.Update(doctorDTO, this.ID);
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
