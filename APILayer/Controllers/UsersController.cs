using DbLayer.Models;
using DbLayer.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APILayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        UserService service = new UserService();

        // GET: api/<UsersController>
        [HttpGet]
        public IList<Users> Get()
        {
            IList<Users> list = service.GetAllUsers();
            return list;
        }

        // GET api/<UsersController>/5
        [HttpGet("{userId}")]
        public Users Get(int userId)
        {
            Users user = service.GetUserById(userId);
            return user;
        }

        // POST api/<UsersController>
        [HttpPost]
        public string Post([FromBody] Users user)
        {
            int rowsAffected = 0;
            var isexist = service.IsEmailExists(user.Email);
            if (isexist)
                return "Exist";

            rowsAffected = service.AddUser(user);
            if (rowsAffected > 0)
                return "Success";
            else
                return "Error";
        }

        // PUT api/<UsersController>/5
        [HttpPut("{userId}")]
        public void Put(int userId, [FromBody] Users user)
        {
            int result = service.UpdateUser(user);
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{userId}")]
        public void Delete(int userId)
        {
            int result = service.DeleteUser(userId);
        }

        [HttpGet("GetByEmail/{email}")]
        public Users GetUserByEmail(string email)
        {
            Users user = service.GetUserByEmail(email);
            return user;
        }

        [HttpGet("exists/{email}")]
        public bool IsEmailExist(string email)
        {
            return service.IsEmailExists(email);
        }

        [HttpGet("validate")]
        public Users ValidateUserLogin(string email, string password)
        {
            return service.ValidateUserLogin(email, password);

        }

        [HttpPut("reset-password")]
        public void ResetPassword(string email, string newPassword)
        {
            service.ResetPassword(email, newPassword);

        }
    }
}