using AspNET.Customizations.ModelBinders;
using AspNET.Models.Options;
using Microsoft.AspNetCore.Mvc;
using System;

namespace AspNET.Models.InputModels
{
    [ModelBinder(BinderType = typeof(BodiesInputModelBinder))]
    public class BodiesInputModel
    {
        public DateTime Begin { get; }
        public DateTime End { get; }
        public string Name { get; }
        public string Number { get; }
        public bool Emitted { get; }
        public bool Ascending { get; set; }
        public int Page { get; }
        public int Limit { get; }
        public int Offset { get; }

        public BodiesInputModel(
            DateTime begin, DateTime end,
            string name,
            string number,
            bool emitted,
            bool ascending,
            int page,
            InvoicesOptions options)
        {
            this.Begin = begin == DateTime.MinValue ? new DateTime(DateTime.Now.Year, 1, 1) : begin;
            this.End = end == DateTime.MinValue ? new DateTime(DateTime.Now.Year + 1, 1, 1) : end;

            this.Name = name;
            this.Number = number;

            if (page < 1) this.Page = 1;
            else this.Page = page;

            this.Page = Math.Max(1, page);
            this.Limit = options.InvoicesPerPage;
            this.Offset = (this.Page - 1) * this.Limit;

            this.Emitted = emitted;
            this.Ascending = ascending;
        }

        public void Deconstruct(
                out DateTime begin, out DateTime end,
                out string name, out string number,
                out bool emitted, out bool ascending,
                out int page, out int limit, out int offset)
        {
            begin = this.Begin;
            end = this.End;
            name = this.Name;
            number = this.Number;
            emitted = this.Emitted;
            ascending = this.Ascending;
            page = this.Page;
            limit = this.Limit;
            offset = this.Offset;
        }
    }
}
