using AspNET.Customizations.ModelBinders;
using Microsoft.AspNetCore.Mvc;
using System;

namespace AspNET.Models.InputModels
{
    [ModelBinder(BinderType = typeof(ProductReportInputModelBinder))]
    public class ProductReportInputModel
    {
        public string CedentePrestatore { get; }
        public int CedentePrestatoreId { get; }
        public DateTime Begin { get; }
        public DateTime End { get; }
        public string Products { get; }

        public ProductReportInputModel(
            string cedentePrestatore, int cedentePrestatoreId,
            DateTime begin, DateTime end,
            string products
            )
        {
            this.Begin = new DateTime();
            this.End = new DateTime();
            this.CedentePrestatoreId = cedentePrestatoreId;
            this.CedentePrestatore = cedentePrestatore;
            this.Begin = begin == DateTime.MinValue ? new DateTime(DateTime.Now.Year, 1, 1) : begin;
            this.End = end == DateTime.MinValue ? new DateTime(DateTime.Now.Year + 1, 1, 1) : end;
            this.Products = products;

        }

        public void Deconstruct(out string cedentePrestatore, out int cedentePrestatoreId,
                                out DateTime begin, out DateTime end, out string products)
        {
            cedentePrestatore = this.CedentePrestatore;
            cedentePrestatoreId = this.CedentePrestatoreId;
            begin = this.Begin;
            end = this.End;
            products = this.Products;
        }
    }
}
