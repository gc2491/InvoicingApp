using AspNET.Models.InputModels;
using AspNET.Models.Options;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace AspNET.Customizations.ModelBinders
{
    public class CliForsInputModelBinder : IModelBinder
    {
        public CliForsInputModelBinder(IOptionsMonitor<InvoicesOptions> options)
        {
            this.InvoicesOptions = options;
        }

        public IOptionsMonitor<InvoicesOptions> InvoicesOptions { get; }

        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            string PartialName = bindingContext.ValueProvider.GetValue("partialname").FirstValue;
            int Page = Convert.ToInt32(bindingContext.ValueProvider.GetValue("page").FirstValue);
            bool Ascending = Convert.ToBoolean(bindingContext.ValueProvider.GetValue("ascending").FirstValue);

            var input = new CliForsInputModel(PartialName, Page, Ascending, InvoicesOptions.CurrentValue);

            bindingContext.Result = ModelBindingResult.Success(input);

            return Task.CompletedTask;
        }
    }
}
