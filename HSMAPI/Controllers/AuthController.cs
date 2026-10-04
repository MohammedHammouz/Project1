using HSMBusiness.Services;
using HSMDataAccess.DTOs;
using HSMDataAccess.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HSMAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(UserService userService) : ControllerBase
    {
        //[HttpPost("login")]
        //public async Task<IActionResult> Login([FromBody] UserAuthentication request)
        //{
        //    var isAuthenticated = await userService.AuthenticateUserAsync(request.UserName, request.PasswordHash);
        //    if (!isAuthenticated)
        //    {
        //        return Unauthorized(new { message = "Invalid credentials" });
        //    }

        //    //var user = await userService.GetByUsernameAsync(request.UserName);
        //    //var token = userService.GenerateJwtToken(user!);
        //    return Ok(new { Token = "token" });
        //}
        //private readonly UserService _user;
        //public AuthController(UserService user)
        //{
        //    _user = user;
        //}
        [HttpGet]
        [Route("{Id}")]
        public async Task<IActionResult> GetUserByIdAsync(string Id)
        {
            var user = await userService.GetByID(Id);
            if (Id == "")
            {
                return BadRequest($"{user.Response},{user.Response},{user.errors}");
            }
            if (user.userDTO == null) {
                return NotFound($"{user.Response},{user.Response},{user.errors}");
            }

            return Ok(user);
        }

        //[HttpGet]
        //[Route("Username")]
        //public async Task<IActionResult> GetUserByUsernameAsync(string Username)
        //{
        //    var user = await userService.GetByUsernameAsync(Username);
        //    if (user == null)
        //        return NotFound();

        //    return Ok(user);
        //}

        [HttpGet]
        [Route("All")]
        public async Task<IActionResult> GetAllUsers()
        {
            var usersList = await userService.GetAll();
            if (usersList.Item4 == null){
                return NotFound($"{usersList.Response},{usersList.Response},{usersList.errors}");
            }
            return Ok(new
            {
                status = usersList.Status,
                response = usersList.Response,
                users = usersList.userList,
                errors = usersList.errors
            }); ;
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateUser(UserDTO user)
        {
            if (user == null){
                return NotFound();
            }
            var NumOfUser = await userService.NumberOfPerson(user.PersonID);
            if (!NumOfUser.IsSuccess)
            {
                return StatusCode(500, new
                {
                    status = NumOfUser.Status,
                    response = NumOfUser.Response,
                    isSuccess = NumOfUser.IsSuccess,
                    errors = NumOfUser.errors
                });
            }
            userService.Mode = UserService.enMode.Add;
            var result = await userService.Save(user);
           
            if (!result.IsSuccess){
                return BadRequest(new
                {
                    status = result.Status,
                    response = result.Response,
                    isSuccess = result.IsSuccess,
                    errors = result.errors
                });
            }
           
            return Ok(new
            {
                status = result.Status,
                response = result.Response,
                isSuccess = result.IsSuccess,
                errors = result.errors
            });
        }

        [HttpPut]
        [Route("{ID}")]
        public async Task<IActionResult> UpdateUser(string ID, UserDTO user)
        {
            if (user == null || ID is null || ID is "")
                return BadRequest();
            var NumOfUser = await userService.NumberOfPerson(user.PersonID);
            //if (!NumOfUser.IsSuccess)
            //{
            //    return StatusCode(500, new
            //    {
            //        status = NumOfUser.Status,
            //        response = NumOfUser.Response,
            //        isSuccess = NumOfUser.IsSuccess,
            //        errors = NumOfUser.errors
            //    });
            //}
            userService.Mode = UserService.enMode.Update;
            var result = await userService.Save(user, ID);

            if (!result.IsSuccess)
                return BadRequest(new
                {
                    status = result.Status,
                    response = result.Response,
                    isSuccess = result.IsSuccess,
                    errors = result.errors
                });


            return Ok(new
            {
                status = result.Status,
                response = result.Response,
                isSuccess = result.IsSuccess,
                errors = result.errors
            });
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            if (id is null || id is "")
                return BadRequest();

            var result = await userService.Delete(id);

            if (!result.IsSuccess)
                return BadRequest();

            return Ok(result.Response);
        }
    }
}
