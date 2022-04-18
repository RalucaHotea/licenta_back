using BusinessObjectLayer.Dtos;
using BusinessLogicLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BoschStore.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService emailService;

        public EmailController(
       IEmailService _emailService
        )
        {
            this.emailService = _emailService;
        }

        [HttpPost]
        [Route("SendEmail")]
        public async Task<IActionResult> SendEmail([FromBody] EmailDetailsDto email)
        {
            await emailService.SendEmailAsync(email);
            return Ok();
        }
    }
}
