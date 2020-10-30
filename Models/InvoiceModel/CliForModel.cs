using FatturaElettronica.Ordinaria.FatturaElettronicaBody.DatiGenerali;
using FatturaElettronica.Ordinaria.FatturaElettronicaBody.DatiPagamento;
using FatturaElettronica.Ordinaria.FatturaElettronicaHeader.CedentePrestatore;
using FatturaElettronica.Ordinaria.FatturaElettronicaHeader.CessionarioCommittente;
using FatturaElettronica.Ordinaria.FatturaElettronicaHeader.DatiTrasmissione;
using FatturaElettronica.Ordinaria.FatturaElettronicaHeader.RappresentanteFiscale;
using FatturaElettronica.Ordinaria.FatturaElettronicaHeader.TerzoIntermediarioOSoggettoEmittente;
using DatiTrasmissioneSemplificata = FatturaElettronica.Semplificata.FatturaElettronicaHeader.DatiTrasmissione;
using RappresentanteFiscaleCPSemplificata = FatturaElettronica.Semplificata.FatturaElettronicaHeader;
using RappresentanteFiscaleCCSemplificata = FatturaElettronica.Semplificata.FatturaElettronicaHeader.CessionarioCommittente;
using CedentePrestatoreSimplified = FatturaElettronica.Semplificata.FatturaElettronicaHeader.CedentePrestatore;
using CessionarioCommittenteSimplified = FatturaElettronica.Semplificata.FatturaElettronicaHeader.CessionarioCommittente;
using AspNET.Models.InvoiceModel.Header;
using System;
using System.Collections.Generic;

namespace AspNET.Models.InvoiceModel
{
    public class CliForModel
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

        public string NumeroLicenzaGuida { get; set; }
        public CliForModel RappresentanteFiscale { get; set; }
        public List<CliForModel> Rappresentati { get; set; }
        public List<MetadataModel> Trasmittente { get; set; }
        public List<MetadataModel> IntermediarioOEmittente { get; set; }
        public List<BodyModel> BodyModelCP { get; set; }
        public List<BodyModel> BodyModelCC { get; set; }
        public List<BodyModel> BodyModelVettore { get; set; }
        public List<ContattiModel> Contatti { get; set; }
        public List<SediModel> StabileOrganizzazione { get; set; }
        public List<SediModel> Sedi { get; set; }
        public List<ContiBancariModel> ContiBancari { get; set; }

        public CliForModel() { }

        public CliForModel(CedentePrestatore cedentePrestatore, RappresentanteFiscale rappresentante, DettaglioPagamento dettaglioPagamento)
        {
            this.IdCodice = cedentePrestatore.DatiAnagrafici.IdFiscaleIVA.IdCodice;
            this.IdPaese = cedentePrestatore.DatiAnagrafici.IdFiscaleIVA.IdPaese;
            this.CodiceFiscale = cedentePrestatore.DatiAnagrafici.CodiceFiscale;
            this.Denominazione = cedentePrestatore.DatiAnagrafici.Anagrafica.Denominazione;
            this.Nome = cedentePrestatore.DatiAnagrafici.Anagrafica.Nome;
            this.Cognome = cedentePrestatore.DatiAnagrafici.Anagrafica.Cognome;
            this.Titolo = cedentePrestatore.DatiAnagrafici.Anagrafica.Titolo;
            this.CodEORI = cedentePrestatore.DatiAnagrafici.Anagrafica.CodEORI;

            this.AlboProfessionale = cedentePrestatore.DatiAnagrafici.AlboProfessionale;
            this.ProvinciaAlbo = cedentePrestatore.DatiAnagrafici.ProvinciaAlbo;
            this.DataIscrizioneAlbo = cedentePrestatore.DatiAnagrafici.DataIscrizioneAlbo;

            this.RegimeFiscale = cedentePrestatore.DatiAnagrafici.RegimeFiscale;
            this.RiferimentoAmministrazione = cedentePrestatore.RiferimentoAmministrazione;

            this.Ufficio = cedentePrestatore.IscrizioneREA.Ufficio;
            this.NumeroREA = cedentePrestatore.IscrizioneREA.NumeroREA;
            this.CapitaleSociale = cedentePrestatore.IscrizioneREA.CapitaleSociale;
            this.SocioUnico = cedentePrestatore.IscrizioneREA.SocioUnico;
            this.StatoLiquidazione = cedentePrestatore.IscrizioneREA.StatoLiquidazione;

            if (rappresentante != null)
            {
                this.RappresentanteFiscale = new CliForModel(rappresentante);
            }

            if (cedentePrestatore.Contatti != null &&
                    (cedentePrestatore.Contatti.Telefono != null ||
                     cedentePrestatore.Contatti.Fax != null ||
                     cedentePrestatore.Contatti.Email != null))
            {
                this.Contatti = new List<ContattiModel>();
                this.Contatti.Add(new ContattiModel(cedentePrestatore.Contatti));
            }

            if (cedentePrestatore.Sede != null && cedentePrestatore.Sede.CAP != null)
            {
                this.Sedi = new List<SediModel>();
                this.Sedi.Add(new SediModel(cedentePrestatore.Sede));
            }

            if (cedentePrestatore.StabileOrganizzazione != null && cedentePrestatore.StabileOrganizzazione.CAP != null)
            {
                if (this.Sedi == null) this.Sedi = new List<SediModel>();
                this.Sedi.Add(new SediModel(cedentePrestatore.StabileOrganizzazione));
            }

            if (dettaglioPagamento != null && dettaglioPagamento.IBAN != null)
            {
                this.ContiBancari = new List<ContiBancariModel>();
                this.ContiBancari.Add(new ContiBancariModel(dettaglioPagamento));
            }
        }

