using AspNET.Models.InputModels;
using System.Threading.Tasks;

namespace AspNET.Models.Services.Queries
{
    interface IQueriesService
    {
        Task<bool> BackupAsync();
        Task<string> ProductReport(ProductReportInputModel input);
    }
}
