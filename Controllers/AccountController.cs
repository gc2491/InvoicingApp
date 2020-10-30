using AspNET.Models;
using AspNET.Models.InputModels;
using AspNET.Models.Options;
using AspNET.Models.ResultModel;
using AspNET.Models.Services.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace AspNET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AuthService AuthService;

        public AccountController(
        InvoiceDbContext invContext,
        UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager,
        RoleManager<IdentityRole> roleManager,
        IOptionsMonitor<AuthOptions> authOptions,
        IOptionsMonitor<SmtpOptions> smtpOptions)
        {
            AuthService = new AuthService(
                invContext,
                userManager,
                signInManager,
                roleManager,
                authOptions,
                smtpOptions
                );
        }

        [Authorize(Roles = "admin, user")]
        [HttpPost("auth")]
        public IActionResult Auth([FromHeader] string authorization)
        {
            QueryResModel<UserAuth> result = AuthService.Auth(authorization);

            if (result.Succeeded)
            {
                return Ok(result.Data);
            }
            else
            {
                return BadRequest(result.Error);
            }
        }

        [Authorize(Roles = "admin")]
        [HttpPost("register")]
        public async Task<IActionResult> Register(AccountInputModel input)
        {
            IdentityResult result = await AuthService.Register(input).ConfigureAwait(false);

            if (result.Succeeded)
            {
                return Ok("Account successfuly created.");
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        [Authorize(Roles = "admin")]
        [HttpGet("roles")]
        public async Task<IActionResult> GetRoles()
        {
            QueryResModel<List<string>> result = await AuthService.GetRoles().ConfigureAwait(false);

            if (result.Succeeded)
            {
                return Ok(result.Data);
            }
            else
            {
                return BadRequest(result.Error);
            }
        }

        [HttpPost("signin")]
        public async Task<IActionResult> SignIn([FromForm] string username, [FromForm] string password)
        {
            QueryResModel<string> result = await AuthService.SignIn(username, password).ConfigureAwait(false);

            if (result.Succeeded)
            {
                return Ok(result.Data);
            }
            else
            {
                return BadRequest(result.Error);
            }
        }

        [Authorize(Roles = "admin, user")]
        [HttpPut("updatesecurity")]
        public async Task<IActionResult> UpdateSecurity([FromForm] AccountInputModel account)
        {
            IdentityResult result = await AuthService.UpdateSecurity(account).ConfigureAwait(false);

            if (result.Succeeded)
            {
                return Ok("Password aggiornata correttamente");
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        [HttpPost("recovery")]
        public async Task<IActionResult> AccountRecovery()
        {
            string username = "";
            using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                username = await reader.ReadToEndAsync();
            }

            // Recover account
            await AuthService.RecoverAccountAsync(username).ConfigureAwait(false);

            return Ok("Richiesta di recupero password ricevuta");
        }

        [HttpPost("resetpassword")]
        public async Task<IActionResult> ResetPasswordAsync([FromForm] ResetPasswordInputModel input)
        {
            if(await AuthService.ResetPasswordAsync(input))
            {
                return Ok("Password cambiata");
            }
            else
            {
                return BadRequest("Impossibile cambiare la password");
            }
        }

        [Authorize(Roles = "admin, user")]
        [HttpPost("checkAuth")]
        public IActionResult CheckAuth()
        {
            return Ok();
        }
    }
}
