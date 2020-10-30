using System;
using System.Collections.Generic;
using System.Linq;
using AspNET.Models.InputModels;
using AspNET.Models.ReportModel;
using ClosedXML.Excel;
using FatturaElettronica.Tabelle;

namespace AspNET.Models.Services.Queries
{
    public class ReportWriter : IReportWriter
    {
        public int WriteReportHeader(IXLWorksheet ws, ProductReportInputModel input)
        {
            int writingRow = 1;

            ws.Cell(writingRow, 1).RichText
                .AddText("Fornitore:")
                .SetBold();
            ws.Cell(writingRow++, 2).RichText
                .AddText(input.CedentePrestatore != null ? input.CedentePrestatore : "/");
            ws.Cell(writingRow, 1).RichText
                .AddText("Prodotti:")
                .SetBold();
            ws.Cell(writingRow++, 2).RichText
                .AddText(input.Products);
            ws.Cell(writingRow, 1).RichText
                .AddText("Dal:")
                .SetBold();
            ws.Cell(writingRow++, 2).RichText
                .AddText(input.Begin.ToString("dd/MM/yyyy"));
            ws.Cell(writingRow, 1).RichText
                .AddText("Al:")
                .SetBold();
            ws.Cell(writingRow++, 2).RichText
                .AddText(input.End.ToString("dd/MM/yyyy"));

            writingRow++;

            ws.Cell(writingRow, 1).RichText
                .AddText("Descrizione")
                .SetBold();
            ws.Cell(writingRow, 2).RichText
                .AddText("UM")
                .SetBold();
            ws.Cell(writingRow, 3).RichText
                .AddText("Q.ta")
                .SetBold();
            ws.Cell(writingRow, 4).RichText
                .AddText("P.Unit.")
                .SetBold();
            ws.Cell(writingRow, 5).RichText
                .AddText("P.Tot.")
                .SetBold();
            ws.Cell(writingRow, 6).RichText
                .AddText("Nr.DDT")
                .SetBold();
            ws.Cell(writingRow, 7).RichText
                .AddText("Data DDT")
                .SetBold();

            ws.Range("B6:G6").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            return ++writingRow;
        }

        public void WriteReportLines(IXLWorksheet ws, Tuple<int, List<ProductReportModel>> reportData)
        {
            int writingRow = reportData.Item1;
            List<ProductReportModel> productReportModel = reportData.Item2;

            DateTime currentInvoiceDate = productReportModel[0].Data;
            string currentInvoiceNumber = productReportModel[0].Numero;

            ws.Cell(writingRow++, 1).RichText
                .AddText("Fatt." + currentInvoiceNumber + " del " + currentInvoiceDate.ToString("dd/MM/yyyy"))
                    .SetItalic()
                    .SetFontSize(10);
            ws.Range("A7:G7").Style.Border.TopBorder = XLBorderStyleValues.Thick;

            var colA = ws.Column("A");
            colA.Width = 30;

            var colBD = ws.Columns("B:D");
            colBD.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            var colE = ws.Column("E");
            colE.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            var colF = ws.Column("F");
            colF.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            colF.AdjustToContents();

            var colG = ws.Column("G");
            colG.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            colG.AdjustToContents();

            var rangeValues = ws.Range("B1:B4");
            rangeValues.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;

            foreach (var p in productReportModel)
            {
                if (p.Data != currentInvoiceDate || p.Numero != currentInvoiceNumber)
                {
                    currentInvoiceDate = p.Data;
                    currentInvoiceNumber = p.Numero;

                    ws.Cell(writingRow, 1).RichText
                        .AddText("Fatt." + currentInvoiceNumber + " del " + currentInvoiceDate.ToString("dd/MM/yyyy"))
                            .SetItalic()
                            .SetFontSize(10);

                    ws.Range("A" + writingRow + ":G" + writingRow).Style.Border.TopBorder = XLBorderStyleValues.Thick;

                    writingRow++;
                }

                ws.Cell(writingRow, 1).SetValue(p.Descrizione);
                ws.Cell(writingRow, 2).SetValue(p.UnitaMisura);
                ws.Cell(writingRow, 3).SetValue(p.Quantita);
                ws.Cell(writingRow, 4).SetValue(p.PrezzoUnitario);
                ws.Cell(writingRow, 5).SetValue(p.PrezzoTotale);
                if (p.DataDDT != DateTime.MinValue)
                {
                    ws.Cell(writingRow, 6).SetValue(p.NumeroDDT);
                    ws.Cell(writingRow, 7).SetValue(p.DataDDT.ToString("dd/MM/yyyy"));
                }

                ws.Range("A" + writingRow + ":G" + writingRow).Style.Border.BottomBorder = XLBorderStyleValues.Thin;

                writingRow++;
            }
        }

