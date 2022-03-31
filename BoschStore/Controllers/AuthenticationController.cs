using System.Threading.Tasks;
using BoschStore;
using BusinessLogicLayer.Interfaces;
using BusinessObjectLayer.Dtos;
using BusinessObjectLayer.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace CIPTool.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly string currentUser;
        private readonly IUserService userService;

        public AuthenticationController(
            IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor,
            IUserService userService)
        {
            this.configuration = configuration;
            this.httpContextAccessor = httpContextAccessor;
            this.userService = userService;
            currentUser = this.httpContextAccessor.HttpContext.User.Identity.Name[5..];
        }

        [DisableCors]
        [AllowAnonymous]
        [HttpGet("login")]
        public async Task<IActionResult> Login()
        {
            UserDto loggedUser;
            var user = await userService.GetUserByUsernameAsync(currentUser);
            if (user != null)
            {
                return Ok(user);
            }

            var group = LdapWrapper.GetDepartmentByUserName(currentUser);

            loggedUser = new UserDto
            {
                Username = currentUser,
                Name = LdapWrapper.GetFirstNameByUsername(currentUser) + " " + LdapWrapper.GetLastNameByUsername(currentUser),
                Email = LdapWrapper.GetEmailAddressByUserName(currentUser),
                RoleType = RoleType.Associate,
                Group = group,
            };

            await userService.AddUserAsync(loggedUser);

            return Ok(loggedUser);
        }
    }
}
