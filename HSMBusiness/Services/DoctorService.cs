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
    public class DoctorService
    {
        //public string ID { get; set; } = null!;
        //public string DepartmentID { get; set; } = null!;
        
        //public string Specialization { get; set; } = null!;
        //public bool Status { get; set; }
        
        //public string UserID { get; set; } = null!;
       
        
        //public DoctorDTO doctorDTO
        //{
        //    get
        //    {
        //        return DoctorMapper.ToDTO(this);
        //    }
        //    set
        //    {
        //        DoctorMapper.FromDTO(value, this);
        //    }
           
        //}
        public enum enMode { Add = 0, Update }
        public enMode mode = enMode.Add;
        private readonly DoctorRepository _doctor;
        public DoctorRepository doctorRepository { get { return _doctor; } }
        public DoctorService( DoctorRepository doctor, enMode mode = enMode.Add)
        {
            _doctor = doctor;
            this.mode = mode;
        }
        
        public async Task<DoctorDTO> GetDoctorByID(string DoctorID)
        {
            Doctor doctor = await _doctor.GetByIDAsync(DoctorID);
            if (doctor == null)
            {
                return null;
            }
            return new DoctorDTO(DoctorID, doctor.DepartmentID, doctor.Specialization, doctor.Status,
                 doctor.UserID);
        }
        public async Task<bool> Delete(string DoctorID)
        {
            Doctor doctor = await _doctor.GetByIDAsync(DoctorID);
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
        private async Task<bool> Add(DoctorDTO doctorDTO)
        {
            var doctorEntity = new DoctorMapper().ToEntity(doctorDTO);
            var AddNew = await _doctor.AddAsync(doctorEntity);
            doctorEntity.ID = AddNew.ID;
            return doctorEntity.ID != "";
        }
        private async Task<bool> Update(string ID)
        {
            Doctor? doctorEntity = await _doctor.GetByIDAsync(ID);
            return await _doctor.UpdateAsync(doctorEntity);
        }
        public async Task<bool> Save(DoctorDTO doctorDTO=null,string ID="")
        {
            switch (mode)
            {
                case enMode.Add:
                    if (await Add(doctorDTO))
                    {
                        mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return await Update(ID);
            }
            return false;
        }
    }
}
