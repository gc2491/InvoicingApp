using System.Collections.Generic;

namespace AspNET.Models.ResultModel
{
    public class StoreInvoiceResultDTO
    {
        public int InvoicesStored { get; set; } = 0;
        public int InvoicesRejected { get; set; } = 0;
        public List<StoreInvoiceResult> results { get; set; }

        public void Add(StoreInvoiceResult result)
        {
            results.Add(result);

            if (result.FileIsValid && result.InvoiceIsValid && result.IsForOwner)
            {
                this.InvoicesStored++;
            }
            else
            {
                this.InvoicesRejected++;
            }
        }
    }
}