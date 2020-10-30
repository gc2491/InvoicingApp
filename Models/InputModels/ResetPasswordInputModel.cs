using AspNET.Customizations.ModelBinders;
using Microsoft.AspNetCore.Mvc;

namespace AspNET.Models.InputModels
{
    [ModelBinder(BinderType = typeof(ResetPasswordInputModelBinder))]
    public class ResetPasswordInputModel
    {
        public ResetPasswordInputModel(
            string username, string token,
             string password, string confirmPassword
            )
        {
            this.Username = username;
            this.Token = token;
            this.Password = password;
            this.ConfirmPassword = confirmPassword;
        }

        public string Username { get; }
        public string Token { get; }
        public string Password { get; }
        public string ConfirmPassword { get; }
    }
}
