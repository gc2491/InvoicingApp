using AspNET.Models.InputModels;
using AspNET.Models.Options;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace AspNET.Customizations.ModelBinders
{
    public class BodiesInputModelBinder : IModelBinder
    {
        public BodiesInputModelBinder(IOptionsMonitor<InvoicesOptions> options)
        {
            this.InvoicesOptions = options;
        }

        public IOptionsMonitor<InvoicesOptions> InvoicesOptions { get; }

        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            DateTime begin = Convert.ToDateTime(bindingContext.ValueProvider.GetValue("begin").FirstValue);
            DateTime end = Convert.ToDateTime(bindingContext.ValueProvider.GetValue("end").FirstValue);
            string name = bindingContext.ValueProvider.GetValue("name").FirstValue;
            string number = bindingContext.ValueProvider.GetValue("number").FirstValue;
            bool emitted = Convert.ToBoolean(bindingContext.ValueProvider.GetValue("emitted").FirstValue);
            bool ascending = Convert.ToBoolean(bindingContext.ValueProvider.GetValue("ascending").FirstValue);
            int page = Convert.ToInt32(bindingContext.ValueProvider.GetValue("page").FirstValue);

            var inputModel = new BodiesInputModel(begin, end, name, number, emitted, ascending, page, InvoicesOptions.CurrentValue);

            bindingContext.Result = ModelBindingResult.Success(inputModel);

            return Task.CompletedTask;
        }
    }
}
