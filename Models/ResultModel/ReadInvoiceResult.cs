using FatturaElettronica.Ordinaria;
using FatturaElettronica.Semplificata;
using FluentValidation.Results;

namespace AspNET.Models.ResultModel
{
    public class ReadInvoiceResult
    {
        public bool FileIsValid { get; set; } = true;
        public bool InvoiceIsValid { get; set; }
        public bool IsForOwner { get; set; }
        public string FileName { get; set; }
        public FatturaOrdinaria Ordinaria { get; set; }
        public FatturaSemplificata Semplificata { get; set; }
        public ValidationResult ValidationResult { get; set; }
    }
}
