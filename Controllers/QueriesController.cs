using System;
using System.IO;
using System.Threading.Tasks;
using AspNET.Models;
using AspNET.Models.InputModels;
using AspNET.Models.Options;
using AspNET.Models.Services.Queries;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AspNET.Controllers
{
    [Authorize(Roles = "admin,user")]
    [Route("api/[controller]")]
    [ApiController]
    public class QueriesController : ControllerBase
    {
        private readonly QueriesService QueriesService;

        public QueriesController(InvoiceDbContext dbContext, IOptionsMonitor<InvoicesOptions> options, IMapper mapper)
        {
            QueriesService = new QueriesService(dbContext, options, mapper);
        }

        // GET: api/queries/backup
        [HttpGet("backup")]
        public async Task<IActionResult> Backup()
        {
            bool success = await QueriesService.BackupAsync().ConfigureAwait(false);

            return Ok(success);
        }

        [HttpGet("productreport")]
        public async Task<IActionResult> GetProductReportAsync(ProductReportInputModel input)
        {
            var file = await QueriesService
                .ProductReport(input)
                .ConfigureAwait(false);

            if (String.IsNullOrEmpty(file)) return NotFound("No data found for the query");

            var memory = new MemoryStream();

            using (var stream1 = new FileStream(file, FileMode.Open))
            {
                await stream1
                 .CopyToAsync(memory)
                 .ConfigureAwait(false);
            }

            memory.Position = 0;

            var headertype = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";

            return File(memory, headertype, Path.GetFileName(file));
        }


        [HttpGet("paymentsreport")]
        public async Task<IActionResult> GetPaymentsReportAsync(PaymentsReportInputModel input)
        {
            var file = await QueriesService
                .PaymentsReport(input)
                .ConfigureAwait(false);

            if (String.IsNullOrEmpty(file)) return NotFound("No data found for the query");

            var memory = new MemoryStream();

            using (var stream1 = new FileStream(file, FileMode.Open))
            {
                await stream1
                 .CopyToAsync(memory)
                 .ConfigureAwait(false);
            }

            memory.Position = 0;

            var headertype = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";

            return File(memory, headertype, Path.GetFileName(file));
        }
    }
}