        public void WriteSummary(IXLWorksheet ws, Tuple<List<ProductReportModel>, string[]> summaryData)
        {
            List<ProductReportModel> productReportModel = summaryData.Item1;
            string[] productsArray = summaryData.Item2;

            var col1 = ws.Column(1);
            col1.Width = 15;

            var col2 = ws.Column(2);
            col2.AdjustToContents();
            col2.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;

            int writingRow = 1;

            for (int i = 0; i < productsArray.Length; i++)
            {
                decimal? totQuantita = productReportModel
                   .Where(p => p.Descrizione.ToUpper().Contains(productsArray[i].ToUpper()))
                   .Select(p => p.Quantita)
                   .Sum();

                decimal? prezzoUnitario = productReportModel
                                    .Where(p => p.Descrizione.ToUpper().Contains(productsArray[i].ToUpper()))
                                    .Select(p => p.PrezzoUnitario)
                                    .FirstOrDefault();

                decimal? totPrezzoTotale = productReportModel
                                    .Where(p => p.Descrizione.ToUpper().Contains(productsArray[i].ToUpper()))
                                    .Select(p => p.PrezzoTotale)
                                    .Sum();

                ws.Cell(writingRow, 1).RichText
                        .AddText("Prodotto:")
                            .SetBold();
                ws.Cell(writingRow, 2).SetValue(productsArray[i]);
                ws.Cell(writingRow + 1, 1).RichText
                    .AddText("Totale quantita:")
                        .SetBold();
                ws.Cell(writingRow + 1, 2).SetValue(totQuantita);
                ws.Cell(writingRow + 2, 1).RichText
                    .AddText("Prezzo unitario:")
                        .SetBold();
                ws.Cell(writingRow + 2, 2).SetValue(prezzoUnitario);
                ws.Cell(writingRow + 3, 1).RichText
                    .AddText("Totale:")
                        .SetBold();
                ws.Cell(writingRow + 3, 2).SetValue(totPrezzoTotale);

                writingRow += 6;
            }
        }

        public int WriteReportHeader(IXLWorksheet ws, PaymentsReportInputModel input)
        {
            int writingRow = 1;

            ws.Cell(writingRow, 1).RichText
                .AddText("Fornitore:")
                .SetBold();
            ws.Cell(writingRow++, 2).RichText
                .AddText(input.Clifor);
            ws.Cell(writingRow, 1).RichText
                .AddText("Dal:")
                .SetBold();
            ws.Cell(writingRow++, 2).RichText
                .AddText(input.Begin.ToString("dd/MM/yyyy"));
            ws.Cell(writingRow, 1).RichText
                .AddText("Al:")
                .SetBold();
            ws.Cell(writingRow++, 2).RichText
                .AddText(input.End.ToString("dd/MM/yyyy"));

            writingRow++;

            ws.Cell(writingRow, 1).RichText
                .AddText("Numero")
                .SetBold();
            ws.Cell(writingRow, 2).RichText
                .AddText("Del")
                .SetBold();
            ws.Cell(writingRow, 3).RichText
                .AddText("Modalita")
                .SetBold();
            ws.Cell(writingRow, 4).RichText
                .AddText("Banca")
                .SetBold();
            ws.Cell(writingRow, 5).RichText
                .AddText("IBAN")
                .SetBold();
            ws.Cell(writingRow, 6).RichText
                .AddText("TRN")
                .SetBold();
            ws.Cell(writingRow, 7).RichText
                .AddText("Scadenza")
                .SetBold();
            ws.Cell(writingRow, 8).RichText
                .AddText("Pagamento")
                .SetBold();
            ws.Cell(writingRow, 9).RichText
                .AddText("Importo")
                .SetBold();

            ws.Range("B" + writingRow + ":H" + writingRow).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Cell("I" + writingRow).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
            ws.Range("A" + writingRow + ":I" + writingRow).Style.Border.BottomBorder = XLBorderStyleValues.Thick;

            return ++writingRow;
        }

