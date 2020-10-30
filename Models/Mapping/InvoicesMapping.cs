using System;
using System.Linq;
using AspNET.Models.InvoiceDTO;
using AspNET.Models.InvoiceDTO.Body;
using AspNET.Models.InvoiceDTO.Body.Linee;
using AspNET.Models.InvoiceDTO.Body.Pagamenti;
using AspNET.Models.InvoiceDTO.Body.RiferimentoNumeroLinea;
using AspNET.Models.InvoiceDTO.Header;
using AspNET.Models.InvoiceModel;
using AspNET.Models.InvoiceModel.Body;
using AspNET.Models.InvoiceModel.Body.Linee;
using AspNET.Models.InvoiceModel.Body.Pagamenti;
using AspNET.Models.InvoiceModel.Body.RiferimentoNumeroLinea;
using AspNET.Models.InvoiceModel.Header;
using AspNET.Models.ReportModel;
using AutoMapper;
using System.Data;

namespace AspNET.Models.Mapping
{
    public class InvoicesMapping : Profile
    {
        public InvoicesMapping()
        {
            CreateMap<BodyModel, BodyDTO>();
            CreateMap<CliForModel, CliForDTO>();
            CreateMap<MetadataModel, MetadataDTO>();

            CreateMap<ContattiModel, ContattiDTO>();
            CreateMap<ContiBancariModel, ContiBancariDTO>();
            CreateMap<SediModel, SediDTO>();

            CreateMap<CausaleModel, CausaleDTO>();
            CreateMap<DatiCassaPrevidenzialeModel, DatiCassaPrevidenzialeDTO>();
            CreateMap<DatiDDTModel, DatiDDTDTO>();
            CreateMap<DatiModel, DatiDTO>();
            CreateMap<DatiPagamentoModel, DatiPagamentoDTO>();
            CreateMap<DatiRiepilogoModel, DatiRiepilogoDTO>();
            CreateMap<DatiSALModel, DatiSALDTO>();
            CreateMap<DettaglioLineeModel, DettaglioLineeDTO>();
            CreateMap<RiferimentoNumeroLineaModel, RiferimentoNumeroLineaDTO>();
            CreateMap<AltriDatiGestionaliModel, AltriDatiGestionaliDTO>();
            CreateMap<CodiceArticoloModel, CodiceArticoloDTO>();
            CreateMap<ScontoMaggiorazioneModel, ScontoMaggiorazioneDTO>();
            CreateMap<DettaglioPagamentoModel, DettaglioPagamentoDTO>();
            CreateMap<DatiBeniServiziModel, DatiBeniServiziDTO>();
            CreateMap<DettaglioLineeModel, ProductReportModel>()
                .ForMember(prm => prm.Data, config => config.MapFrom(dlm => dlm.BodyModel.Data))
                .ForMember(prm => prm.Numero, config => config.MapFrom(dlm => dlm.BodyModel.Numero))
                .ForMember(prm => prm.NumeroDDT, config => config.MapFrom(dlm => GetDDTNumber(dlm)))
                .ForMember(prm => prm.DataDDT, config => config.MapFrom(dlm => GetDDTDate(dlm)));
            CreateMap<DettaglioPagamentoModel, PaymentsReportModel>()
                .ForMember(prm => prm.NumeroFattura, config => config.MapFrom(dpm => dpm.DatiPagamento.BodyModel.Numero))
                .ForMember(prm => prm.DataFattura, config => config.MapFrom(dpm => dpm.DatiPagamento.BodyModel.Data))
                .ForMember(prm => prm.TRNCode, config => config.MapFrom(dpm => dpm.TRNCode))
                .ForMember(prm => prm.IstitutoFinanziario, config => config.MapFrom(dpm => GetIstitutoFinanziario(dpm)))
                .ForMember(prm => prm.IBAN, config => config.MapFrom(dlm => GetIBAN(dlm)));
        }

        private string GetDDTNumber(DettaglioLineeModel dlm)
        {
            return dlm.BodyModel.DatiDDT
                .Where(ddt => ddt.RiferimentoNumeroLinea == null ? true :
                              ddt.RiferimentoNumeroLinea
                                  .Any(rnl => rnl.RiferimentoNumeroLinea == dlm.NumeroLinea))
                .Select(ddt => ddt.NumeroDDT)
                .FirstOrDefault();
        }

        private DateTime GetDDTDate(DettaglioLineeModel dlm)
        {
            return dlm.BodyModel.DatiDDT
                .Where(ddt => ddt.RiferimentoNumeroLinea == null ? true :
                              ddt.RiferimentoNumeroLinea
                                  .Any(rnl => rnl.RiferimentoNumeroLinea == dlm.NumeroLinea))
                .Select(ddt => ddt.DataDDT)
                .FirstOrDefault();
        }

        private string GetIstitutoFinanziario(DettaglioPagamentoModel dpm)
        {
            if(dpm.ContoBancarioId == null) return null;

            return dpm.DatiPagamento.BodyModel.CedentePrestatore.ContiBancari
                .Where(cb => cb.Id == dpm.ContoBancarioId)
                .Select(cb => cb.IstitutoFinanziario)
                .FirstOrDefault();
        }

        private string GetIBAN(DettaglioPagamentoModel dpm)
        {
            if(dpm.ContoBancarioId == null) return null;

            return dpm.DatiPagamento.BodyModel.CedentePrestatore.ContiBancari
                .Where(cb => cb.Id == dpm.ContoBancarioId)
                .Select(cb => cb.IBAN)
                .FirstOrDefault();
        }
    }
}
