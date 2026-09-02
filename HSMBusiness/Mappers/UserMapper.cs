using HSMDataAccess.DTOs;
using HSMDataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSMBusiness.Mappers
{
    public class UserMapper 
    {
        public enum enMode { Add,Update}
       
        public UserDTO ToDTO(User user)
        {
            return new UserDTO(user.ID,user.Name,user.Role,user.Status,user.PasswordHash,user.EmployeeID);
        }

        public User ToEntity(UserDTO userDTO, enMode mode = enMode.Add, User user = null)
        {
            
            if (mode == enMode.Add)
            {
                return new User
                {
                    ID = Guid.NewGuid().ToString("N").Substring(0, 10),
                    Name = userDTO.Name,
                    Role = userDTO.Role,
                    Status = userDTO.Status,
                    PasswordHash = userDTO.PasswordHash,
                    EmployeeID = userDTO.EmployeeID
                };
            }
            else
            {
                user.Name = userDTO.Name;
                user.Role = userDTO.Role;
                user.Status = userDTO.Status;
                user.PasswordHash = userDTO.PasswordHash;
                user.EmployeeID = userDTO.EmployeeID;
                return user;
            }

        }
        
    }
}
