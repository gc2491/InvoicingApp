using AspNET.Models.Options.Invoices;

namespace AspNET.Models.Options
{
    public class InvoicesOptions
    {
        public OwnerOptions Owner { get; set; }
        public int InvoicesPerPage { get; set; }
        public int PaymentsPerPage { get; set; }
    }
}
