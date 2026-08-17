using HSMDataAccess.Data;
using HSMDataAccess.RepositoryServices;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace HSMBusiness
{
    public class User
    {
        private readonly HSMDataAccess.RepositoryServices.UserRepository _repository;
        public string UserID { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Role { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? ContactNumber { get; set; } = null;
        public bool Status { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public DateTime? UpdatedOn { get; set; }
        public string AccessLevel { get; set; } = null!;
        public string Password { get; set; }
        public string CreatedBy { get; set; } = null!;
        public string UpdatedBy { get; set; } = null!;
        public enum enMode { Add = 0, Update }
        public enMode Mode = enMode.Add;
        public HSMDataAccess.DTOs.UserDTO user {get {
                return new HSMDataAccess.DTOs.UserDTO(UserID, Name, Role, Status,Password);
            } }
        public User(HSMDataAccess.DTOs.UserDTO user, HSMDataAccess.RepositoryServices.UserRepository repository, enMode Mode = enMode.Add)
        {
            this.UserID = user.UserID;
            this.Name = user.Name;
            this.Role = user.Role;
            _repository = repository;
            this.Status = user.Status;
          
            this.Mode = Mode;
    }
        private async Task<bool> _AddNew()
        {
            this.UserID =await _repository.AddUser(user);
            return this.UserID!="";
        }
        private async Task<bool> _Update()
        {
            return await _repository.UpdateUser(user);
        }
        public async Task<bool>Delete(string UserID)
        {
            var user =await _repository.GetUseByID(UserID);
            if (user == null)
            {
                return false;
            }
            return await _repository.Delete(UserID);
        }
        public async Task<HSMDataAccess.DTOs.UserDTO> GetUseByID(string ID)
        {
            HSMDataAccess.DTOs.UserDTO CurrentUser = new HSMDataAccess.DTOs.UserDTO("", "", "", false,"");
            var user1 = await _repository.GetUseByID(ID);
            if (user1 == null)
            {
                return new HSMDataAccess.DTOs.UserDTO("", "", "", false, "");
            }
            CurrentUser.UserID = ID;
            CurrentUser.Name = user1.Name;
            CurrentUser.Role = user1.Role;
            CurrentUser.Status = user1.Status;
            CurrentUser.PasswordHash = user1.HashPassword;
            return CurrentUser;
        }
        public async Task<List<HSMDataAccess.DTOs.UserDTO>> GetUsers()
        {
            return await _repository.GetAllUsers();
        }
        public async Task<bool> Save()
        {
            switch (Mode)
            {
                case enMode.Update:
                    return await _Update();
                case enMode.Add:
                    if (await _AddNew())
                    {
                        
                        Mode = enMode.Update;
                        return true;
                    }
                        
                    else
                        return false;
            }
            return false;
        }
    }
}