        public CliForModel(CessionarioCommittente cessionarioCommittente)
        {
            this.IdPaese = cessionarioCommittente.DatiAnagrafici.IdFiscaleIVA.IdPaese;
            this.IdCodice = cessionarioCommittente.DatiAnagrafici.IdFiscaleIVA.IdCodice;
            this.CodiceFiscale = cessionarioCommittente.DatiAnagrafici.CodiceFiscale;
            this.Denominazione = cessionarioCommittente.DatiAnagrafici.Anagrafica.Denominazione;
            this.Nome = cessionarioCommittente.DatiAnagrafici.Anagrafica.Nome;
            this.Cognome = cessionarioCommittente.DatiAnagrafici.Anagrafica.Cognome;
            this.Titolo = cessionarioCommittente.DatiAnagrafici.Anagrafica.Titolo;
            this.CodEORI = cessionarioCommittente.DatiAnagrafici.Anagrafica.CodEORI;

            if (cessionarioCommittente.RappresentanteFiscale != null)
            {
                this.RappresentanteFiscale = new CliForModel(cessionarioCommittente.RappresentanteFiscale);
            }

            if (cessionarioCommittente.Sede != null && cessionarioCommittente.Sede.CAP != null)
            {
                this.Sedi = new List<SediModel>();
                this.Sedi.Add(new SediModel(cessionarioCommittente.Sede));
            }


            if (cessionarioCommittente.StabileOrganizzazione != null && cessionarioCommittente.StabileOrganizzazione.CAP != null)
            {
                if (this.Sedi == null) this.Sedi = new List<SediModel>();
                this.Sedi.Add(new SediModel(cessionarioCommittente.StabileOrganizzazione));
            }
        }

        public CliForModel(DatiAnagraficiVettore vettore)
        {
            if (vettore.IdFiscaleIVA.IdPaese != null &&
               vettore.IdFiscaleIVA.IdCodice != null)
            {
                this.IdPaese = vettore.IdFiscaleIVA.IdPaese;
                this.IdCodice = vettore.IdFiscaleIVA.IdCodice;
                this.CodiceFiscale = vettore.CodiceFiscale;
                this.Denominazione = vettore.Anagrafica.Denominazione;
                this.Nome = vettore.Anagrafica.Nome;
                this.Cognome = vettore.Anagrafica.Cognome;
                this.Titolo = vettore.Anagrafica.Titolo;
                this.CodEORI = vettore.Anagrafica.CodEORI;
                this.NumeroLicenzaGuida = vettore.NumeroLicenzaGuida;
            }
        }