        public void WriteReportLines(IXLWorksheet ws, Tuple<int, List<PaymentsReportModel>, PaymentsReportInputModel> reportData)
        {
            int writingRow = reportData.Item1;
            List<PaymentsReportModel> paymentsReportModel = reportData.Item2;
            PaymentsReportInputModel input = reportData.Item3;

            foreach (var p in paymentsReportModel)
            {
                var tabellaPagamento = new ModalitaPagamento().List;

                var paymentMethod = tabellaPagamento
                    .Where(x => x.Codice == p.ModalitaPagamento)
                    .Select(x => x.Nome)
                    .FirstOrDefault();

                ws.Cell(writingRow, 1).SetValue(p.NumeroFattura);
                ws.Cell(writingRow, 2).SetValue(p.DataFattura);
                ws.Cell(writingRow, 3).SetValue(paymentMethod);
                ws.Cell(writingRow, 4).SetValue(p.IstitutoFinanziario);
                ws.Cell(writingRow, 5).SetValue(p.IBAN);
                ws.Cell(writingRow, 6).SetValue(p.TRNCode);
                if (p.DataScadenzaPagamento != DateTime.MinValue)
                {
                    ws.Cell(writingRow, 7).SetValue(p.DataScadenzaPagamento.ToString("dd/MM/yyyy"));
                }
                if (p.PaymentDate != DateTime.MinValue)
                {
                    ws.Cell(writingRow, 8).SetValue(p.PaymentDate.ToString("dd/MM/yyyy"));
                }
                ws.Cell(writingRow, 9).SetValue(p.ImportoPagamento);

                ws.Range("A" + writingRow + ":I" + writingRow).Style.Border.BottomBorder = XLBorderStyleValues.Thin;

                writingRow++;
            }

            writingRow++;

            var due = paymentsReportModel
                .Select(x => x.ImportoPagamento)
                .Sum();
            var paid = paymentsReportModel
                .Where(x => x.PaymentDate != DateTime.MinValue)
                .Select(x => x.ImportoPagamento)
                .Sum();
            var difference = paymentsReportModel
                .Where(x => x.PaymentDate == DateTime.MinValue)
                .Select(x => x.ImportoPagamento)
                .Sum();

            ws.Cell(writingRow, 1).RichText
                .AddText("Totale fatture:")
                .SetBold();
            ws.Cell(writingRow, 2).SetValue(due);

            ws.Cell(writingRow + 1, 1).RichText
                .AddText("Totale pagato:")
                .SetBold();
            ws.Cell(writingRow + 1, 2).SetValue(paid);

            ws.Cell(writingRow + 2, 1).RichText
                .AddText("Residuo:")
                .SetBold();
            ws.Cell(writingRow + 2, 2).SetValue(difference);


            var colA = ws.Column("A");
            colA.AdjustToContents();

            var colBD = ws.Columns("B:D");
            colBD.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            colBD.AdjustToContents();

            var colE = ws.Column("E");
            colE.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            colE.AdjustToContents();

            var colF = ws.Column("F");
            colF.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            colF.AdjustToContents();

            var colG = ws.Column("G");
            colG.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            colG.AdjustToContents();

            var colH = ws.Column("H");
            colH.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            colH.AdjustToContents();

            var colI = ws.Column("I");
            colI.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
            colI.AdjustToContents();

            var rangeValues = ws.Range("B1:B4");
            rangeValues.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
        }
    }
}
