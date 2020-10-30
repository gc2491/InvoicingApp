using AspNET.Models.InvoiceModel;
using AspNET.Models.InvoiceModel.Body;
using AspNET.Models.InvoiceModel.Body.Linee;
using AspNET.Models.InvoiceModel.Body.Pagamenti;
using AspNET.Models.InvoiceModel.Header;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AspNET.Models.InvoiceModel.Body.RiferimentoNumeroLinea;

namespace AspNET.Models
{
    public class InvoiceDbContext : IdentityDbContext
    {
        public InvoiceDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<BodyModel> Bodies { get; set; }

        public DbSet<DatiCassaPrevidenzialeModel> DatiCassaPrevidenziale { get; set; }
        public DbSet<DatiDDTModel> DatiDDT { get; set; }
        public DbSet<DatiModel> DatiModel { get; set; }
        public DbSet<RiferimentoNumeroLineaModel> RiferimentoNumeroLineaModels { get; set; }

        public DbSet<DatiPagamentoModel> DatiPagamento { get; set; }
        public DbSet<DettaglioPagamentoModel> DettaglioPagamento { get; set; }

        public DbSet<DatiRiepilogoModel> Riepiloghi { get; set; }

        public DbSet<DettaglioLineeModel> Linee { get; set; }
        public DbSet<AltriDatiGestionaliModel> AltriDatiGestionali { get; set; }
        public DbSet<CodiceArticoloModel> CodiceArticolo { get; set; }
        public DbSet<ScontoMaggiorazioneModel> ScontoMaggiorazione { get; set; }

        public DbSet<CliForModel> Clifor { get; set; }

        public DbSet<ContattiModel> Contatti { get; set; }
        public DbSet<SediModel> Sedi { get; set; }
        public DbSet<ContiBancariModel> ContiBancari { get; set; }

        public DbSet<MetadataModel> Metadata { get; set; }

