using AspNET.Models;
using AspNET.Models.InputModels;
using AspNET.Models.InvoiceDTO;
using AspNET.Models.InvoiceDTO.Body.Pagamenti;
using AspNET.Models.InvoiceModel;
using AspNET.Models.InvoiceModel.Body.Pagamenti;
using AspNET.Models.Options;
using AspNET.Models.ResultModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AspNET.Models.Services.Invoices;
using AutoMapper;
using AspNET.Models.InvoiceModel.Header;
using AspNET.Models.InvoiceDTO.Header;
using AspNET.Models.InvoiceDTO.Body;
using AspNET.Models.InvoiceModel.Body;

namespace AspNET.Controllers
{
    [Authorize(Roles = "admin,user")]
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly InvoiceService InvService;

        public InvoiceController(
            InvoiceDbContext invContext,
            IOptionsMonitor<InvoicesOptions> cliForOptions,
            IMapper mapper)
        {
            InvService = new InvoiceService(cliForOptions, invContext, mapper);
        }

        [HttpGet("body/{id}", Name = "GetBody")]
        public async Task<IActionResult> GetOneBodyAsync(int id)
        {
            var body = await InvService.GetOneBodyAsync(id).ConfigureAwait(false);
            if (body.Id != 0)
                return Ok(body);
            else
                return NotFound();
        }

        [HttpGet("body")]
        public Task<ManyBodyDTO> GetBodiesAsync(BodiesInputModel model)
        {
            return InvService.GetBodiesAsync(model);
        }

        // Read XML invoices in folder "./Files/Invoices"
        [HttpPost]
        public async Task<StoreInvoiceResultDTO> StoreInvoicesAsync()
        {
            return await InvService.StoreInvoiceAsync().ConfigureAwait(false);
        }

        [HttpPut("body")]
        public async Task<IActionResult> UpdateBodyAsync([FromBody] BodyModel invoice)
        {
            QueryResModel<BodyDTO> qrm = await InvService.UpdateAsync(invoice).ConfigureAwait(false);
            if (qrm.Succeeded)
            {
                return Ok(qrm.Data);
            }
            else
            {
                return BadRequest(qrm.Error);
            }
        }

        [HttpDelete("body/{id}")]
        public async Task<IActionResult> DeleteBodyAsync(int id)
        {
            if (await InvService.DeleteBodyAsync(id).ConfigureAwait(false))
                return Ok();
            else
                return BadRequest();
        }

