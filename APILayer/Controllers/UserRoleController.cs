using DbLayer.Common;
using DbLayer.Models;
using DbLayer.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APILayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRoleController : ControllerBase
    {
        readonly UserRoleService roleService = new UserRoleService();

        // GET: api/<UserRoleController>
        [HttpGet]
        public IList<UserRole> Get()
        {
            List<UserRole> list = roleService.GetAllUserRoles().ToList();
            return list;
        }

        // POST api/<UserRoleController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
            int result = roleService.AddUserRole(new UserRole { RoleName = value });
        }

        // PUT api/<UserRoleController>/5
        [HttpPut("{oldRoleName}")]
        public void Put(string oldRoleName, [FromBody] string value)
        {
            int result = roleService.UpdateUserRole(oldRoleName, value);
        }

        // DELETE api/<UserRoleController>/5
        [HttpDelete("{roleName}")]
        public void Delete(string roleName)
        {
            int result = roleService.DeleteUserRole(roleName);
        }
    }
}
