using HSMBusiness.Mappers;
using HSMDataAccess.Data;
using HSMDataAccess.DTOs;
using HSMDataAccess.Entities;
using HSMDataAccess.RepositoryServices;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace HSMBusiness.Services
{
    public class UserService 
    {
        private readonly UserRepository _repository;

        public enum enMode { Add = 0, Update }
        public enMode Mode = enMode.Add;

        public UserService(UserRepository repository, enMode Mode = enMode.Add) 
        {
          
            _repository = repository;
            this.Mode = Mode;
        }
        public UserRepository userRepository
        {
            get
            {
                return _repository;
            }
        }
        private async Task<bool> _AddNew(UserDTO userDTO)
        {
            var user = new UserMapper().ToEntity(userDTO);
           var NewUser = await _repository.AddAsync(user);
            user.ID = NewUser.ID;
            return user.ID != "";
        }
        private async Task<bool> _Update(string ID,UserDTO userDTO)
        {

           User user = await _repository.GetByIDAsync(ID);
            if (!await _repository.ExistsAsync(ID))
                return false;
            if (user == null)
            {
                return false;
            }
            user = new UserMapper().ToEntity(userDTO,UserMapper.enMode.Update,user);
            return await _repository.UpdateAsync(user);
        }

        public async Task<UserDTO> GetByID(string ID)
        {
            UserDTO CurrentUser = new UserDTO("", "", "", false, "", "");
            var user1 = await _repository.GetByIDAsync(ID);
            if (user1 == null)
            {
                return new UserDTO("", "", "", false, "", "");
            }
            CurrentUser = new UserMapper().ToDTO(user1);
            return CurrentUser;
        }
        public async Task<List<UserDTO>> GetAll()
        {
            var users = await _repository.GetAllAsync();
            return users.Select(
                u =>
                new UserMapper().ToDTO(u)
                ).ToList();
        }
        public async Task<bool> Save(UserDTO userDTO,string ID="")
        {
            switch (Mode)
            {
                case enMode.Update:
                    return await _Update(ID,userDTO);
                case enMode.Add:
                    if (await _AddNew(userDTO))
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
