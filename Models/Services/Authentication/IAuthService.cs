using AspNET.Models.InputModels;
using AspNET.Models.ResultModel;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspNET.Models.Services.Authentication
{
    interface IAuthService
    {
        QueryResModel<UserAuth> Auth(string token);
        Task<IdentityResult> Register(AccountInputModel input);
        Task<IdentityResult> UpdateSecurity(AccountInputModel account);
        Task RecoverAccountAsync(string username);
        Task<QueryResModel<string>> SignIn(string username, string password);
        Task<QueryResModel<List<string>>> GetRoles();
    }
}