        [HttpPut("datipagamento")]
        public async Task<IActionResult> UpdateDatiPagamento(DatiPagamentoModel dp)
        {
            if(dp.Id < 1) return BadRequest("Invalid ID property");

            var res = await InvService.UpdateDatiPagamentoAsync(dp).ConfigureAwait(false);

            if (res.Id == dp.Id)
            {
                return Ok(res);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("datipagamento")]
        public async Task<IActionResult> AddDatiPagamento(DatiPagamentoModel dp)
        {
            var res = await InvService.AddAsync(dp).ConfigureAwait(false);

            if (res.Count > 0)
            {
                return Ok(res);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("datipagamento/{id}")]
        public async Task<IActionResult> DeleteDatiPagamento(int id)
        {
            if(await InvService.DeleteDatiPagamentoAsync(id).ConfigureAwait(false))
            {
            return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut("datipagamento/{datiPagamentoModelId}/{bodyModelId}")]
        public async Task<IActionResult> DeleteDatiPagamento(int datiPagamentoModelId, int bodyModelId)
        {
            if(await InvService.ChangeActiveDatiPagamentoAsync(datiPagamentoModelId, bodyModelId).ConfigureAwait(false))
            {
            return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("dettagliopagamento")]
        public async Task<IActionResult> GetPaymentsAsync(PagamentiInputModel input)
        {
            QueryResModel<ManyDettaglioPagamentoDTO> qrm = await InvService.GetPaymentsAsync(input).ConfigureAwait(false);


            if (qrm.Succeeded)
            {
                return Ok(qrm.Data);
            }
            else
            {
                return NotFound(qrm.Error);
            }
        }

        // Switch payment status (paid/unpaid)
        [HttpPut("dettagliopagamento/{id}")]
        public async Task<IActionResult> SetAsPaid(int id)
        {
            DateTime? res = await InvService.SetPaymentStatusAsync(id).ConfigureAwait(false);

            if (res != DateTime.MinValue)
            {
                return Ok(res);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut("dettagliopagamento")]
        public async Task<IActionResult> UpdateDettaglioPagamento(DettaglioPagamentoModel dp)
        {
            if(dp.Id < 1) return BadRequest("Invalid ID property");

            DettaglioPagamentoDTO res = await InvService.UpdateDettaglioPagamentoAsync(dp).ConfigureAwait(false);

            if (res.Id == dp.Id)
            {
                return Ok(res);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("dettagliopagamento")]
        public async Task<IActionResult> AddDettaglioPagamento(DettaglioPagamentoModel dp)
        {
            var res = await InvService.AddAsync(dp).ConfigureAwait(false);

            if (res.Count > 0)
            {
                return Ok(res);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("dettagliopagamento/{id}")]
        public async Task<IActionResult> DeleteDettaglioPagamento(int id)
        {
            if(await InvService.DeleteDettaglioPagamentoAsync(id).ConfigureAwait(false))
            {
            return Ok();
            }
            else
            {
                return BadRequest();
            }
        }


        [HttpGet("clifor/{id}", Name = "GetCliFor")]
        public async Task<IActionResult> GetOneCliForAsync(int id)
        {
            QueryResModel<CliForDTO> qrm = await InvService.GetOneCliForAsync(id).ConfigureAwait(false);

            if (qrm.Succeeded)
            {
                return Ok(qrm.Data);
            }
            else
            {
                return NotFound(qrm.Error);
            }
        }

        [HttpGet("clifor")]
        public async Task<IActionResult> GetCliForsAsync(CliForsInputModel model)
        {
            QueryResModel<ManyCliForDTO> qrm = await InvService.GetCliForsAsync(model).ConfigureAwait(false);

            if (qrm.Succeeded)
            {
                return Ok(qrm.Data);
            }
            else
            {
                return NotFound(qrm.Error);
            }
        }

        [HttpPost("clifor")]
        public async Task<IActionResult> GetCPIds([FromForm] string name)
        {
            QueryResModel<Dictionary<string, int>> qrm = await InvService.GetCPIds(name).ConfigureAwait(false);

            if (qrm.Succeeded)
            {
                return Ok(qrm.Data);
            }
            else
            {
                return NotFound(qrm.Error);
            }
        }

        [HttpPut("clifor")]
        public async Task<IActionResult> UpdateCliForAsync([FromBody] CliForModel cliFor)
        {
            QueryResModel<CliForDTO> qrm = await InvService.UpdateAsync(cliFor).ConfigureAwait(false);
            if (qrm.Succeeded)
            {
                return Ok(qrm.Data);
            }
            else
            {
                return BadRequest(qrm.Error);
            }
        }

        [HttpDelete("clifor/{id}")]
        public async Task<IActionResult> DeleteCliForAsync(int id)
        {
            if (await InvService.DeleteCliforAsync(id).ConfigureAwait(false))
                return Ok();
            else
                return BadRequest();
        }

        [HttpGet("datipagamento/{id}")]
        public async Task<IActionResult> GetDatiPagamento(int id)
        {
            QueryResModel<List<DatiPagamentoDTO>> qrm = await InvService.GetDatiPagamento(id).ConfigureAwait(false);

            if (qrm.Succeeded)
            {
                return Ok(qrm.Data);
            }
            else
            {
                return BadRequest(qrm.Error);
            }
        }

        [HttpGet("contatti/{id}")]
        public async Task<IActionResult> GetContattoAsync(int id)
        {
            QueryResModel<List<ContattiDTO>> qrm = await InvService.GetContacts(id).ConfigureAwait(false);
            if (qrm.Succeeded)
            {
                return Ok(qrm.Data);
            }
            else
            {
                return BadRequest(qrm.Error);
            }
        }

        [HttpPost("contatti")]
        public async Task<IActionResult> AddContattoAsync(ContattiModel contatto)
        {
            if (await InvService.AddAsync(contatto).ConfigureAwait(false))
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut("contatti")]
        public async Task<IActionResult> UpdateAsync(ContattiModel contatto)
        {
            if (await InvService.UpdateAsync(contatto).ConfigureAwait(false))
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("contatti/{id}")]
        public async Task<IActionResult> DeleteContattoAsync(int id)
        {
            if (await InvService.DeleteContattoAsync(id).ConfigureAwait(false))
                return Ok();
            else
                return BadRequest();
        }

        [HttpGet("contibancari/{id}")]
        public async Task<IActionResult> GetContiBancariAsync(int id)
        {
            var qrm = await InvService.GetContiBancariAsync(id).ConfigureAwait(false);

            if (qrm.Succeeded)
            {
                return Ok(qrm.Data);
            }
            else
            {
                return BadRequest(qrm.Error);
            }
        }

        [HttpPost("contibancari")]
        public async Task<IActionResult> AddContoBancarioAsync(ContiBancariModel conto)
        {
            if (await InvService.AddAsync(conto).ConfigureAwait(false))
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut("contibancari")]
        public async Task<IActionResult> UpdateAsync(ContiBancariModel conto)
        {
            if (await InvService.UpdateAsync(conto).ConfigureAwait(false))
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("contibancari/{id}")]
        public async Task<IActionResult> DeleteContoAsync(int id)
        {
            if (await InvService.DeleteContoAsync(id).ConfigureAwait(false))
                return Ok();
            else
                return BadRequest();
        }

        [HttpPost("sedi")]
        public async Task<IActionResult> AddAsync(SediModel sede)
        {
            if (await InvService.AddAsync(sede).ConfigureAwait(false))
                return Ok();
            else
                return BadRequest();
        }

        [HttpPut("sedi")]
        public async Task<IActionResult> UpdateAsync(SediModel sede)
        {
            if (!ModelState.IsValid)
            {
            }

            if (await InvService.UpdateAsync(sede).ConfigureAwait(false))
                return Ok();
            else
                return BadRequest();
        }

        [HttpDelete("sedi/{id}")]
        public async Task<IActionResult> DeleteSedeAsync(int id)
        {
            if (await InvService.DeleteSedeAsync(id).ConfigureAwait(false))
                return Ok();
            else
                return BadRequest();
        }
    }
}
