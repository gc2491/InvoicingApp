using AspNET.Models.InputModels;
using AspNET.Models.InvoiceDTO;
using AspNET.Models.InvoiceDTO.Body.Pagamenti;
using AspNET.Models.InvoiceModel;
using AspNET.Models.ResultModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AspNET.Models.InvoiceDTO.Header;
using AspNET.Models.InvoiceModel.Header;
using AspNET.Models.InvoiceModel.Body.Pagamenti;
using AspNET.Models.InvoiceDTO.Body;
using AspNET.Models.InvoiceModel.Body;

namespace AspNET.Models.Services.Invoices
{
    interface IInvoiceService
    {
        Task<BodyDTO> GetOneBodyAsync(int id);
        Task<ManyBodyDTO> GetBodiesAsync(BodiesInputModel input);
        Task<StoreInvoiceResultDTO> StoreInvoiceAsync();
        Task<QueryResModel<BodyDTO>> UpdateAsync(BodyModel invoice);
        Task<bool> DeleteBodyAsync(int id);

        Task<List<DatiPagamentoDTO>> AddAsync(DatiPagamentoModel dp);
        Task<DatiPagamentoDTO> UpdateDatiPagamentoAsync(DatiPagamentoModel dp);
        Task<bool> DeleteDatiPagamentoAsync(int id);
        Task<bool> ChangeActiveDatiPagamentoAsync(int datiPagamentoModelId, int bodyModelId);

        Task<QueryResModel<ManyDettaglioPagamentoDTO>> GetPaymentsAsync(PagamentiInputModel input);
        Task<DateTime?> SetPaymentStatusAsync(int id);
        Task<List<DettaglioPagamentoDTO>> AddAsync(DettaglioPagamentoModel dp);
        Task<DettaglioPagamentoDTO> UpdateDettaglioPagamentoAsync(DettaglioPagamentoModel dp);
        Task<bool> DeleteDettaglioPagamentoAsync(int id);

        Task<QueryResModel<CliForDTO>> GetOneCliForAsync(int id);
        Task<QueryResModel<ManyCliForDTO>> GetCliForsAsync(CliForsInputModel input);
        Task<QueryResModel<Dictionary<string, int>>> GetCPIds(string name);
        Task<QueryResModel<CliForDTO>> UpdateAsync(CliForModel cliFor);
        Task<bool> DeleteCliforAsync(int id);

        Task<QueryResModel<List<ContattiDTO>>> GetContacts(int cliforId);
        Task<bool> AddAsync(ContattiModel contatto);
        Task<bool> UpdateAsync(ContattiModel contatto);
        Task<bool> DeleteContattoAsync(int id);

        Task<QueryResModel<List<ContiBancariDTO>>> GetContiBancariAsync(int cliforId);
        Task<bool> AddAsync(ContiBancariModel conto);
        Task<bool> UpdateAsync(ContiBancariModel conto);
        Task<bool> DeleteContoAsync(int id);

        Task<bool> AddAsync(SediModel sede);
        Task<bool> UpdateAsync(SediModel sede);
        Task<bool> DeleteSedeAsync(int id);
    }
}
