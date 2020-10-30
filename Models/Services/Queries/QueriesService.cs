using AspNET.Models.InputModels;
using AspNET.Models.InvoiceModel.Body;
using AspNET.Models.InvoiceModel.Body.Pagamenti;
using AspNET.Models.Options;
using AspNET.Models.ReportModel;
using AutoMapper;
using ClosedXML.Excel;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NinjaNye.SearchExtensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AspNET.Models.Services.Queries
{
    public class QueriesService : IQueriesService
    {
        public QueriesService(InvoiceDbContext dbContext, IOptionsMonitor<InvoicesOptions> options, IMapper Mapper)
        {
            this.DbContext = dbContext;
            this.Mapper = Mapper;
            this.ReportWriter = new ReportWriter();
            this.OwnerId = DbContext.Clifor
                .Where(cf => cf.IdPaese == options.CurrentValue.Owner.IdPaese &&
                            cf.IdCodice == options.CurrentValue.Owner.IdCodice)
                .Select(cf => cf.Id)
                .FirstOrDefault();
        }

        public InvoiceDbContext DbContext { get; }
        public int OwnerId { get; }
        public IMapper Mapper { get; }
        public ReportWriter ReportWriter { get; }
        public string ReportsPath { get; } = "Files/Reports/";

        public async Task<bool> BackupAsync()
        {
            string currentDir = Path.GetFullPath("./");
            int result = await DbContext.Database
            .ExecuteSqlRawAsync("BACKUP DATABASE InvoiceApp TO DISK =@path WITH CHECKSUM", new SqlParameter("@path", DateTime.Now.ToString("yyyymmddHHmmss") + ".bak"));

            return result >= 0 ? true : false;
        }

        public async Task<string> ProductReport(ProductReportInputModel input)
        {
            string[] productsArray = input.Products.Split(",");

            int productsArrayLength = productsArray.Length;
            for (int i = 0; i < productsArrayLength; i++)
            {
                productsArray[i] = productsArray[i].Trim();
            }

            List<DettaglioLineeModel> lineeReport = new List<DettaglioLineeModel>();

            lineeReport = await DbContext.Linee
                .Search(l => l.Descrizione)
                .Containing(productsArray)
                .Where(l => input.CedentePrestatoreId != 0 ?
                            l.BodyModel.CedentePrestatoreId == input.CedentePrestatoreId : true &&
                            l.BodyModel.CedentePrestatoreId != OwnerId)
                .Include(l => l.BodyModel)
                    .ThenInclude(b => b.DatiDDT)
                        .ThenInclude(ddt => ddt.RiferimentoNumeroLinea)
                .ToListAsync()
                .ConfigureAwait(false);

            if (lineeReport.Count == 0) return null;

            lineeReport = lineeReport
                .OrderBy(lr => lr.BodyModel.Data)
                .ThenBy(lr => lr.BodyModel.Id)
                .ThenBy(lr => lr.NumeroLinea)
                .ToList();

            List<ProductReportModel> productReportModel = new List<ProductReportModel>();

            foreach (var linea in lineeReport)
            {
                productReportModel.Add(Mapper
                        .Map<DettaglioLineeModel, ProductReportModel>(linea));
            }

            XLWorkbook wb = new XLWorkbook();

            IXLWorksheet ws = wb.AddWorksheet("Report");

            int writingRow = ReportWriter.WriteReportHeader(ws, input);

            var reportData = new Tuple<int, List<ProductReportModel>>
            (
                writingRow,
                productReportModel
            );

            ReportWriter.WriteReportLines(ws, reportData);

            IXLWorksheet riepilogoWs = wb.AddWorksheet("Riepilogo");

            var summaryData = new Tuple<List<ProductReportModel>, string[]>
            (
                productReportModel, productsArray
            );

            ReportWriter.WriteSummary(riepilogoWs, summaryData);

            string filePath = ReportsPath + "prodotti_" +
                              DateTime.Now.ToString("yy-MM-dd_hhmmss");

            if(!string.IsNullOrEmpty(input.CedentePrestatore))
            {
                filePath += "_" + input.CedentePrestatore.Replace(' ', '_');
            }

            filePath +=  ".xlsx";

            wb.SaveAs(filePath);

            return filePath;
        }

        public async Task<string> PaymentsReport(PaymentsReportInputModel input)
        {
            var lineeReport = new List<DettaglioPagamentoModel>();

            lineeReport = await DbContext.DettaglioPagamento
                .Where(dp => (input.Emitted ?
                             dp.DatiPagamento.BodyModel.CessionarioCommittenteId == input.CliforId :
                             dp.DatiPagamento.BodyModel.CedentePrestatoreId == input.CliforId) &&
                             dp.DatiPagamento.BodyModel.Data >= input.Begin &&
                             dp.DatiPagamento.BodyModel.Data <= input.End)
                .Include(l => l.DatiPagamento)
                    .ThenInclude(dp => dp.BodyModel)
                        .ThenInclude(b => b.CedentePrestatore)
                            .ThenInclude(cf => cf.ContiBancari)
                .ToListAsync()
                .ConfigureAwait(false);

            if (lineeReport.Count == 0) return null;

            lineeReport = lineeReport
                .OrderBy(lr => lr.DataScadenzaPagamento)
                .ThenBy(lr => lr.Id)
                .ToList();

            var paymentsReportModel = new List<PaymentsReportModel>();

            foreach (var linea in lineeReport)
            {
                paymentsReportModel.Add(Mapper
                        .Map<DettaglioPagamentoModel, PaymentsReportModel>(linea));
            }

            XLWorkbook wb = new XLWorkbook();

            IXLWorksheet ws = wb.AddWorksheet("Report");

            int writingRow = ReportWriter.WriteReportHeader(ws, input);

            var reportData = new Tuple<int, List<PaymentsReportModel>, PaymentsReportInputModel>
            (
                writingRow,
                paymentsReportModel,
                input
            );

            ReportWriter.WriteReportLines(ws, reportData);

            string filePath = ReportsPath + "pagamenti_" +
                              DateTime.Now.ToString("yy-MM-dd_hhmmss") + "_" +
                              input.Clifor.Replace(' ', '_') + "_" + ".xlsx";
            wb.SaveAs(filePath);

            return filePath;
        }
    }
}
