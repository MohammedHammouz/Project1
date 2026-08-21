using HSMBusiness.dto;
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
        
        public string Specialization { get; set; } = null!;
        public bool Status { get; set; }
        
        public string UserID { get; set; } = null!;
       
        
        public DoctorDTO doctorDTO
        {
            get
            {
                return DoctorMapper.ToDTO(this);
            }
            set
            {
                DoctorMapper.FromDTO(value, this);
            }
           
        }
        public enum enMode { Add = 0, Update }
        public enMode mode = enMode.Add;
        private readonly DoctorRepository _doctor;
        public DoctorRepository doctorRepository { get { return _doctor; } }
        public Doctor( DoctorRepository doctor, enMode mode = enMode.Add)
        {
            _doctor = doctor;
            this.mode = mode;
        }
        private Doctor(string ID, string DepartmentID, string Specialization, bool Status, string UserID)
        {
            this.ID = ID;
            this.DepartmentID = DepartmentID;
            this.Specialization = Specialization;
            this.Status = Status;
            this.UserID = UserID;
            
        }
        public async Task<DoctorDTO> GetDoctorByID(string DoctorID)
        {
            DoctorEntity doctor = await _doctor.GetByIDAsync(DoctorID);
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
        public async Task<List<DoctorDTO>> GetAll()
        {
            var doctors = await _doctor.GetAllAsync();
            if (doctors == null)
            {
                return null;
            }
            return doctors.Select(d => new DoctorDTO(d.ID,d.DepartmentID,d.Specialization,d.Status,d.UserID))
        .ToList();
        }
        private async Task<bool> Add()
        {
            var doctorEntity = DoctorMapper.ToEntity(doctorDTO);
            var AddNew = await _doctor.AddAsync(doctorEntity);
            this.ID = AddNew.ID;
            return this.ID != "";
        }
        private async Task<bool> Update()
        {
            DoctorEntity? doctorEntity = DoctorMapper.ToEntity(doctorDTO);
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
