using AspNET.Customizations.ModelBinders;
using AspNET.Models.Options;
using Microsoft.AspNetCore.Mvc;
using System;

namespace AspNET.Models.InputModels
{
    [ModelBinder(BinderType = typeof(CliForsInputModelBinder))]
    public class CliForsInputModel
    {
        public CliForsInputModel(
            string partialName,
            int page, bool ascending,
            InvoicesOptions options)
        {
            if (partialName == null) this.PartialName = "";
            else this.PartialName = partialName.ToLower();
            this.Page = Math.Max(1, page);
            this.Limit = options.InvoicesPerPage;
            this.Offset = (this.Page - 1) * this.Limit;
            this.Ascending = ascending;
        }

        public string PartialName { get; }
        public int Page { get; }
        public bool Ascending { get; }
        public int Offset { get; }
        public int Limit { get; }
    }
}