        public DbSet<DatiBeniServiziModel> BeniServizi { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BodyModel>(entity =>
            {
                entity.ToTable("Bodies");

                entity.HasIndex(body => new { body.Numero, body.Data, body.CedentePrestatoreId }).IsUnique();

                entity.HasMany(body => body.Causale).WithOne(c => c.BodyModel).HasForeignKey(c => c.BodyModelId);
                entity.HasMany(body => body.DatiCassaPrevidenziale).WithOne(dcp => dcp.BodyModel).HasForeignKey(dcp => dcp.BodyModelId);
                entity.HasMany(body => body.DatiDDT).WithOne(ddt => ddt.BodyModel).HasForeignKey(ddt => ddt.BodyModelId);
                entity.HasMany(body => body.Dati).WithOne(dati => dati.BodyModel).HasForeignKey(dati => dati.BodyModelId);
                entity.HasMany(body => body.DatiPagamento).WithOne(dp => dp.BodyModel).HasForeignKey(dp => dp.BodyModelId);
                entity.HasMany(body => body.DatiRiepilogo).WithOne(dr => dr.BodyModel).HasForeignKey(dr => dr.BodyModelId);
                entity.HasMany(body => body.DatiRitenuta).WithOne(dr => dr.BodyModel).HasForeignKey(dr => dr.BodyModelId);
                entity.HasMany(body => body.DatiSAL).WithOne(sal => sal.BodyModel).HasForeignKey(sal => sal.BodyModelId);
                entity.HasMany(body => body.DettaglioLinee).WithOne(dl => dl.BodyModel).HasForeignKey(dl => dl.BodyModelId);
                entity.HasMany(body => body.DatiBeniServizi).WithOne(dbs => dbs.BodyModel).HasForeignKey(dbs => dbs.BodyModelId);
                entity.HasOne(body => body.LuogoResa).WithOne(sede => sede.Body).HasForeignKey<BodyModel>(body => body.LuogoResaId);

                entity.Property(e => e.TipoDocumento).HasMaxLength(4).IsRequired();
                entity.Property(e => e.Divisa).HasMaxLength(3).IsRequired();
                entity.Property(e => e.Data).IsRequired();
                entity.Property(e => e.Numero).HasMaxLength(20).IsRequired();

                entity.Property(e => e.BolloVirtuale).HasMaxLength(2);
                entity.Property(e => e.ImportoBollo).HasColumnType("decimal(15,2)");

                entity.Property(e => e.ImportoTotaleDocumento).HasColumnType("decimal(15,2)");
                entity.Property(e => e.Arrotondamento).HasColumnType("decimal(15,2)");
                entity.Property(e => e.Art73).HasMaxLength(2);

                entity.Property(e => e.NumeroFatturaPrincipale).HasMaxLength(20);

                entity.Property(e => e.MezzoTrasporto).HasMaxLength(80);
                entity.Property(e => e.CausaleTrasporto).HasMaxLength(100);
                entity.Property(e => e.NumeroColli).HasMaxLength(4);
                entity.Property(e => e.Descrizione).HasMaxLength(100);
                entity.Property(e => e.UnitaMisuraPeso).HasMaxLength(10);
                entity.Property(e => e.PesoLordo).HasColumnType("decimal(7,2)");
                entity.Property(e => e.PesoNetto).HasColumnType("decimal(7,2)");
                entity.Property(e => e.TipoResa).HasMaxLength(3);
            });

            modelBuilder.Entity<CausaleModel>(entity =>
            {
                entity.ToTable("Causale");

                entity.Property(e => e.Causale).HasMaxLength(200);
            });

            modelBuilder.Entity<DatiCassaPrevidenzialeModel>(entity =>
            {
                entity.ToTable("DatiCassaPrevidenziale");

                entity.Property(e => e.TipoCassa).HasMaxLength(4).IsRequired();
                entity.Property(e => e.AlCassa).HasColumnType("decimal(6,2)").IsRequired();
                entity.Property(e => e.ImportoContributoCassa).HasColumnType("decimal(15,2)").IsRequired();
                entity.Property(e => e.ImponibileCassa).HasColumnType("decimal(15,2)");
                entity.Property(e => e.AliquotaIVA).HasColumnType("decimal(6,2)").IsRequired();
                entity.Property(e => e.Ritenuta).HasMaxLength(2);
                entity.Property(e => e.Natura).HasMaxLength(2);
                entity.Property(e => e.RiferimentoAmministrazione).HasMaxLength(20);
            });


            modelBuilder.Entity<DatiDDTModel>(entity =>
            {
                entity.ToTable("DatiDDT");

                entity
                    .HasMany(ddt => ddt.RiferimentoNumeroLinea)
                    .WithOne(rnl => rnl.DatiDDTModel)
                    .HasForeignKey(rnl => rnl.DatiDDTModelId);

                entity.Property(e => e.DataDDT).IsRequired();
                entity.Property(e => e.NumeroDDT).HasMaxLength(20).IsRequired();
            });


            modelBuilder.Entity<DatiModel>(entity =>
            {
                entity.ToTable("Dati");

                entity
                    .HasMany(dati => dati.RiferimentoNumeroLinea)
                    .WithOne(rnl => rnl.DatiModel)
                    .HasForeignKey(rnl => rnl.DatiModelId);

                entity.Property(e => e.DataType).HasMaxLength(20).IsRequired();
                entity.Property(e => e.IdDocumento).HasMaxLength(20).IsRequired();
                entity.Property(e => e.NumItem).HasMaxLength(20);
                entity.Property(e => e.CodiceCommessaConvenzione).HasMaxLength(100);
                entity.Property(e => e.CodiceCUP).HasMaxLength(15);
                entity.Property(e => e.CodiceCIG).HasMaxLength(15);
            });

            modelBuilder.Entity<DatiRitenutaModel>(entity =>
            {
                entity.ToTable("DatiRitenuta");

                entity.Property(e => e.TipoRitenuta).HasMaxLength(4);
                entity.Property(e => e.ImportoRitenuta).HasColumnType("decimal(15,2)");
                entity.Property(e => e.AliquotaRitenuta).HasColumnType("decimal(6,2)");
                entity.Property(e => e.CausalePagamento).HasMaxLength(1);

            });

            modelBuilder.Entity<RiferimentoNumeroLineaModel>(entity =>
            {
                entity.ToTable("RiferimentoNumeroLinea");

                entity.Property(e => e.RiferimentoNumeroLinea).HasMaxLength(4);
                entity.Property(e => e.ModelReference).HasMaxLength(10);
            });

            modelBuilder.Entity<DatiPagamentoModel>(entity =>
            {
                entity.ToTable("DatiPagamento");

                entity.HasMany(dp => dp.DettaglioPagamento).WithOne(dettaglio => dettaglio.DatiPagamento).HasForeignKey(dettaglio => dettaglio.DatiPagamentoModelId);

                entity.Property(e => e.CondizioniPagamento).HasMaxLength(4).IsRequired();
                entity.Property(e => e.Active).HasDefaultValue(false).IsRequired();
            });

            modelBuilder.Entity<DettaglioPagamentoModel>(entity =>
            {
                entity.ToTable("DettaglioPagamento");

                entity.Property(e => e.TRNCode).HasMaxLength(30);
                entity.Property(e => e.ModalitaPagamento).HasMaxLength(4).IsRequired();
                entity.Property(e => e.GiorniTerminiPagamento).HasMaxLength(3);
                entity.Property(e => e.ImportoPagamento).HasColumnType("decimal(15,2)").IsRequired();
                entity.Property(e => e.CodUfficioPostale).HasMaxLength(20);
                entity.Property(e => e.CognomeQuietanzante).HasMaxLength(60);
                entity.Property(e => e.NomeQuietanzante).HasMaxLength(60);
                entity.Property(e => e.CFQuietanzante).HasMaxLength(16);
                entity.Property(e => e.TitoloQuietanzante).HasMaxLength(10);
                entity.Property(e => e.ScontoPagamentoAnticipato).HasColumnType("decimal(15,2)");
                entity.Property(e => e.PenalitaPagamentiRitardati).HasColumnType("decimal(15,2)");
                entity.Property(e => e.CodicePagamento).HasMaxLength(15);
            });

            modelBuilder.Entity<DatiRiepilogoModel>(entity =>
            {
                entity.ToTable("DatiRiepilogo");

                entity.Property(e => e.AliquotaIVA).HasColumnType("decimal(6,2)").IsRequired();
                entity.Property(e => e.Natura).HasMaxLength(2);
                entity.Property(e => e.SpeseAccessorie).HasColumnType("decimal(15,2)");
                entity.Property(e => e.Arrotondamento).HasColumnType("decimal(21,2)");
                entity.Property(e => e.ImponibileImporto).HasColumnType("decimal(15,2)").IsRequired();
                entity.Property(e => e.Imposta).HasColumnType("decimal(15,2)").IsRequired();
                entity.Property(e => e.EsigibilitaIVA).HasMaxLength(1);
                entity.Property(e => e.RiferimentoNormativo).HasMaxLength(100);
            });


            modelBuilder.Entity<DatiSALModel>(entity =>
            {
                entity.ToTable("DatiSAL");

                entity.HasIndex(sal => new { sal.Id, sal.RiferimentoFase }).IsUnique();

                entity.Property(e => e.RiferimentoFase).HasMaxLength(3).IsRequired();
            });


            modelBuilder.Entity<DettaglioLineeModel>(entity =>
            {
                entity.ToTable("DettaglioLinee");

                entity.HasIndex(linee => new { linee.BodyModelId, linee.NumeroLinea }).IsUnique();

                entity.HasMany(dl => dl.AltriDatiGestionali).WithOne(adg => adg.DettaglioLinee).HasForeignKey(adg => adg.DettaglioLineeModelId);
                entity.HasMany(dl => dl.CodiceArticolo).WithOne(ca => ca.DettaglioLinee).HasForeignKey(ca => ca.DettaglioLineeId);
                entity.HasMany(dl => dl.ScontoMaggiorazione).WithOne(sm => sm.DettaglioLinee).HasForeignKey(sm => sm.DettaglioLineeId);

                entity.Property(e => e.NumeroLinea).HasMaxLength(4).IsRequired();
                entity.Property(e => e.TipoCessionePrestazione).HasMaxLength(2);
                entity.Property(e => e.Descrizione).HasMaxLength(1000).IsRequired();
                entity.Property(e => e.Quantita).HasColumnType("decimal(21,8)");
                entity.Property(e => e.UnitaMisura).HasMaxLength(10);
                entity.Property(e => e.PrezzoUnitario).HasColumnType("decimal(21,8)").IsRequired();
                entity.Property(e => e.PrezzoTotale).HasColumnType("decimal(21,8)").IsRequired();
                entity.Property(e => e.AliquotaIVA).HasColumnType("decimal(6,2)").IsRequired();
                entity.Property(e => e.Ritenuta).HasMaxLength(2);
                entity.Property(e => e.Natura).HasMaxLength(2);
                entity.Property(e => e.RiferimentoAmministrazione).HasMaxLength(20);
            });

            modelBuilder.Entity<AltriDatiGestionaliModel>(entity =>
            {
                entity.ToTable("AltriDatiGestionali");

                entity.Property(e => e.TipoDato).HasMaxLength(10).IsRequired();
                entity.Property(e => e.RiferimentoTesto).HasMaxLength(60);
                entity.Property(e => e.RiferimentoNumero).HasColumnType("decimal(21,8)");
            });

            modelBuilder.Entity<CodiceArticoloModel>(entity =>
            {
                entity.ToTable("CodiceArticolo");

                entity.HasIndex(codArt => new { codArt.CodiceTipo, codArt.CodiceValore }).IsUnique();

                entity.Property(e => e.CodiceTipo).HasMaxLength(35).IsRequired();
                entity.Property(e => e.CodiceValore).HasMaxLength(35).IsRequired();
            });

            modelBuilder.Entity<ScontoMaggiorazioneModel>(entity =>
            {
                entity.ToTable("ScontoMaggiorazione");

                entity.Property(e => e.Tipo).HasMaxLength(2).IsRequired();
                entity.Property(e => e.Percentuale).HasColumnType("decimal(6,2)");
                entity.Property(e => e.Importo).HasColumnType("decimal(15,2)");
            });

            modelBuilder.Entity<CliForModel>().HasIndex(cf => cf.CodiceFiscale).IsUnique();
            modelBuilder.Entity<CliForModel>().HasIndex(iva => new { iva.IdCodice, iva.IdPaese }).IsUnique();
            modelBuilder.Entity<CliForModel>(entity =>
            {
                entity.ToTable("CliFor");

                entity
                    .HasMany(cf => cf.BodyModelCP)
                    .WithOne(b => b.CedentePrestatore)
                    .HasForeignKey(b => b.CedentePrestatoreId)
                    .OnDelete(DeleteBehavior.Restrict);
                entity
                    .HasMany(cf => cf.BodyModelCC)
                    .WithOne(b => b.CessionarioCommittente)
                    .HasForeignKey(b => b.CessionarioCommittenteId)
                    .OnDelete(DeleteBehavior.Restrict);
                entity
                    .HasMany(cf => cf.BodyModelVettore)
                    .WithOne(b => b.Vettore)
                    .HasForeignKey(b => b.VettoreId)
                    .OnDelete(DeleteBehavior.NoAction);
                entity
                    .HasMany(cf => cf.Contatti)
                    .WithOne(c => c.Clifor)
                    .HasForeignKey(c => c.CliforModelId);
                entity
                    .HasMany(cf => cf.ContiBancari)
                    .WithOne(c => c.Clifor)
                    .HasForeignKey(c => c.CliforModelId);
                entity
                    .HasMany(cf => cf.Sedi)
                    .WithOne(s => s.Clifor)
                    .HasForeignKey(s => s.CliforModelId)
                    .OnDelete(DeleteBehavior.NoAction);
                entity
                    .HasMany(cf => cf.Rappresentati)
                    .WithOne(cf => cf.RappresentanteFiscale)
                    .HasForeignKey(cf => cf.RappresentanteFiscaleId)
                    .OnDelete(DeleteBehavior.NoAction);
                entity
                    .HasMany(cf => cf.IntermediarioOEmittente)
                    .WithOne(md => md.IntermediarioOEmittente)
                    .HasForeignKey(md => md.IntermediarioOEmittenteId)
                    .OnDelete(DeleteBehavior.NoAction);
                entity
                    .HasMany(cf => cf.Trasmittente)
                    .WithOne(md => md.Trasmittente)
                    .HasForeignKey(md => md.TrasmittenteId)
                    .OnDelete(DeleteBehavior.NoAction);
                entity
                    .HasMany(cf => cf.StabileOrganizzazione)
                    .WithOne(sedi => sedi.StabileOrganizzazione)
                    .HasForeignKey(sedi => sedi.StabileOrganizzazioneId)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.Property(e => e.IdPaese).HasMaxLength(2);
                entity.Property(e => e.IdCodice).HasMaxLength(28);
                entity.Property(e => e.CodiceFiscale).HasMaxLength(16);
                entity.Property(e => e.Denominazione).HasMaxLength(80);
                entity.Property(e => e.Nome).HasMaxLength(60);
                entity.Property(e => e.Cognome).HasMaxLength(60);
                entity.Property(e => e.Titolo).HasMaxLength(10);
                entity.Property(e => e.CodEORI).HasMaxLength(17);

                entity.Property(e => e.AlboProfessionale).HasMaxLength(60);
                entity.Property(e => e.ProvinciaAlbo).HasMaxLength(2);
                entity.Property(e => e.NumeroIscrizioneAlbo).HasMaxLength(60);

                entity.Property(e => e.RegimeFiscale).HasMaxLength(4);
                entity.Property(e => e.RiferimentoAmministrazione).HasMaxLength(20);

                entity.Property(e => e.Ufficio).HasMaxLength(2);
                entity.Property(e => e.NumeroREA).HasMaxLength(20);
                entity.Property(e => e.CapitaleSociale).HasColumnType("decimal(15,2)");
                entity.Property(e => e.SocioUnico).HasMaxLength(2);
                entity.Property(e => e.StatoLiquidazione).HasMaxLength(2);

                entity.Property(e => e.NumeroLicenzaGuida).HasMaxLength(20);
            });

            modelBuilder.Entity<ContattiModel>(entity =>
            {
                entity.ToTable("Contatti");

                entity.HasIndex(contatto => contatto.Telefono).IsUnique();
                entity.HasIndex(contatto => contatto.Fax).IsUnique();
                entity.HasIndex(contatto => contatto.Email).IsUnique();


                entity.Property(e => e.Description).HasMaxLength(100);
                entity.Property(e => e.Telefono).HasMaxLength(12);
                entity.Property(e => e.Fax).HasMaxLength(12);
                entity.Property(e => e.Email).HasMaxLength(256);
                entity.Property(e => e.StillActive).IsRequired();
            });


            modelBuilder.Entity<ContiBancariModel>(entity =>
            {
                entity.ToTable("ContiBancari");

                entity.HasIndex(conti => conti.IBAN).IsUnique();

                entity.Property(e => e.IBAN).HasMaxLength(34).IsRequired();
                entity.Property(e => e.ABI).HasMaxLength(5);
                entity.Property(e => e.CAB).HasMaxLength(5);
                entity.Property(e => e.BIC).HasMaxLength(11);
                entity.Property(e => e.IstitutoFinanziario).HasMaxLength(80);
                entity.Property(e => e.StillActive).IsRequired();
            });


            modelBuilder.Entity<SediModel>(entity =>
            {
                entity.ToTable("Sedi");

                entity.HasIndex(sedi => new
                {
                    sedi.Indirizzo,
                    sedi.NumeroCivico,
                    sedi.CAP,
                    sedi.Comune,
                    sedi.Provincia,
                    sedi.Nazione,
                    sedi.StillActive
                }).IsUnique();

                entity.Property(e => e.Description).HasMaxLength(100);
                entity.Property(e => e.Indirizzo).HasMaxLength(60).IsRequired();
                entity.Property(e => e.NumeroCivico).HasMaxLength(8);
                entity.Property(e => e.CAP).HasMaxLength(5).IsRequired();
                entity.Property(e => e.Comune).HasMaxLength(60).IsRequired();
                entity.Property(e => e.Provincia).HasMaxLength(2);
                entity.Property(e => e.Nazione).HasMaxLength(2).IsRequired();
                entity.Property(e => e.StillActive).IsRequired();
            });

            modelBuilder.Entity<DatiBeniServiziModel>(entity =>
            {
                entity.ToTable("DatiBeniServizi");

                entity.Property(e => e.Descrizione).HasMaxLength(1000).IsRequired();
                entity.Property(e => e.Importo).HasColumnType("decimal(15,2)");
                entity.Property(e => e.Imposta).HasColumnType("decimal(15,2)").IsRequired();
                entity.Property(e => e.Aliquota).HasColumnType("decimal(6,2)");
                entity.Property(e => e.Natura).HasMaxLength(2);
                entity.Property(e => e.RiferimentoNormativo).HasMaxLength(100);
            });

            modelBuilder.Entity<MetadataModel>(entity =>
            {
                entity.ToTable("Metadata");

                entity.HasMany(md => md.Bodies)
                .WithOne(body => body.Metadata)
                .HasForeignKey(body => body.MetadataId);

                entity.Property(e => e.ProgressivoInvio).HasMaxLength(10).IsRequired();
                entity.Property(e => e.FormatoTrasmissione).HasMaxLength(5).IsRequired();
                entity.Property(e => e.CodiceDestinatario).HasMaxLength(7).IsRequired();
                entity.Property(e => e.SoggettoEmittente).HasMaxLength(2);
                entity.Property(e => e.PECDestinatario).HasMaxLength(256);
            });
        }
    }
}
