using AspNET.Customizations.ModelBinders;
using AspNET.Models.Options;
using Microsoft.AspNetCore.Mvc;
using System;

namespace AspNET.Models.InputModels
{
    [ModelBinder(BinderType = typeof(PagamentiInputModelBinder))]
    public class PagamentiInputModel
    {
        public PagamentiInputModel(string name, DateTime? endDate, bool emitted, int page, bool ascending, InvoicesOptions options)
        {
            this.Name = name;
            this.Emitted = emitted;

            if (endDate == DateTime.MinValue)
            {
                this.EndDate = null;
            }
            else
            {
                this.EndDate = endDate;

            }

            this.Page = Math.Max(1, page);
            this.Limit = options.PaymentsPerPage;
            this.Offset = (this.Page - 1) * this.Limit;

            this.Ascending = ascending;
        }

        public string Name { get; }
        public DateTime? EndDate { get; }
        public bool Emitted { get; }
        public int Page { get; }
        public int Limit { get; }
        public int Offset { get; }
        public bool Ascending { get; }
    }
}
