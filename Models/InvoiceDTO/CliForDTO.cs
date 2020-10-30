using AspNET.Models.InvoiceDTO.Header;
using System;
using System.Collections.Generic;

namespace AspNET.Models.InvoiceDTO
{
    public class CliForDTO
    {
        public int Id { get; set; }
        public int? RappresentanteFiscaleId { get; set; }
        public string IdPaese { get; set; }
        public string IdCodice { get; set; }
        public string CodiceFiscale { get; set; }
        public string Denominazione { get; set; }
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public string Titolo { get; set; }
        public string CodEORI { get; set; }

        public string AlboProfessionale { get; set; }
        public string ProvinciaAlbo { get; set; }
        public string NumeroIscrizioneAlbo { get; set; }
        public DateTime? DataIscrizioneAlbo { get; set; }

        public string RegimeFiscale { get; set; }
        public string RiferimentoAmministrazione { get; set; }

        public string Ufficio { get; set; }
        public string NumeroREA { get; set; }
        public decimal? CapitaleSociale { get; set; }
        public string SocioUnico { get; set; }
        public string StatoLiquidazione { get; set; }
        public string Indirizzo { get; set; }
        public string NumeroCivico { get; set; }
        public string CAP { get; set; }
        public string Comune { get; set; }
        public string Provincia { get; set; }
        public string Nazione { get; set; }
        public string NumeroLicenzaGuida { get; set; }
        public List<CliForDTO> Rappresentati { get; set; }
        public List<ContattiDTO> Contatti { get; set; }
        public List<SediDTO> Sedi { get; set; }
        public List<ContiBancariDTO> ContiBancari { get; set; }
        public List<BodyDTO> BodyModelCP { get; set; }
        public List<BodyDTO> BodyModelCC { get; set; }

    }
}
