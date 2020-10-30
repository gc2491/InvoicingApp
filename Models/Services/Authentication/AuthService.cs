using AspNET.Models.InputModels;
using AspNET.Models.Options;
using AspNET.Models.ResultModel;
using AspNET.Models.Services.Email;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AspNET.Models.Services.Authentication
{
    public class AuthService : IAuthService
    {
        public InvoiceDbContext InvoiceContext { get; set; }
        public UserManager<IdentityUser> UserManager { get; }
        public SignInManager<IdentityUser> SignInManager { get; }
        public RoleManager<IdentityRole> RoleManager { get; }
        public IOptionsMonitor<AuthOptions> AuthOptions { get; }
        public IOptionsMonitor<SmtpOptions> SmtpOptions { get; }

        public AuthService(
            InvoiceDbContext invContext,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            IOptionsMonitor<AuthOptions> authOptions,
            IOptionsMonitor<SmtpOptions> smtpOptions)
        {
            InvoiceContext = invContext;
            UserManager = userManager;
            SignInManager = signInManager;
            RoleManager = roleManager;
            AuthOptions = authOptions;
            SmtpOptions = smtpOptions;
        }

        public QueryResModel<UserAuth> Auth(string token)
        {
            if (token.Contains("Bearer "))
            {
                token = token.Split(" ")[1];
            }
            else
            {
                return new QueryResModel<UserAuth> { Succeeded = false, Error = "Invalid JWT Token" };
            }

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            JwtSecurityToken jwtToken = handler.ReadJwtToken(token);
            IEnumerable<Claim> claims = jwtToken.Claims;
            QueryResModel<UserAuth> user = new QueryResModel<UserAuth>();
            user.Data = new UserAuth();

            foreach (var claim in claims)
            {
                switch (claim.Type)
                {
                    case "sub":
                        user.Data.Username = claim.Value;
                        break;
                    case ClaimTypes.Role:
                        user.Data.Role = claim.Value;
                        break;

                    default:
                        break;
                }
            }
            return user;
        }

        public async Task<QueryResModel<List<string>>> GetRoles()
        {
            QueryResModel<List<string>> roles = new QueryResModel<List<string>>();

            List<IdentityRole> ide = await RoleManager.Roles.ToListAsync();

            roles.Data = ide.Select(r => r.Name).OrderByDescending(r => r).ToList();

            return roles;
        }

        public async Task<IdentityResult> Register(AccountInputModel input)
        {
            IdentityUser newUser = new IdentityUser() { UserName = input.Username };
            IdentityResult result = await UserManager.CreateAsync(newUser, input.Password).ConfigureAwait(false);

            IdentityUser user = await UserManager.FindByNameAsync(input.Username).ConfigureAwait(false);

            if (user != null && !(await UserManager.IsInRoleAsync(user, input.Role).ConfigureAwait(false)))
            {
                await UserManager.AddToRoleAsync(user, input.Role).ConfigureAwait(false);
            }

            return result;
        }

        public async Task<IdentityResult> UpdateSecurity(AccountInputModel account)
        {
            IdentityResult result = new IdentityResult();
            IdentityUser user = await UserManager.FindByNameAsync(account.Username).ConfigureAwait(false);

            if (user != null && await UserManager.CheckPasswordAsync(user, account.OldPassword).ConfigureAwait(false))
            {
                result = await UserManager.ChangePasswordAsync(user, account.OldPassword, account.Password);
            }

            return result;
        }

        public async Task RecoverAccountAsync(string username)
        {
            try
            {
                IdentityUser user = await UserManager.FindByNameAsync(username);

                string token = await UserManager.GeneratePasswordResetTokenAsync(user);

                EmailSender sender = new EmailSender(SmtpOptions);
                string subject = "testing";
                string htmlMessage = @"<p>Se hai richiesto il recupero della password, clicca il seguente link, altrimenti ignora la mail:</p>
                <a href='http://localhost:3000/resetpassword/" + username + "/" + token + "'> Reimposta password</a>";

                await sender.SendEmailAsync(SmtpOptions.CurrentValue.RecoveryAddress, subject, htmlMessage);

                return;
            }
            catch
            {
                return;
            }
        }


        public async Task<bool> ResetPasswordAsync(ResetPasswordInputModel input)
        {
            try
            {
                IdentityUser user = await UserManager.FindByNameAsync(input.Username);
                var result = await UserManager.ResetPasswordAsync(user, input.Token, input.Password);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<QueryResModel<string>> SignIn(string username, string password)
        {
            QueryResModel<string> result = new QueryResModel<string>();

            IdentityUser user = await UserManager.FindByNameAsync(username).ConfigureAwait(false);

            if (user != null && await UserManager.CheckPasswordAsync(user, password).ConfigureAwait(false))
            {
                List<Claim> claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, username),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                var roles = await UserManager.GetRolesAsync(user).ConfigureAwait(false);
                foreach (var role in roles)
                {
                    var roleClaim = new Claim(ClaimTypes.Role, role);
                    claims.Add(roleClaim);
                }


                var secretByte = Encoding.UTF8.GetBytes(AuthOptions.CurrentValue.Secret);
                var key = new SymmetricSecurityKey(secretByte);

                var algorithm = SecurityAlgorithms.HmacSha256;

                var signingCredentials = new SigningCredentials(key, algorithm);

                var token = new JwtSecurityToken(
                    AuthOptions.CurrentValue.Issuer,
                    AuthOptions.CurrentValue.Audience,
                    claims: claims,
                    notBefore: DateTime.Now,
                    expires: DateTime.Now.AddHours(1),
                    signingCredentials: signingCredentials
                    );

                var tokenJson = new JwtSecurityTokenHandler().WriteToken(token);

                result.Succeeded = true;
                result.Data = tokenJson;
            }
            else
            {
                result.Error = "Error: Username and password do not match an existing account.";
            }

            return result;
        }

        public async Task SignInCookie(HttpContext httpContext, string username, string password)
        {
            IdentityUser user = await UserManager.FindByNameAsync(username).ConfigureAwait(false);

            if (user != null && await UserManager.CheckPasswordAsync(user, password).ConfigureAwait(false))
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.Role, "Administrator")
                };

                var roles = await UserManager.GetRolesAsync(user).ConfigureAwait(false);
                foreach (var role in roles)
                {
                    var roleClaim = new Claim(ClaimTypes.Role, role);
                    claims.Add(roleClaim);
                }

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var principal = new ClaimsPrincipal(identity);
                var authProperties = new AuthenticationProperties
                {
                    AllowRefresh = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30),
                    IsPersistent = true,
                };

                await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authProperties);
            }
        }
    }
}