        public CliForModel(DatiTrasmissione trasmittente)
        {
            if (trasmittente.IdTrasmittente.IdCodice != null &&
               trasmittente.IdTrasmittente.IdPaese != null)
            {
                this.IdPaese = trasmittente.IdTrasmittente.IdPaese;
                this.IdCodice = trasmittente.IdTrasmittente.IdCodice;
                if (trasmittente.ContattiTrasmittente != null &&
                        (trasmittente.ContattiTrasmittente.Telefono != null ||
                         trasmittente.ContattiTrasmittente.Email != null)
                   )
                {
                    var contatto = new ContattiModel(trasmittente.ContattiTrasmittente);
                    if (contatto != null && (contatto.Telefono != null || contatto.Fax != null || contatto.Email != null))
                    {
                        this.Contatti = new List<ContattiModel>();
                        this.Contatti.Add(contatto);
                    }
                }
            }
        }

        public CliForModel(TerzoIntermediarioOSoggettoEmittente terzo)
        {
            if (terzo.DatiAnagrafici.IdFiscaleIVA.IdPaese != null &&
               terzo.DatiAnagrafici.IdFiscaleIVA.IdCodice != null)
            {
                this.IdPaese = terzo.DatiAnagrafici.IdFiscaleIVA.IdPaese;
                this.IdCodice = terzo.DatiAnagrafici.IdFiscaleIVA.IdCodice;
                this.CodiceFiscale = terzo.DatiAnagrafici.CodiceFiscale;
                this.Denominazione = terzo.DatiAnagrafici.Anagrafica.Denominazione;
                this.Nome = terzo.DatiAnagrafici.Anagrafica.Nome;
                this.Cognome = terzo.DatiAnagrafici.Anagrafica.Cognome;
                this.Titolo = terzo.DatiAnagrafici.Anagrafica.Titolo;
                this.CodEORI = terzo.DatiAnagrafici.Anagrafica.CodEORI;
            }
        }

        public CliForModel(RappresentanteFiscale rappresentante)
        {
            if (rappresentante.DatiAnagrafici.IdFiscaleIVA.IdPaese != null &&
               rappresentante.DatiAnagrafici.IdFiscaleIVA.IdCodice != null)
            {
                this.IdPaese = rappresentante.DatiAnagrafici.IdFiscaleIVA.IdPaese;
                this.IdCodice = rappresentante.DatiAnagrafici.IdFiscaleIVA.IdCodice;
                this.CodiceFiscale = rappresentante.DatiAnagrafici.CodiceFiscale;
                this.Denominazione = rappresentante.DatiAnagrafici.Anagrafica.Denominazione;
                this.Nome = rappresentante.DatiAnagrafici.Anagrafica.Nome;
                this.Cognome = rappresentante.DatiAnagrafici.Anagrafica.Cognome;
                this.Titolo = rappresentante.DatiAnagrafici.Anagrafica.Titolo;
                this.CodEORI = rappresentante.DatiAnagrafici.Anagrafica.CodEORI;
            }
        }

        public CliForModel(RappresentanteFiscaleCessionarioCommittente rappresentante)
        {
            if (rappresentante.IdFiscaleIVA.IdPaese != null &&
               rappresentante.IdFiscaleIVA.IdCodice != null)
            {
                this.IdPaese = rappresentante.IdFiscaleIVA.IdPaese;
                this.IdCodice = rappresentante.IdFiscaleIVA.IdCodice;
                this.Denominazione = rappresentante.Denominazione;
                this.Nome = rappresentante.Nome;
                this.Cognome = rappresentante.Cognome;
            }
        }

        public CliForModel(CedentePrestatoreSimplified.CedentePrestatore cedentePrestatore)
        {
            if (cedentePrestatore != null)
            {
                this.IdPaese = cedentePrestatore.IdFiscaleIVA.IdPaese;
                this.IdCodice = cedentePrestatore.IdFiscaleIVA.IdCodice;
                this.CodiceFiscale = cedentePrestatore.CodiceFiscale;
                this.Denominazione = cedentePrestatore.Denominazione;
                this.Nome = cedentePrestatore.Nome;
                this.Cognome = cedentePrestatore.Cognome;
                this.Ufficio = cedentePrestatore.IscrizioneREA.Ufficio;
                this.NumeroREA = cedentePrestatore.IscrizioneREA.NumeroREA;
                this.CapitaleSociale = cedentePrestatore.IscrizioneREA.CapitaleSociale;
                this.SocioUnico = cedentePrestatore.IscrizioneREA.SocioUnico;
                this.StatoLiquidazione = cedentePrestatore.IscrizioneREA.StatoLiquidazione;
                this.RegimeFiscale = cedentePrestatore.RegimeFiscale;

                if (cedentePrestatore.RappresentanteFiscale != null)
                {
                    this.RappresentanteFiscale = new CliForModel(cedentePrestatore.RappresentanteFiscale);
                }

                if (cedentePrestatore.Sede != null && cedentePrestatore.Sede.CAP != null)
                {
                    this.Sedi = new List<SediModel>();
                    this.Sedi.Add(new SediModel(cedentePrestatore.Sede));
                }

                if (cedentePrestatore.StabileOrganizzazione != null &&
                    cedentePrestatore.StabileOrganizzazione.CAP != null)
                {
                    if (this.Sedi == null) this.Sedi = new List<SediModel>();
                    this.Sedi.Add(new SediModel(cedentePrestatore.StabileOrganizzazione));
                }
            }
        }

