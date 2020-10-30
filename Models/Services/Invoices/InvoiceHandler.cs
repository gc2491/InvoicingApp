using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using AspNET.Models.InvoiceModel;
using AspNET.Models.Options;
using AspNET.Models.ResultModel;
using FatturaElettronica;
using FatturaElettronica.Common;
using FatturaElettronica.Extensions;
using FatturaElettronica.Ordinaria;
using FatturaElettronica.Semplificata;
using Microsoft.EntityFrameworkCore;

namespace AspNET.Models.Services.Invoices
{
    public class InvoiceHandler : IInvoiceHandler
    {
        public InvoiceHandler(InvoiceDbContext DbContext, InvoicesOptions InvoicesOptions)
        {
            this.DbContext = DbContext;
            this.InvoicesOptions = InvoicesOptions;
        }

        public InvoiceDbContext DbContext { get; }
        public InvoicesOptions InvoicesOptions { get; }
        public string InvoicePath { get; set; } = "Files/Invoices/";
        public string InvalidPath { get; set; } = "Files/Invoices/invalid/";
        public string StoredPath { get; set; } = "Files/Invoices/stored/";

        public List<ReadInvoiceResult> ReadInvoices()
        {
            List<ReadInvoiceResult> readingResult = new List<ReadInvoiceResult>();

            var readerSettings = new XmlReaderSettings
            {
                IgnoreWhitespace = true,
                IgnoreComments = true,
                IgnoreProcessingInstructions = true
            };

            List<string> files = Directory.GetFiles(InvoicePath).ToList();

            foreach (var file in files)
            {
                var readInvoiceResult = new ReadInvoiceResult();

                if (Path.GetExtension(file) != ".xml")
                {
                    readInvoiceResult.FileIsValid = false;

                    continue;
                }

                using var stream = new FileStream(file, FileMode.Open, FileAccess.Read);

                FatturaBase tempInvoice = null;

                try
                {
                    tempInvoice = FatturaBase.CreateInstanceFromXml(stream);
                } catch
                {
                    readInvoiceResult.InvoiceIsValid = false;
                    readInvoiceResult.FileIsValid = false;
                    readInvoiceResult.IsForOwner = false;
                    readInvoiceResult.ValidationResult = null;

                    readInvoiceResult.FileName = Path.GetFileName(file);

                    readingResult.Add(readInvoiceResult);
                    continue;
                }

                if (tempInvoice.GetType() == typeof(FatturaOrdinaria))
                {
                    readInvoiceResult.Ordinaria = new FatturaOrdinaria();
                    readInvoiceResult.Ordinaria = (FatturaOrdinaria)tempInvoice;
                    readInvoiceResult.ValidationResult = readInvoiceResult.Ordinaria.Validate();
                }
                else
                {
                    readInvoiceResult.Semplificata = new FatturaSemplificata();
                    readInvoiceResult.Semplificata = (FatturaSemplificata)tempInvoice;
                    readInvoiceResult.ValidationResult = readInvoiceResult.Semplificata.Validate();
                }


                readInvoiceResult.InvoiceIsValid = readInvoiceResult.ValidationResult.IsValid;

                IdFiscaleIVA IdFiscaleSupplier = new IdFiscaleIVA();
                IdFiscaleIVA IdFiscaleClient = new IdFiscaleIVA();

                if (readInvoiceResult.Ordinaria != null)
                {
                    IdFiscaleSupplier = ((FatturaOrdinaria)readInvoiceResult.Ordinaria).FatturaElettronicaHeader.CedentePrestatore.DatiAnagrafici.IdFiscaleIVA;
                    IdFiscaleClient = ((FatturaOrdinaria)readInvoiceResult.Ordinaria).FatturaElettronicaHeader.CessionarioCommittente.DatiAnagrafici.IdFiscaleIVA;
                }
                else
                {
                    IdFiscaleSupplier = ((FatturaSemplificata)readInvoiceResult.Semplificata).FatturaElettronicaHeader.CedentePrestatore.IdFiscaleIVA;
                    IdFiscaleClient = ((FatturaSemplificata)readInvoiceResult.Semplificata).FatturaElettronicaHeader.CessionarioCommittente.IdentificativiFiscali.IdFiscaleIVA;
                }

                readInvoiceResult.IsForOwner = EitherIsOwner(IdFiscaleSupplier, IdFiscaleClient);

                readInvoiceResult.FileName = Path.GetFileName(file);

                readingResult.Add(readInvoiceResult);
            }

            return readingResult;
        }

        public void MoveFiles(List<ReadInvoiceResult> invoices)
        {
            foreach (ReadInvoiceResult i in invoices)
            {
                string destination = i.FileIsValid && i.IsForOwner && i.InvoiceIsValid ?
                                     StoredPath : InvalidPath;

                string originalName = i.FileName;

                if (File.Exists(destination + i.FileName))
                {
                    i.FileName = DateTime.Now.ToString("HHmmss") + i.FileName;
                }

                File.Move(InvoicePath + originalName, destination + i.FileName);
            }
        }

