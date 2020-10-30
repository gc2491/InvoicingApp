using AspNET.Customizations.ModelBinders;
using Microsoft.AspNetCore.Mvc;
using System;

namespace AspNET.Models.InputModels
{
    [ModelBinder(BinderType = typeof(PaymentsReportInputModelBinder))]
    public class PaymentsReportInputModel
    {
        public PaymentsReportInputModel(
            string clifor, int cliforId,
            DateTime begin, DateTime end, bool emitted
            )
        {
            this.Begin = new DateTime();
            this.End = new DateTime();
            this.Clifor = clifor;
            this.CliforId = cliforId;
            this.Begin = begin == DateTime.MinValue ? new DateTime(DateTime.Now.Year, 1, 1) : begin;
            this.End = end == DateTime.MinValue ? new DateTime(DateTime.Now.Year + 1, 1, 1) : end;
            this.Emitted = emitted;

        }

        public string Clifor { get; }
        public int CliforId { get; }
        public DateTime Begin { get; }
        public DateTime End { get; }
        public bool Emitted { get; }
    }
}
