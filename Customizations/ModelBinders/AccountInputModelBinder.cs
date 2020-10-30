using AspNET.Models.InputModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Threading.Tasks;

namespace AspNET.Customizations.ModelBinders
{
    public class AccountInputModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            string username = bindingContext.ValueProvider.GetValue("username").FirstValue;
            string oldPassword = bindingContext.ValueProvider.GetValue("oldPassword").FirstValue;
            string password = bindingContext.ValueProvider.GetValue("password").FirstValue;
            string confirmPassword = bindingContext.ValueProvider.GetValue("confirmpassword").FirstValue;
            string role = bindingContext.ValueProvider.GetValue("role").FirstValue;

            if(role != "admin" && role != "user") role = "user";

            var input = new AccountInputModel(username, oldPassword, password, confirmPassword, role);

            bindingContext.Result = ModelBindingResult.Success(input);

            return Task.CompletedTask;
        }
    }
}