        public async Task<int?> AddCliForAsync(CliForModel cliFor)
        {
            if (cliFor.CodiceFiscale == null && (cliFor.IdPaese == null || cliFor.IdCodice == null))
            {
                return null;
            }

            if (cliFor.RappresentanteFiscale != null)
            {
                cliFor.RappresentanteFiscaleId = await this
                    .AddCliForAsync(cliFor.RappresentanteFiscale)
                    .ConfigureAwait(false);

                cliFor.RappresentanteFiscale = null;
            }

            if (cliFor.IdPaese != null && cliFor.IdCodice != null)
            {
                if (await DbContext.Clifor
                        .AnyAsync(cf => cf.IdPaese == cliFor.IdPaese && cf.IdCodice == cliFor.IdCodice)
                        .ConfigureAwait(false)
                   )
                {
                    var retrievedCliFor = await DbContext.Clifor
                        .Where(cf => cf.IdPaese == cliFor.IdPaese && cf.IdCodice == cliFor.IdCodice)
                        .FirstOrDefaultAsync()
                        .ConfigureAwait(false);

                    // Add NumeroLicenzaGuida to Clifor
                    if (retrievedCliFor.NumeroLicenzaGuida == null && cliFor.NumeroLicenzaGuida != null)
                    {
                        retrievedCliFor.NumeroLicenzaGuida = cliFor.NumeroLicenzaGuida;
                        DbContext.Clifor.Update(retrievedCliFor);
                    }

                    return retrievedCliFor.Id;
                }
            }

            if (cliFor.CodiceFiscale != null)
            {
                if (await DbContext.Clifor
                        .AnyAsync(cf => cf.CodiceFiscale == cliFor.CodiceFiscale)
                        .ConfigureAwait(false)
                   )
                {
                    var retrievedCliFor = await DbContext.Clifor
                        .Where(cf => cf.CodiceFiscale == cliFor.CodiceFiscale)
                        .FirstOrDefaultAsync()
                        .ConfigureAwait(false);

                    // Add NumeroLicenzaGuida to Clifor
                    if (retrievedCliFor.NumeroLicenzaGuida == null && cliFor.NumeroLicenzaGuida != null)
                    {
                        retrievedCliFor.NumeroLicenzaGuida = cliFor.NumeroLicenzaGuida;
                        DbContext.Clifor.Update(retrievedCliFor);
                    }

                    return retrievedCliFor.Id;
                }
            }

            DbContext.Clifor.Add(cliFor);
            await DbContext
                .SaveChangesAsync()
                .ConfigureAwait(false);

            int id = cliFor.IdPaese != null & cliFor.IdCodice != null ?
                await DbContext.Clifor
                    .Where(cf => cf.IdPaese == cliFor.IdPaese && cf.IdCodice == cliFor.IdCodice)
                    .Select(cf => cf.Id)
                    .FirstOrDefaultAsync()
                    .ConfigureAwait(false)
                    :
                await DbContext.Clifor
                    .Where(cf => cf.CodiceFiscale == cliFor.CodiceFiscale)
                    .Select(cf => cf.Id)
                    .FirstOrDefaultAsync()
                    .ConfigureAwait(false);

            return id;
        }

        public bool EitherIsOwner(IdFiscaleIVA cliFor1, IdFiscaleIVA cliFor2 = null)
        {
            if (InvoicesOptions.Owner.IdPaese == cliFor1.IdPaese &&
                InvoicesOptions.Owner.IdCodice == cliFor1.IdCodice)
            {
                return true;
            }
            else if (cliFor2 != null &&
                     InvoicesOptions.Owner.IdPaese == cliFor2.IdPaese &&
                     InvoicesOptions.Owner.IdCodice == cliFor2.IdCodice)
            {
                return true;
            }

            return false;
        }

        public bool EitherIsOwner(CliForModel cliFor1, CliForModel cliFor2 = null)
        {
            if (InvoicesOptions.Owner.IdPaese == cliFor1.IdPaese &&
                InvoicesOptions.Owner.IdCodice == cliFor1.IdCodice)
            {
                return true;
            }
            else if (cliFor2 != null &&
                     InvoicesOptions.Owner.IdPaese == cliFor2.IdPaese &&
                     InvoicesOptions.Owner.IdCodice == cliFor2.IdCodice)
            {
                return true;
            }

            return false;
        }

        public bool SameIdIvaOrFiscale(CliForModel clifor1, CliForModel clifor2)
        {
            if (((clifor1.IdPaese != null && clifor2.IdPaese != null)
                && (clifor1.IdCodice != null & clifor2.IdCodice != null)
                && clifor1.IdPaese == clifor2.IdPaese
                && clifor1.IdCodice == clifor2.IdCodice)
                || (clifor1.CodiceFiscale != null && clifor2.CodiceFiscale != null
                && clifor1.CodiceFiscale == clifor2.CodiceFiscale))
            {
                return true;
            }
            else return false;
        }
    }
}
