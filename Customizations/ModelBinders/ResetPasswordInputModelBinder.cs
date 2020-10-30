using AspNET.Models.InputModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Threading.Tasks;

namespace AspNET.Customizations.ModelBinders
{
    public class ResetPasswordInputModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            string username = bindingContext.ValueProvider.GetValue("username").FirstValue;
            string token = bindingContext.ValueProvider.GetValue("token").FirstValue;
            string password = bindingContext.ValueProvider.GetValue("password").FirstValue;
            string confirmPassword = bindingContext.ValueProvider.GetValue("confirmPassword").FirstValue;

            var input = new ResetPasswordInputModel(username, token, password, confirmPassword);

            bindingContext.Result = ModelBindingResult.Success(input);

            return Task.CompletedTask;
        }
    }
}
