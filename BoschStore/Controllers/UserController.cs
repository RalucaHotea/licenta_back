using BusinessLogicLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoschStore.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserService userService;

        public UserController(IUserService _userService)
        {
            userService = _userService;
        }

        [HttpGet]
        [Route("GetUserByUserUsername")]
        public async Task<ActionResult> GetUserByUsername([FromQuery] string username)
        {
            var product = await userService.GetUserByUsernameAsync(username);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

    }
}
