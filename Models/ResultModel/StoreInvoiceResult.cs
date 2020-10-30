using FluentValidation.Results;

namespace AspNET.Models.ResultModel
{
    public class StoreInvoiceResult
    {
        public StoreInvoiceResult(ReadInvoiceResult result)
        {
            this.InvoiceIsValid = result.InvoiceIsValid;
            this.IsForOwner = result.IsForOwner;
            this.FileIsValid = result.FileIsValid;
            this.FileName = result.FileName;
            this.ValidationResult = result.ValidationResult;
        }
        public bool InvoiceIsValid { get; set; }
        public bool IsForOwner { get; set; }
        public bool FileIsValid { get; set; }
        public string FileName { get; set; }
        public ValidationResult ValidationResult { get; set; }

    }
}
