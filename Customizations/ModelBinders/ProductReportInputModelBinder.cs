using AspNET.Models.InputModels;
using AspNET.Models.Options;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace AspNET.Customizations.ModelBinders
{
    public class ProductReportInputModelBinder : IModelBinder
    {
        public IOptionsMonitor<InvoicesOptions> InvoicesOptions { get; }

        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            string cedentePrestatore = bindingContext.ValueProvider.GetValue("cedentePrestatore").FirstValue;
            int cedentePrestatoreId = Convert.ToInt32(bindingContext.ValueProvider.GetValue("cedentePrestatoreId").FirstValue);
            DateTime begin = Convert.ToDateTime(bindingContext.ValueProvider.GetValue("begin").FirstValue);
            DateTime end = Convert.ToDateTime(bindingContext.ValueProvider.GetValue("end").FirstValue);
            string products = bindingContext.ValueProvider.GetValue("products").FirstValue;

            var input = new ProductReportInputModel(cedentePrestatore, cedentePrestatoreId, begin, end, products);

            bindingContext.Result = ModelBindingResult.Success(input);

            return Task.CompletedTask;
        }
    }
}