        public CliForModel(CessionarioCommittenteSimplified.CessionarioCommittente cessionarioCommittente)
        {
            if (cessionarioCommittente != null)
            {
                this.IdPaese = cessionarioCommittente.IdentificativiFiscali.IdFiscaleIVA.IdPaese;
                this.IdCodice = cessionarioCommittente.IdentificativiFiscali.IdFiscaleIVA.IdCodice;
                this.CodiceFiscale = cessionarioCommittente.IdentificativiFiscali.CodiceFiscale;
                this.Denominazione = cessionarioCommittente.AltriDatiIdentificativi.Denominazione;
                this.Nome = cessionarioCommittente.AltriDatiIdentificativi.Nome;
                this.Cognome = cessionarioCommittente.AltriDatiIdentificativi.Cognome;

                if (cessionarioCommittente.AltriDatiIdentificativi.RappresentanteFiscale != null)
                {
                    this.RappresentanteFiscale = new CliForModel(cessionarioCommittente.AltriDatiIdentificativi.RappresentanteFiscale);
                }

                if (cessionarioCommittente.AltriDatiIdentificativi.Sede != null &&
                    cessionarioCommittente.AltriDatiIdentificativi.Sede.CAP != null)
                {
                    this.Sedi = new List<SediModel>();
                    this.Sedi.Add(new SediModel(cessionarioCommittente.AltriDatiIdentificativi.Sede));
                }

                if (cessionarioCommittente.AltriDatiIdentificativi.StabileOrganizzazione != null &&
                    cessionarioCommittente.AltriDatiIdentificativi.StabileOrganizzazione.CAP != null)
                {
                    if (this.Sedi == null) this.Sedi = new List<SediModel>();
                    this.Sedi.Add(new SediModel(cessionarioCommittente.AltriDatiIdentificativi.StabileOrganizzazione));
                }
            }
        }

        public CliForModel(DatiTrasmissioneSemplificata.DatiTrasmissione trasmittenteSemplificata)
        {
            if (trasmittenteSemplificata.IdTrasmittente.IdCodice != null &&
               trasmittenteSemplificata.IdTrasmittente.IdPaese != null)
            {
                this.IdPaese = trasmittenteSemplificata.IdTrasmittente.IdPaese;
                this.IdCodice = trasmittenteSemplificata.IdTrasmittente.IdCodice;
            }
        }

        public CliForModel(RappresentanteFiscaleCPSemplificata.RappresentanteFiscale rappresentante)
        {
            if (rappresentante.IdFiscaleIVA.IdPaese != null &&
               rappresentante.IdFiscaleIVA.IdCodice != null)
            {
                this.IdPaese = rappresentante.IdFiscaleIVA.IdPaese;
                this.IdCodice = rappresentante.IdFiscaleIVA.IdCodice;
                this.Denominazione = rappresentante.Denominazione;
                this.Nome = rappresentante.Nome;
                this.Cognome = rappresentante.Cognome;
            }
        }

        private CliForModel(RappresentanteFiscaleCCSemplificata.RappresentanteFiscaleCessionarioCommittente rappresentante)
        {
            if (rappresentante.IdFiscaleIVA.IdPaese != null &&
               rappresentante.IdFiscaleIVA.IdCodice != null)
            {
                this.IdPaese = rappresentante.IdFiscaleIVA.IdPaese;
                this.IdCodice = rappresentante.IdFiscaleIVA.IdCodice;
                this.Denominazione = rappresentante.Denominazione;
                this.Nome = rappresentante.Nome;
                this.Cognome = rappresentante.Cognome;
            }
        }
    }
}
