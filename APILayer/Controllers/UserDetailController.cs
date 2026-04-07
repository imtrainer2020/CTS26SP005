using APILayer.Common;
using DbLayer.Models;
using DbLayer.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APILayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserDetailController : ControllerBase
    {
        UserDetailService service = new UserDetailService();

        // GET api/<UserDetailController>/5
        [HttpGet("{id}")]
        public UserDetail Get(int id)
        {
            return service.GetUserDetailById(id);
        }

        // GET api/<UserDetailController>/5
        [HttpGet("User/{userId}")]
        public UserDetail GetbyUserId(int userId)
        {
            return service.GetUserDetailByUserId(userId);
        }

        // POST api/<UserDetailController>
        [HttpPost]
        public void Post([FromForm] UserDetail userDetail, IFormFile photoFile)
        {
            userDetail.Photo = (photoFile != null && photoFile.Length > 0) ? CommonFuncs.UploadFile(photoFile).ToArray() : null;
            int res = service.AddUserDetail(userDetail);
        }

        // PUT api/<UserDetailController>/5
        [HttpPut]
        public void Put(IFormFile photoFile, [FromForm] UserDetail userDetail)
        {
            userDetail.Photo = (photoFile != null && photoFile.Length > 0) ? 
                CommonFuncs.UploadFile(photoFile).ToArray() 
                : null;

            int res = service.UpdateUserDetail(userDetail);
        }

        // DELETE api/<UserDetailController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            int res = service.DeleteUserDetail(id);
        }

    }
}
