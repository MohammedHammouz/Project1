using HSMDataAccess.Data;
using HSMDataAccess.DTOs;
using HSMDataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace HSMDataAccess.RepositoryServices
{
    public class DoctorRepository
    {
        AppDBContext _context;
        public DoctorRepository(AppDBContext context)
        {
            _context = context;
        }
        public async Task<List<DoctorEntity?>> GetAll()
        {
            var doctors = await _context.Doctors
                .FromSqlInterpolated($"EXEC dbo.SP_GetAllDoctors")
                .AsNoTracking()
                .ToListAsync();
            return doctors;
        }
        public async Task<DoctorEntity> GetByID(string DoctorID)
        {
            var doctor = await _context.Doctors
                .FromSqlInterpolated($"EXEC SP_GetDoctorByID @ID={DoctorID}")
                .FirstOrDefaultAsync();
            return doctor;
        }
        public async Task<string> AddNew(DoctorDTO doctor)
        {
            await _context.Database.ExecuteSqlInterpolatedAsync($@"
        EXEC dbo.SP_AddDoctor
            @ID={doctor.ID},
            @DepartmentID={doctor.DepartmentID},
            @Specialization={doctor.Specialization},
            @Status={doctor.Status},
            @CreatedOn={doctor.CreatedOn},
            @UpdatedOn={doctor.UpdatedOn},
           
            @CreatedBy={doctor.CreatedBy},
            @UpdatedBy={doctor.UpdatedBy},
            @UserID={doctor.UserID}");
            return doctor.ID;
        }
        public async Task<bool> Update(DoctorDTO doctor,string DoctorID)
        {
            int rowsAffected = await _context.Database.ExecuteSqlInterpolatedAsync($@"
        EXEC dbo.SP_UpdateDoctor
            
            @DepartmentID={doctor.DepartmentID},
            @Specialization={doctor.Specialization},
            @Status={doctor.Status},
            @CreatedOn={doctor.CreatedOn},
            @UpdatedOn={doctor.UpdatedOn},
            @CreatedBy={doctor.CreatedBy},
            @UpdatedBy={doctor.UpdatedBy},
            @UserID={doctor.UserID},
            @ID={DoctorID}");
            return rowsAffected>0;
        }
        public async Task<bool> Delete(string DoctorID)
        {
            int rowsAffected = await _context.Database.ExecuteSqlInterpolatedAsync($@"
        EXEC dbo.SP_DeleteDoctor @ID={DoctorID}");
            return rowsAffected > 0;
        }
    }
}
