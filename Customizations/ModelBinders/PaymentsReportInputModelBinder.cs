using AspNET.Models.InputModels;
using AspNET.Models.Options;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace AspNET.Customizations.ModelBinders
{
    public class PaymentsReportInputModelBinder : IModelBinder
    {
        public IOptionsMonitor<InvoicesOptions> InvoicesOptions { get; }

        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            string clifor = bindingContext.ValueProvider.GetValue("clifor").FirstValue;
            int cliforId = Convert.ToInt32(bindingContext.ValueProvider.GetValue("cliforId").FirstValue);
            DateTime begin = Convert.ToDateTime(bindingContext.ValueProvider.GetValue("begin").FirstValue);
            DateTime end = Convert.ToDateTime(bindingContext.ValueProvider.GetValue("end").FirstValue);
            bool emitted = Convert.ToBoolean(bindingContext.ValueProvider.GetValue("emitted").FirstValue);

            var input = new PaymentsReportInputModel(clifor, cliforId, begin, end, emitted);

            bindingContext.Result = ModelBindingResult.Success(input);

            return Task.CompletedTask;
        }
    }
}
