using Azure;
using HSMBusiness.Erorrs;
using HSMBusiness.Mappers;
using HSMBusiness.Pattern;
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
        private readonly UserError _userError;
        ResultPatern resultPattern = new ResultPatern();
        public enum enMode { Add = 0, Update }
        public enMode Mode = enMode.Add;

        public UserService(UserRepository repository,
    UserError userError, enMode Mode = enMode.Add) 
        {
          
            _repository = repository;
            _userError = userError;
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
        private async Task<(int Status, string? Response, bool IsSuccess, Dictionary<string, string>? errors)> _Update(string ID,UserDTO userDTO)
        {

           User user = await _repository.GetByIDAsync(ID);
           
            var response = await resultPattern.GiveResponse(200, new Dictionary<string, string>());
            var errors = await _userError.ValidateUser(userDTO);
            if (errors.Count > 0)
            {
                response =
                await resultPattern.GiveResponse(400, errors);
                return (
                    response.Status,
                    response.Response,
                    response.IsSuccess,
                    response.Errors
                );
            }
            if (user == null)
            {
                 response = await resultPattern.GiveResponse(404, new Dictionary<string, string>());
                return (response.Status, response.Response, response.IsSuccess,response.Errors);
            }
            user = new UserMapper().ToEntity(userDTO,UserMapper.enMode.Update,user);
            await _repository.UpdateAsync(user);
            return  (response.Status, response.Response, response.IsSuccess, response.Errors);
        }

        public async Task<(int Status, string? Response, bool IsSucess, UserDTO? userDTO ,Dictionary<string, string>?errors)> GetByID(string ID)
        {
            UserDTO? CurrentUser = null;
            var user1 = await _repository.GetByIDAsync(ID);
            var response = await resultPattern.GiveResponse(200, new Dictionary<string, string>());
            if (user1 == null)
            {
                response = await resultPattern.GiveResponse(404, new Dictionary<string, string>());
                return (response.Status,response.Response,response.IsSuccess,CurrentUser,response.Errors);
            }
            CurrentUser = new UserMapper().ToDTO(user1);
            return  (response.Status, response.Response, response.IsSuccess, CurrentUser,response.Errors);
        }
        public async Task<(int Status, string? Response, bool IsSucess, List<UserDTO>? userList, Dictionary<string, string>? errors)> GetAll()
        {
            var users = await _repository.GetAllAsync();
            var response = await resultPattern.GiveResponse(200, new Dictionary<string, string>());
            if (users == null)
            {
                response = await resultPattern.GiveResponse(404, new Dictionary<string, string>());
               return (response.Status, response.Response, response.IsSuccess, null,response.Errors);
            }
            return (response.Status, response.Response, response.IsSuccess, users.Select(
                u =>
                new UserMapper().ToDTO(u)
                ).ToList(),response.Errors);
        }
        public async Task<(int Status, string? Response, bool IsSuccess)> Delete(string ID)
        {
            var report =
                await _repository.GetByIDAsync(ID);

            var response =
                await resultPattern.GiveResponse(200, new Dictionary<string, string>());

            if (report == null)
            {
                response =
                    await resultPattern.GiveResponse(404, new Dictionary<string, string>());

                return (
                    response.Status,
                    response.Response,
                    response.IsSuccess
                );
            }

            bool IsDeleted =
                await _repository.DeleteAsync(report);

            if (!IsDeleted)
            {
                response =
                    await resultPattern.GiveResponse(400, new Dictionary<string, string>());

                return (
                    response.Status,
                    response.Response,
                    response.IsSuccess
                );
            }

            return (
                response.Status,
                response.Response,
                response.IsSuccess
            );
        }
        public async Task<bool> IsUserExist(Doctor doctorEntity, DoctorDTO doctorDTO)
        {

            var user = await GetByID(doctorEntity.UserID);
            if (user.userDTO == null)
            {
                return false;
            }

            return true;
        }
        public async Task<(int Status, string? Response, bool IsSuccess, Dictionary<string, string>? errors)> Save(UserDTO userDTO,string ID="")
        {
            var response =
                await resultPattern.GiveResponse(200, new Dictionary<string, string>());
            var errors =await _userError.ValidateUser(userDTO);
            if (errors.Count > 0)
            {
                response =
                await resultPattern.GiveResponse(400, errors);
                return (
                    response.Status,
                    response.Response,
                    response.IsSuccess,
                    response.Errors
                );
            }
            switch (Mode)
            {

                case enMode.Update:
                    return await _Update(ID,userDTO);
                case enMode.Add:
                    if (await _AddNew(userDTO))
                    {
                        Mode = enMode.Update;
                    }
                    else
                    {
                        response =
                            await resultPattern.GiveResponse(400, new Dictionary<string, string>());
                    }

                    return (
                        response.Status,
                        response.Response,
                        response.IsSuccess,
                        response.Errors
                    );
            }
            response =
               await resultPattern.GiveResponse(500, new Dictionary<string, string>());

            return (
                response.Status,
                response.Response,
                response.IsSuccess,
                        response.Errors
            );
        }
    }
}
