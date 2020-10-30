using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNET.Models.InputModels;
using AspNET.Models.InvoiceDTO;
using AspNET.Models.InvoiceDTO.Body;
using AspNET.Models.InvoiceDTO.Body.Pagamenti;
using AspNET.Models.InvoiceDTO.Header;
using AspNET.Models.InvoiceModel;
using AspNET.Models.InvoiceModel.Body;
using AspNET.Models.InvoiceModel.Body.Pagamenti;
using AspNET.Models.InvoiceModel.Header;
using AspNET.Models.Options;
using AspNET.Models.ResultModel;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace AspNET.Models.Services.Invoices
{
    public class InvoiceService : IInvoiceService
    {
        public InvoiceDbContext DbContext { get; }
        public InvoicesOptions InvoicesOptions { get; }
        public int OwnerId { get; }
        public IMapper Mapper { get; }
        public InvoiceHandler InvoiceHandler { get; }

        public InvoiceService(
            IOptionsMonitor<InvoicesOptions> invoicesOptions,
            InvoiceDbContext dbContext,
            IMapper mapper)
        {
            this.InvoicesOptions = invoicesOptions.CurrentValue;
            this.DbContext = dbContext;
            this.Mapper = mapper;
            this.InvoiceHandler = new InvoiceHandler(DbContext, InvoicesOptions);
            this.OwnerId = DbContext.Clifor
                .Where(cf => cf.IdPaese == InvoicesOptions.Owner.IdPaese &&
                            cf.IdCodice == InvoicesOptions.Owner.IdCodice)
                .Select(cf => cf.Id)
                .FirstOrDefault();
        }

        public async Task<BodyDTO> GetOneBodyAsync(int id)
        {
            BodyModel invoice = await DbContext.Bodies
                .Where(b => b.Id == id)
                .Include(b => b.Causale)
                .Include(b => b.DatiDDT)
                .ThenInclude(ddt => ddt.RiferimentoNumeroLinea)
                .Include(b => b.DatiPagamento)
                .ThenInclude(dp => dp.DettaglioPagamento)
                .Include(b => b.DatiRiepilogo)
                .Include(b => b.DettaglioLinee)
                .ThenInclude(dl => dl.CodiceArticolo)
                .Include(b => b.DettaglioLinee)
                .ThenInclude(dl => dl.ScontoMaggiorazione)
                .Include(b => b.CedentePrestatore)
                .Include(b => b.CessionarioCommittente)
                .Include(b => b.Metadata)
                .Include(b => b.Vettore)
                .AsNoTracking()
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);

            if (invoice.Id != 0)
            {
                invoice.CedentePrestatore.BodyModelCP = null;
                invoice.CedentePrestatore.BodyModelCC = null;
                invoice.CessionarioCommittente.BodyModelCP = null;
                invoice.CessionarioCommittente.BodyModelCC = null;
            }

            BodyDTO bodyDTO = Mapper.Map<BodyModel, BodyDTO>(invoice);

            return bodyDTO;
        }

        public async Task<ManyBodyDTO> GetBodiesAsync(BodiesInputModel input)
        {
            var (begin, end, name, number, emitted, ascending, page, limit, offset) = input;

            IQueryable<BodyModel> getBodiesQuery = DbContext.Bodies
                .Where(b => b.Data >= begin && b.Data <= end)
                .Include(b => b.Causale)
                .Include(b => b.DatiDDT)
                    .ThenInclude(ddt => ddt.RiferimentoNumeroLinea)
                .Include(b => b.DatiPagamento)
                    .ThenInclude(dp => dp.DettaglioPagamento)
                .Include(b => b.DatiRiepilogo)
                .Include(b => b.DettaglioLinee)
                    .ThenInclude(dl => dl.CodiceArticolo)
                .Include(b => b.DettaglioLinee)
                    .ThenInclude(dl => dl.ScontoMaggiorazione)
                .Include(b => b.DatiBeniServizi)
                .Include(b => b.CedentePrestatore)
                    .ThenInclude(cp => cp.ContiBancari)
                .Include(b => b.CessionarioCommittente)
                .AsNoTracking();

            if (ascending) getBodiesQuery = getBodiesQuery
                .OrderBy(b => b.Data)
                .ThenBy(b => b.Id);
            else getBodiesQuery = getBodiesQuery
                .OrderByDescending(b => b.Data)
                .ThenBy(b => b.Id);

            if (number != null)
            {
                getBodiesQuery = getBodiesQuery.Where(b => b.Numero == number);
            }

            if (emitted)
            {
                getBodiesQuery = getBodiesQuery
                    .Where(b => b.CedentePrestatoreId == OwnerId);

                if (name != null) getBodiesQuery = getBodiesQuery
                    .Where(b => b.CessionarioCommittente.Denominazione
                       .Contains(name) ||
                       (b.CessionarioCommittente.Nome + " " + b.CessionarioCommittente.Cognome)
                       .Contains(name));
            }
            else
            {
                getBodiesQuery = getBodiesQuery
                    .Where(b => b.CessionarioCommittenteId == OwnerId);

                if (name != null) getBodiesQuery = getBodiesQuery
                    .Where(b => b.CedentePrestatore.Denominazione
                       .Contains(name) ||
                       (b.CedentePrestatore.Nome + " " + b.CedentePrestatore.Cognome)
                       .Contains(name));
            }

            List<BodyModel> getBodiesModel = await getBodiesQuery
                .Skip(offset)
                .Take(limit)
                .ToListAsync()
                .ConfigureAwait(false);

            ManyBodyDTO getBodiesDTO = new ManyBodyDTO();

            int bodiesFound = await getBodiesQuery
                .CountAsync()
                .ConfigureAwait(false);

            getBodiesDTO.TotalPages = (int)Math.Ceiling((double)bodiesFound / InvoicesOptions.InvoicesPerPage);

            foreach (var bodiesElement in getBodiesModel)
            {
                bodiesElement.CedentePrestatore.BodyModelCP = null;
                bodiesElement.CedentePrestatore.BodyModelCC = null;
                bodiesElement.CessionarioCommittente.BodyModelCP = null;
                bodiesElement.CessionarioCommittente.BodyModelCC = null;

                bodiesElement.DatiDDT = bodiesElement.DatiDDT
                    .OrderBy(ddt => ddt.DataDDT)
                    .ThenBy(ddt => ddt.NumeroDDT)
                    .ToList();

                bodiesElement.DettaglioLinee = bodiesElement.DettaglioLinee
                    .OrderBy(dl => dl.NumeroLinea)
                    .ToList();

                getBodiesDTO.Bodies.Add(Mapper.Map<BodyModel, BodyDTO>(bodiesElement));
            }

            foreach (var b in getBodiesDTO.Bodies)
            {
                b.DettaglioLinee = b.DettaglioLinee
                    .OrderBy(dl => dl.NumeroLinea)
                    .ToList();
            }

            return getBodiesDTO;
        }

        public async Task<StoreInvoiceResultDTO> StoreInvoiceAsync()
        {
            List<ReadInvoiceResult> readingResults = InvoiceHandler.ReadInvoices();

            InvoiceHandler.MoveFiles(readingResults);

            List<MetadataModel> invoices = new List<MetadataModel>();

            foreach (var result in readingResults)
            {
                if (result.FileIsValid && result.InvoiceIsValid && result.IsForOwner)
                {
                    invoices.Add(new MetadataModel(result, InvoicesOptions.Owner));
                }
            }

            foreach (var invoice in invoices)
            {
                int supplierId = await InvoiceHandler
                    .AddCliForAsync(invoice.Bodies[0].CedentePrestatore)
                    .ConfigureAwait(false) ?? 0;

                int clientId = await InvoiceHandler
                    .AddCliForAsync(invoice.Bodies[0].CessionarioCommittente)
                    .ConfigureAwait(false) ?? 0;

                if (invoice.Trasmittente != null)
                {
                    invoice.TrasmittenteId = await InvoiceHandler
                        .AddCliForAsync(invoice.Trasmittente)
                        .ConfigureAwait(false) ?? 0;

                    invoice.Trasmittente = null;
                }

                if (invoice.IntermediarioOEmittente != null)
                {
                    invoice.IntermediarioOEmittenteId = await InvoiceHandler
                        .AddCliForAsync(invoice.IntermediarioOEmittente)
                        .ConfigureAwait(false);

                    invoice.IntermediarioOEmittente = null;
                }

                List<int> bodiesToBeRemoved = new List<int>();

                for (int i = 0; i < invoice.Bodies.Count; i++)
                {
                    var body = invoice.Bodies[i];

                    bool invoiceNotInDB = !await DbContext.Bodies
                                        .AnyAsync(b => b.CedentePrestatoreId == supplierId &&
                                           b.Numero == body.Numero &&
                                           b.Data == body.Data)
                                        .ConfigureAwait(false);

                    if (invoiceNotInDB)
                    {
                        body.CedentePrestatoreId = supplierId;
                        body.CedentePrestatore = null;
                        body.CessionarioCommittenteId = clientId;
                        body.CessionarioCommittente = null;

                        if (body.Vettore != null)
                        {
                            body.VettoreId = await InvoiceHandler
                                .AddCliForAsync(body.Vettore)
                                .ConfigureAwait(false);

                            body.Vettore = null;
                        }
                    }
                    else
                    {
                        invoice.Bodies[i].Id = i;
                        bodiesToBeRemoved.Add(invoice.Bodies[i].Id);
                    }
                }

                foreach (var id in bodiesToBeRemoved)
                {
                    invoice.Bodies.RemoveAll(x => x.Id == id);
                }

                if (invoice.Bodies.Count > 0)
                {
                    DbContext.Metadata.Add(invoice);
                    await DbContext
                        .SaveChangesAsync()
                        .ConfigureAwait(false);
                }
            }

            StoreInvoiceResultDTO resultDTO = new StoreInvoiceResultDTO();
            resultDTO.results = new List<StoreInvoiceResult>();

            foreach (var result in readingResults)
            {
                resultDTO.Add(new StoreInvoiceResult(result));
            }

            return resultDTO;
        }

        public async Task<QueryResModel<BodyDTO>> UpdateAsync(BodyModel invoice)
        {
            QueryResModel<BodyDTO> queryResult = new QueryResModel<BodyDTO>();

            int changes = -1;
            if (await DbContext.Bodies.AnyAsync(b => b.Id == invoice.Id).ConfigureAwait(false))
            {
                DbContext.Bodies.Update(invoice);
                changes = await DbContext.SaveChangesAsync().ConfigureAwait(false);
            }

            if (changes <= 0)
            {
                queryResult.Succeeded = false;
                queryResult.Error = changes == -1 ? "Error: invoice not found" : "Error: Update failed.";
                return queryResult;
            }
            else
            {
                BodyModel bodyModel = await DbContext.Bodies
                    .Where(cf => cf.Id == invoice.Id)
                    .AsNoTracking()
                    .FirstAsync()
                    .ConfigureAwait(false);

                queryResult.Data = Mapper.Map<BodyModel, BodyDTO>(bodyModel);

                return queryResult;
            }
        }

        public async Task<bool> DeleteBodyAsync(int id)
        {
            if (await DbContext.Bodies.AnyAsync(b => b.Id == id).ConfigureAwait(false))
            {
                BodyModel bodyDelete = await DbContext.Bodies
                    .Where(b => b.Id == id)
                    .FirstAsync()
                    .ConfigureAwait(false);

                DbContext.Remove(bodyDelete);
                await DbContext
                    .SaveChangesAsync()
                    .ConfigureAwait(false);

                return true;
            }
            return false;
        }

        public async Task<DatiPagamentoDTO> UpdateDatiPagamentoAsync(DatiPagamentoModel dp)
        {
            if (await DbContext.DatiPagamento.AnyAsync(pagamento => pagamento.Id == dp.Id).ConfigureAwait(false))
            {
                DbContext.DatiPagamento.Update(dp);
                int changes = await DbContext
                    .SaveChangesAsync()
                    .ConfigureAwait(false);

                if (changes == 1)
                {
                    DatiPagamentoModel updated = await DbContext.DatiPagamento
                        .Where(pagamento => pagamento.Id == dp.Id)
                        .FirstOrDefaultAsync()
                        .ConfigureAwait(false);

                    return Mapper.Map<DatiPagamentoModel, DatiPagamentoDTO>(updated);
                }
            }

            return new DatiPagamentoDTO();
        }

        public async Task<List<DatiPagamentoDTO>> AddAsync(DatiPagamentoModel dp)
        {
            if (dp.BodyModelId < 1)
            {
                return new List<DatiPagamentoDTO>();
            }

            await DbContext.DatiPagamento
                .AddAsync(dp)
                .ConfigureAwait(false);

            await DbContext
                .SaveChangesAsync()
                .ConfigureAwait(false);

            var datiPagamento = await DbContext.DatiPagamento
                .Where(dati => dati.BodyModelId == dp.BodyModelId)
                .Include(dati => dati.DettaglioPagamento)
                .ToListAsync()
                .ConfigureAwait(false);

            var pagamentiList = new List<DatiPagamentoDTO>();

            foreach (var dati in datiPagamento)
            {
                pagamentiList.Add(Mapper.Map<DatiPagamentoModel, DatiPagamentoDTO>(dati));
            }

            return pagamentiList;
        }

        public async Task<bool> DeleteDatiPagamentoAsync(int id)
        {
            try
            {
                var datiPagamento = await DbContext.DatiPagamento
                    .Where(dp => dp.Id == id)
                    .FirstOrDefaultAsync()
                    .ConfigureAwait(false);

                DbContext.Remove(datiPagamento);

                await DbContext
                    .SaveChangesAsync()
                    .ConfigureAwait(false);

                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        public async Task<bool> ChangeActiveDatiPagamentoAsync(int datiPagamentoModelId, int bodyModelId)
        {
            try
            {
                var datiPagamento = await DbContext.DatiPagamento
                    .Where(dp => dp.BodyModelId == bodyModelId)
                    .ToListAsync()
                    .ConfigureAwait(false);

                if(datiPagamento.Count < 2) return false;

                foreach (var dp in datiPagamento)
                {
                    if(dp.Id == datiPagamentoModelId)
                    {
                        dp.Active = true;
                    }
                    else
                    {
                        dp.Active = false;
                    }

                    DbContext.DatiPagamento.Update(dp);
                }

                await DbContext.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }

        }

        public async Task<QueryResModel<ManyDettaglioPagamentoDTO>> GetPaymentsAsync(PagamentiInputModel input)
        {
            QueryResModel<ManyDettaglioPagamentoDTO> queryResult = new QueryResModel<ManyDettaglioPagamentoDTO>();
            queryResult.Data = new ManyDettaglioPagamentoDTO();
            queryResult.Data.Payments = new List<PaymentsDTO>();

            IQueryable<DettaglioPagamentoModel> paymentsQuery = DbContext.DettaglioPagamento
                .Where(dp => dp.PaymentDate == null && dp.DatiPagamento.Active)
                .OrderBy(dp => dp.DataScadenzaPagamento);

            if (input.EndDate != null)
            {
                paymentsQuery = paymentsQuery
                    .Where(dp => dp.DataScadenzaPagamento <= input.EndDate || dp.DataScadenzaPagamento == null);
            }

            if (input.Emitted)
            {
                paymentsQuery = paymentsQuery
                    .Where(dp => dp.DatiPagamento.BodyModel.CedentePrestatoreId == OwnerId);

                if (input.Name != null) paymentsQuery = paymentsQuery
                    .Where(dp => dp.DatiPagamento.BodyModel.CessionarioCommittente.Denominazione.Contains(input.Name) ||
                       (dp.DatiPagamento.BodyModel.CessionarioCommittente.Nome + " " + dp.DatiPagamento.BodyModel.CessionarioCommittente.Cognome).Contains(input.Name));

            }
            else
            {
                paymentsQuery = paymentsQuery
                    .Where(dp => dp.DatiPagamento.BodyModel.CessionarioCommittenteId == OwnerId);

                if (input.Name != null) paymentsQuery = paymentsQuery
                    .Where(dp => dp.DatiPagamento.BodyModel.CedentePrestatore.Denominazione.Contains(input.Name) ||
                       (dp.DatiPagamento.BodyModel.CedentePrestatore.Nome + " " + dp.DatiPagamento.BodyModel.CedentePrestatore.Cognome).Contains(input.Name));

            }

            List<DettaglioPagamentoModel> payments = await paymentsQuery
                .Skip(input.Offset)
                .Take(input.Limit)
                .ToListAsync()
                .ConfigureAwait(false);

            int nrOfPayments = await paymentsQuery
                .CountAsync()
                .ConfigureAwait(false);

            queryResult.Data.TotalPages = (int)Math.Ceiling((double)nrOfPayments / InvoicesOptions.PaymentsPerPage);

            foreach (var p in payments)
            {
                int cliforId = await DbContext.DettaglioPagamento
                    .Where(dp => dp.Id == p.Id)
                    .Select(dp => input.Emitted ?
                            dp.DatiPagamento.BodyModel.CessionarioCommittenteId :
                            dp.DatiPagamento.BodyModel.CedentePrestatore.Id)
                    .FirstOrDefaultAsync()
                    .ConfigureAwait(false);

                string clifor = await DbContext.Clifor
                    .Where(dp => dp.Id == cliforId)
                    .Select(cf => cf.Denominazione ?? cf.Cognome + " " + cf.Nome)
                    .FirstOrDefaultAsync()
                    .ConfigureAwait(false);

                List<ContiBancariModel> conti = await DbContext.DettaglioPagamento
                    .Where(dp => dp.Id == p.Id)
                    .Select(dp => input.Emitted ?
                            dp.DatiPagamento.BodyModel.CessionarioCommittente.ContiBancari :
                            dp.DatiPagamento.BodyModel.CedentePrestatore.ContiBancari)
                    .FirstOrDefaultAsync()
                    .ConfigureAwait(false);

                string numero = await DbContext.DettaglioPagamento
                    .Where(dp => dp.Id == p.Id)
                    .Select(dp => dp.DatiPagamento.BodyModel.Numero)
                    .FirstOrDefaultAsync()
                    .ConfigureAwait(false);

                DateTime data = await DbContext.DettaglioPagamento
                    .Where(dp => dp.Id == p.Id)
                    .Select(dp => dp.DatiPagamento.BodyModel.Data)
                    .FirstOrDefaultAsync()
                    .ConfigureAwait(false);

                queryResult.Data.Payments.Add(new PaymentsDTO
                {
                    DettaglioPagamento = Mapper.Map<DettaglioPagamentoModel, DettaglioPagamentoDTO>(p),
                    CliforId = cliforId,
                    Clifor = clifor,
                    ContiBancari = Mapper.Map<List<ContiBancariModel>, List<ContiBancariDTO>>(conti),
                    Numero = numero,
                    Data = data
                });
            }

            if (nrOfPayments == 0)
            {
                queryResult.Succeeded = false;
                queryResult.Error = "Resources not found.";
            }

            return queryResult;
        }

        public async Task<DateTime?> SetPaymentStatusAsync(int id)
        {
            if (await DbContext.DettaglioPagamento.AnyAsync(dp => dp.Id == id).ConfigureAwait(false))
            {
                DettaglioPagamentoModel pagamento = await DbContext.DettaglioPagamento
                    .Where(dp => dp.Id == id)
                    .FirstOrDefaultAsync()
                    .ConfigureAwait(false);

                if (pagamento.PaymentDate == null)
                {
                    pagamento.PaymentDate = DateTime.Now;
                }
                else
                {
                    pagamento.PaymentDate = null;
                }

                DbContext.Update(pagamento);
                int res = await DbContext.SaveChangesAsync().ConfigureAwait(false);

                if (res == 1) return pagamento.PaymentDate;
            }
            return DateTime.MinValue;
        }

        public async Task<DettaglioPagamentoDTO> UpdateDettaglioPagamentoAsync(DettaglioPagamentoModel dp)
        {
            if (await DbContext.DettaglioPagamento.AnyAsync(pagamento => pagamento.Id == dp.Id).ConfigureAwait(false))
            {
                DbContext.DettaglioPagamento.Update(dp);
                int changes = await DbContext
                    .SaveChangesAsync()
                    .ConfigureAwait(false);

                if (changes == 1)
                {
                    DettaglioPagamentoModel updated = await DbContext.DettaglioPagamento
                        .Where(pagamento => pagamento.Id == dp.Id)
                        .FirstOrDefaultAsync()
                        .ConfigureAwait(false);

                    return Mapper.Map<DettaglioPagamentoModel, DettaglioPagamentoDTO>(updated);
                }
            }
            return new DettaglioPagamentoDTO();
        }

        public async Task<List<DettaglioPagamentoDTO>> AddAsync(DettaglioPagamentoModel dp)
        {
            if (dp.DatiPagamentoModelId < 1)
            {
                return new List<DettaglioPagamentoDTO>();
            }

            await DbContext.DettaglioPagamento
                .AddAsync(dp)
                .ConfigureAwait(false);

            await DbContext
                .SaveChangesAsync()
                .ConfigureAwait(false);

            var dettagliPagamenti = await DbContext.DatiPagamento
                .Where(dati => dati.Id == dp.DatiPagamentoModelId)
                .Select(dati => dati.DettaglioPagamento)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);

            var pagamentiList = new List<DettaglioPagamentoDTO>();

            foreach (var dettaglio in dettagliPagamenti)
            {
                pagamentiList.Add(Mapper.Map<DettaglioPagamentoModel, DettaglioPagamentoDTO>(dettaglio));
            }

            return pagamentiList;
        }

        public async Task<bool> DeleteDettaglioPagamentoAsync(int id)
        {
            try
            {
                var dettaglioPagamento = await DbContext.DettaglioPagamento
                    .Where(dp => dp.Id == id)
                    .FirstOrDefaultAsync()
                    .ConfigureAwait(false);

                DbContext.Remove(dettaglioPagamento);

                await DbContext
                    .SaveChangesAsync()
                    .ConfigureAwait(false);

                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        // @Clifor
        public async Task<QueryResModel<CliForDTO>> GetOneCliForAsync(int id)
        {
            QueryResModel<CliForDTO> queryResult = new QueryResModel<CliForDTO>();
            CliForModel cliFor = await DbContext.Clifor
                .Where(cf => cf.Id == id)
            .Include(cf => cf.Contatti)
            .Include(cf => cf.ContiBancari)
            .Include(cf => cf.Sedi)
            .AsNoTracking()
            .FirstOrDefaultAsync()
            .ConfigureAwait(false);

            if (cliFor.Id != 0)
            {
                cliFor.BodyModelCP = await DbContext.Bodies
                    .Where(b => b.CedentePrestatoreId == cliFor.Id)
                    .Include(b => b.Causale)
                    .Include(b => b.DatiDDT)
                        .ThenInclude(ddt => ddt.RiferimentoNumeroLinea)
                    .Include(b => b.DatiPagamento)
                        .ThenInclude(dp => dp.DettaglioPagamento)
                    .Include(b => b.DatiRiepilogo)
                    .Include(b => b.DettaglioLinee)
                        .ThenInclude(dl => dl.CodiceArticolo)
                    .Include(b => b.DettaglioLinee)
                        .ThenInclude(dl => dl.ScontoMaggiorazione)
                    .Include(b => b.DatiBeniServizi)
                    .Include(b => b.CedentePrestatore)
                        .ThenInclude(cp => cp.ContiBancari)
                    .Include(b => b.CessionarioCommittente)
                        .ThenInclude(cc => cc.ContiBancari)
                    .OrderByDescending(b => b.Data)
                    .Take(5)
                    .ToListAsync();

                cliFor.BodyModelCC = await DbContext.Bodies
                    .Where(b => b.CessionarioCommittenteId == cliFor.Id)
                    .Include(b => b.Causale)
                    .Include(b => b.DatiDDT)
                        .ThenInclude(ddt => ddt.RiferimentoNumeroLinea)
                    .Include(b => b.DatiPagamento)
                        .ThenInclude(dp => dp.DettaglioPagamento)
                    .Include(b => b.DatiRiepilogo)
                    .Include(b => b.DettaglioLinee)
                        .ThenInclude(dl => dl.CodiceArticolo)
                    .Include(b => b.DettaglioLinee)
                        .ThenInclude(dl => dl.ScontoMaggiorazione)
                    .Include(b => b.DatiBeniServizi)
                    .Include(b => b.CedentePrestatore)
                        .ThenInclude(cp => cp.ContiBancari)
                    .Include(b => b.CedentePrestatore)
                        .ThenInclude(cp => cp.ContiBancari)
                    .OrderByDescending(b => b.Data)
                    .Take(5)
                    .ToListAsync();

                foreach (var inv in cliFor.BodyModelCP)
                {
                    inv.CedentePrestatore = null;
                    inv.CessionarioCommittente.BodyModelCC = null;
                    inv.CessionarioCommittente.BodyModelCP = null;

                    if (inv.ImportoTotaleDocumento == null)
                    {
                        inv.ImportoTotaleDocumento = 0;
                        foreach (var pagamento in inv.DatiPagamento)
                        {
                            foreach (var dettaglio in pagamento.DettaglioPagamento)
                            {
                                inv.ImportoTotaleDocumento += dettaglio.ImportoPagamento;
                            }
                        }
                    }
                }

                foreach (var inv in cliFor.BodyModelCC)
                {
                    inv.CessionarioCommittente = null;
                    inv.CedentePrestatore.BodyModelCC = null;
                    inv.CedentePrestatore.BodyModelCP = null;

                    if (inv.ImportoTotaleDocumento == null)
                    {
                        inv.ImportoTotaleDocumento = 0;
                        foreach (var pagamento in inv.DatiPagamento)
                        {
                            foreach (var dettaglio in pagamento.DettaglioPagamento)
                            {
                                inv.ImportoTotaleDocumento += dettaglio.ImportoPagamento;
                            }
                        }
                    }
                }

                queryResult.Data = Mapper.Map<CliForModel, CliForDTO>(cliFor);
            }
            else
            {
                queryResult.Data = null;
                queryResult.Succeeded = false;
                queryResult.Error = "Resource not found";
            }

            return queryResult;
        }

        public async Task<QueryResModel<ManyCliForDTO>> GetCliForsAsync(CliForsInputModel input)
        {
            QueryResModel<ManyCliForDTO> queryResult = new QueryResModel<ManyCliForDTO>();

            IQueryable<CliForModel> IQclifors = DbContext.Clifor
                .Where(cf => cf.Nome.ToLower().Contains(input.PartialName) ||
                   cf.Cognome.ToLower().Contains(input.PartialName) ||
                   cf.Denominazione.ToLower().Contains(input.PartialName) ||
                   cf.CodiceFiscale.ToLower().Contains(input.PartialName) ||
                   (cf.IdPaese + cf.IdCodice).ToLower().Contains(input.PartialName)
                )
                .Include(cf => cf.Contatti)
                .Include(cf => cf.ContiBancari)
                .Include(cf => cf.Sedi)
                .OrderBy(cf => cf.Denominazione ?? (cf.Nome + cf.Cognome));

            List<CliForModel> clifors = await IQclifors
                .Skip(input.Offset)
                .Take(input.Limit)
                .ToListAsync()
                .ConfigureAwait(false);

            ManyCliForDTO cliForsDTO = new ManyCliForDTO();

            queryResult.Data.Count = await IQclifors
                .CountAsync()
                .ConfigureAwait(false);

            foreach (var cf in clifors)
            {
                queryResult.Data.CliFors.Add(Mapper.Map<CliForModel, CliForDTO>(cf));
            }

            if (queryResult.Data.Count == 0)
            {
                queryResult.Succeeded = false;
                queryResult.Error = "No resources found.";
            }

            return queryResult;
        }

        public async Task<QueryResModel<Dictionary<string, int>>> GetCPIds(string name)
        {
            QueryResModel<Dictionary<string, int>> res = new QueryResModel<Dictionary<string, int>>();
            res.Data = new Dictionary<string, int>();

            var result = await DbContext.Clifor
                .Where(cf => cf.Denominazione.Contains(name) || cf.Nome.Contains(name) || cf.Cognome.Contains(name))
                .Select(cf =>
                   new
                   {
                       denominazione = cf.Denominazione,
                       nome = cf.Nome,
                       cognome = cf.Cognome,
                       id = cf.Id
                   })
                .ToListAsync()
                .ConfigureAwait(false);

            foreach (var r in result)
            {
                var temp = r.denominazione == null ? r.nome + " " + r.cognome : r.denominazione;
                res.Data.Add(temp, r.id);
            }

            return res;
        }

        public async Task<QueryResModel<CliForDTO>> UpdateAsync(CliForModel data)
        {
            QueryResModel<CliForDTO> queryResult = new QueryResModel<CliForDTO>();

            int changes = -1;
            if (await DbContext.Clifor.AnyAsync(cf => cf.Id == data.Id).ConfigureAwait(false))
            {
                DbContext.Clifor.Update(data);
                changes = await DbContext
                    .SaveChangesAsync()
                    .ConfigureAwait(false);
            }

            if (changes <= 0)
            {
                queryResult.Succeeded = false;
                queryResult.Error = changes == -1 ? "Error: CliFor not found" : "Error: Update failed.";
                return queryResult;
            }
            else
            {
                CliForModel cliFor = await DbContext.Clifor
                    .Where(cf => cf.Id == data.Id)
                    .AsNoTracking()
                    .FirstAsync()
                    .ConfigureAwait(false);
                queryResult.Data = Mapper.Map<CliForModel, CliForDTO>(cliFor);

                return queryResult;
            }

        }

        public async Task<bool> DeleteCliforAsync(int id)
        {
            try
            {
                var clifor = DbContext.Clifor
                    .Where(c => c.Id == id).First();
                DbContext.Clifor.Remove(clifor);
                await DbContext.SaveChangesAsync().ConfigureAwait(false);

                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        public async Task<QueryResModel<List<DatiPagamentoDTO>>> GetDatiPagamento(int invoiceId)
        {
            var resModel = new QueryResModel<List<DatiPagamentoDTO>>();

            var datiPagamento = await DbContext.DatiPagamento
                .Where(dp => dp.BodyModelId == invoiceId)
                .Include(dp => dp.DettaglioPagamento)
                .ToListAsync()
                .ConfigureAwait(false);


            resModel.Data = new List<DatiPagamentoDTO>();

            if (datiPagamento.Count > 0)
            {
                resModel.Succeeded = true;

                foreach (var dp in datiPagamento)
                {
                    resModel.Data.Add(Mapper.Map<DatiPagamentoModel, DatiPagamentoDTO>(dp));
                }

                return resModel;
            }
            else
            {
                resModel.Succeeded = false;
                resModel.Error = "Nessun pagamento trovato";

                return resModel;
            }
        }

        // @Contatti
        public async Task<QueryResModel<List<ContattiDTO>>> GetContacts(int cliforId)
        {
            var resModel = new QueryResModel<List<ContattiDTO>>();

            var contatti = await DbContext.Contatti
                .Where(c => c.CliforModelId == cliforId)
                .ToListAsync()
                .ConfigureAwait(false);


            resModel.Data = new List<ContattiDTO>();

            if (contatti.Count > 0)
            {
                resModel.Succeeded = true;

                foreach (var c in contatti)
                {
                    resModel.Data.Add(Mapper.Map<ContattiModel, ContattiDTO>(c));
                }

                return resModel;
            }
            else
            {
                resModel.Succeeded = false;
                resModel.Error = "Nessun contatto trovato";

                return resModel;
            }
        }

        public async Task<bool> AddAsync(ContattiModel contatto)
        {
            if (contatto.CliforModelId > 0 && (contatto.Telefono != null || contatto.Fax != null || contatto.Email != null))
            {
                try
                {
                    DbContext.Contatti.Add(contatto);
                    await DbContext.SaveChangesAsync().ConfigureAwait(false);

                    return true;
                }
                catch (System.Exception)
                {
                    return false;
                }
            }

            return false;
        }

        public async Task<bool> UpdateAsync(ContattiModel contatto)
        {
            try
            {
                DbContext.Contatti.Update(contatto);
                await DbContext.SaveChangesAsync().ConfigureAwait(false);

                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteContattoAsync(int id)
        {
            try
            {
                ContattiModel contatto = DbContext.Contatti
                    .Where(c => c.Id == id).First();
                DbContext.Contatti.Remove(contatto);
                await DbContext.SaveChangesAsync().ConfigureAwait(false);

                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        // @ContiBancari
        public async Task<QueryResModel<List<ContiBancariDTO>>> GetContiBancariAsync(int cliforId)
        {
            var resModel = new QueryResModel<List<ContiBancariDTO>>();

            var contiBancari = await DbContext.ContiBancari
                .Where(c => c.CliforModelId == cliforId)
                .ToListAsync()
                .ConfigureAwait(false);


            resModel.Data = new List<ContiBancariDTO>();

            if (contiBancari.Count > 0)
            {
                resModel.Succeeded = true;

                foreach (var cb in contiBancari)
                {
                    resModel.Data.Add(Mapper.Map<ContiBancariModel, ContiBancariDTO>(cb));
                }

                return resModel;
            }
            else
            {
                resModel.Succeeded = false;
                resModel.Error = "Nessun conti bancario trovato";

                return resModel;
            }
        }

        public async Task<bool> AddAsync(ContiBancariModel conto)
        {
            if (conto.CliforModelId > 0 && conto.IBAN != null)
            {
                try
                {
                    DbContext.ContiBancari.Add(conto);
                    await DbContext.SaveChangesAsync().ConfigureAwait(false);

                    return true;
                }
                catch (System.Exception)
                {
                    return false;
                }
            }

            return false;
        }

        public async Task<bool> UpdateAsync(ContiBancariModel conto)
        {
            try
            {
                DbContext.ContiBancari.Update(conto);
                await DbContext.SaveChangesAsync().ConfigureAwait(false);

                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteContoAsync(int id)
        {
            if (await DbContext.ContiBancari.AnyAsync(c => c.Id == id).ConfigureAwait(false))
            {
                ContiBancariModel conto = await DbContext.ContiBancari
                    .Where(c => c.Id == id)
                    .FirstAsync()
                    .ConfigureAwait(false);

                DbContext.ContiBancari.Remove(conto);

                await DbContext
                    .SaveChangesAsync()
                    .ConfigureAwait(false);

                return true;
            }
            return false;
        }

        // @Sedi
        public async Task<bool> AddAsync(SediModel sede)
        {
            if (sede.CliforModelId > 0 && sede.Indirizzo != null && sede.CAP != null && sede.Comune != null && sede.Nazione != null)
            {
                try
                {
                    DbContext.Sedi.Add(sede);
                    await DbContext.SaveChangesAsync().ConfigureAwait(false);

                    return true;
                }
                catch (System.Exception)
                {
                    return false;
                }
            }

            return false;
        }

        public async Task<bool> UpdateAsync(SediModel sede)
        {
            try
            {
                DbContext.Sedi.Update(sede);
                await DbContext.SaveChangesAsync().ConfigureAwait(false);

                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteSedeAsync(int id)
        {
            if (await DbContext.Sedi.AnyAsync(s => s.Id == id).ConfigureAwait(false))
            {
                SediModel sede = await DbContext.Sedi
                    .Where(s => s.Id == id)
                    .FirstAsync()
                    .ConfigureAwait(false);

                DbContext.Sedi.Remove(sede);
                await DbContext
                    .SaveChangesAsync()
                    .ConfigureAwait(false);

                return true;
            }
            return false;
        }
    }
}
