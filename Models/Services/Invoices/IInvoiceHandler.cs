using System.Collections.Generic;
using System.Threading.Tasks;
using AspNET.Models.InvoiceModel;
using AspNET.Models.ResultModel;
using FatturaElettronica.Common;

namespace AspNET.Models.Services.Invoices
{
    interface IInvoiceHandler
    {
        List<ReadInvoiceResult> ReadInvoices();
        void MoveFiles(List<ReadInvoiceResult> invoices);

        Task<int?> AddCliForAsync(CliForModel cliFor);
        bool EitherIsOwner(IdFiscaleIVA cliFor1, IdFiscaleIVA cliFor2);
        bool EitherIsOwner(CliForModel cliFor1, CliForModel cliFor2);
        bool SameIdIvaOrFiscale(CliForModel cliFor1, CliForModel cliFor2);
    }
}
