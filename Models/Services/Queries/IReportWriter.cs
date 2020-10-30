using System;
using System.Collections.Generic;
using AspNET.Models.InputModels;
using AspNET.Models.ReportModel;
using ClosedXML.Excel;

namespace AspNET.Models.Services.Queries
{
    public interface IReportWriter
    {
        // Products report
        int WriteReportHeader(IXLWorksheet ws, ProductReportInputModel input);
        void WriteReportLines(IXLWorksheet ws, Tuple<int, List<ProductReportModel>> reportData);
        void WriteSummary(IXLWorksheet ws, Tuple<List<ProductReportModel>, string[]> summaryData);

        // Payments report
        int WriteReportHeader(IXLWorksheet ws, PaymentsReportInputModel input);
        void WriteReportLines(IXLWorksheet ws, Tuple<int, List<PaymentsReportModel>, PaymentsReportInputModel> reportData);
    }
}
