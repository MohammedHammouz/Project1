using HSMDataAccess.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace HSMDataAccess.RepositoryServices
{
    public class UserRepository
    {
        private readonly AppDBContext _context;

        public UserRepository(AppDBContext context)
        {
            _context = context;
        }
        public async Task<List<DTOs.UserDTO>>GetAllUsers()
        {
           
        var users = await _context.Users.Select(u=>new DTOs.UserDTO { 
                UserID=u.UserID,
                Name = u.Name,
                Role = u.Role,
                Status = u.Status
            }).AsNoTracking().ToListAsync();
            return users;
        }
        public async Task<Entities.UserEntity> GetUseByID(string ID)
        {
            
            var user = await _context.Users.FindAsync(ID);
            
            return user;
        }
        public async Task<string> AddUser(DTOs.UserDTO NewUser)
        {
            var CurrentUser= new Entities.UserEntity
            {
                UserID = NewUser.UserID,
                Name = NewUser.Name,
                Role = NewUser.Role,
                Status = NewUser.Status,
                HashPassword=NewUser.PasswordHash
            };
            var user=await _context.Users.AddAsync(CurrentUser);
            int AffectedRows = await _context.SaveChangesAsync();
            return CurrentUser.UserID;
        }
        public async Task<bool>Delete(string UserID)
        {
            var user = await _context.Users.FindAsync(UserID);
            if (user == null)
                return false;
            _context.Users.Remove(user);
            return await _context.SaveChangesAsync()>0;
        }
        public async Task<bool> UpdateUser(DTOs.UserDTO NewUser)
        {
            var User =await _context.Users.FirstOrDefaultAsync(u => u.UserID == NewUser.UserID);
            if (User == null)
            {
                return false;
            }
            User.Name = NewUser.Name;
            User.Role = NewUser.Role;
            User.Status = NewUser.Status;
            User.HashPassword = NewUser.PasswordHash;
            int RowsAffected =await _context.SaveChangesAsync();
            return RowsAffected > 0;
        }
    }
}
