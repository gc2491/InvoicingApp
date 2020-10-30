using AspNET.Models.InputModels;
using AspNET.Models.Options;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace AspNET.Customizations.ModelBinders
{
    public class PagamentiInputModelBinder : IModelBinder
    {
        public PagamentiInputModelBinder(IOptionsMonitor<InvoicesOptions> options)
        {
            this.InvoicesOptions = options;
        }

        public IOptionsMonitor<InvoicesOptions> InvoicesOptions { get; }

        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            string Name = bindingContext.ValueProvider.GetValue("name").FirstValue;
            DateTime? EndDate = Convert.ToDateTime(bindingContext.ValueProvider.GetValue("endDate").FirstValue);
            bool Emitted = Convert.ToBoolean(bindingContext.ValueProvider.GetValue("emitted").FirstValue);
            int Page = Convert.ToInt32(bindingContext.ValueProvider.GetValue("page").FirstValue);
            bool Ascending = Convert.ToBoolean(bindingContext.ValueProvider.GetValue("ascending").FirstValue);

            var input = new PagamentiInputModel(Name, EndDate, Emitted, Page, Ascending, InvoicesOptions.CurrentValue);

            bindingContext.Result = ModelBindingResult.Success(input);

            return Task.CompletedTask;
        }
    }
}
