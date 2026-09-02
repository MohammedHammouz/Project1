using HSMBusiness.Services;
using HSMDataAccess.DTOs;
using Microsoft.AspNetCore.Http;
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
            if (user == null)
                return NotFound();

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
            if (usersList == null)
                return NotFound();

            return Ok(usersList);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateUser(UserDTO user)
        {
            if (user == null)
                return BadRequest();

            userService.Mode = UserService.enMode.Add;
            var result = await userService.Save(user);

            if (!result)
                return BadRequest();

            return Ok(new { Message = "User created successfully" });
        }

        [HttpPut]
        [Route("{ID}")]
        public async Task<IActionResult> UpdateUser(string ID, UserDTO user)
        {
            if (user == null || ID is null || ID is "")
                return BadRequest();

            userService.Mode = UserService.enMode.Update;
            var result = await userService.Save(user, ID);

            if (!result)
                return BadRequest();


            return Ok(new { Message = "User updated successfully" });
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            if (id is null || id is "")
                return BadRequest();

            var result = await userService.Delete(id);

            if (!result)
                return BadRequest();

            return Ok(new { Message = "User deleted successfully" });
        }
    }
}
