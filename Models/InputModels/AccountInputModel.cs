using AspNET.Customizations.ModelBinders;
using Microsoft.AspNetCore.Mvc;

namespace AspNET.Models.InputModels
{
    [ModelBinder(BinderType = typeof(AccountInputModelBinder))]
    public class AccountInputModel
    {
        public AccountInputModel(string username, string oldPassword, string password, string confirmPassword, string role)
        {
            this.Username = username;
            this.OldPassword = oldPassword;
            this.Password = password;
            this.ConfirmPassword = confirmPassword;
            this.Role = role;
        }

        public string Username { get; set; }
        public string OldPassword {get;set;}
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Role { get; set; }
    }
}
