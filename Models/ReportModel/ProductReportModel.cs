using System;

namespace AspNET.Models.ReportModel
{
    public class ProductReportModel
    {
        public string Descrizione { get; set; }
        public decimal? Quantita { get; set; }
        public string UnitaMisura { get; set; }
        public decimal PrezzoUnitario { get; set; }
        public decimal PrezzoTotale { get; set; }
        public DateTime DataDDT { get; set; }
        public string NumeroDDT { get; set; }
        // Invoice info
        public DateTime Data { get; set; }
        public string Numero { get; set; }
    }
}
