using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AspNET.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CliFor",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RappresentanteFiscaleId = table.Column<int>(nullable: true),
                    IdPaese = table.Column<string>(maxLength: 2, nullable: true),
                    IdCodice = table.Column<string>(maxLength: 28, nullable: true),
                    CodiceFiscale = table.Column<string>(maxLength: 16, nullable: true),
                    Denominazione = table.Column<string>(maxLength: 80, nullable: true),
                    Nome = table.Column<string>(maxLength: 60, nullable: true),
                    Cognome = table.Column<string>(maxLength: 60, nullable: true),
                    Titolo = table.Column<string>(maxLength: 10, nullable: true),
                    CodEORI = table.Column<string>(maxLength: 17, nullable: true),
                    AlboProfessionale = table.Column<string>(maxLength: 60, nullable: true),
                    ProvinciaAlbo = table.Column<string>(maxLength: 2, nullable: true),
                    NumeroIscrizioneAlbo = table.Column<string>(maxLength: 60, nullable: true),
                    DataIscrizioneAlbo = table.Column<DateTime>(nullable: true),
                    RegimeFiscale = table.Column<string>(maxLength: 4, nullable: true),
                    RiferimentoAmministrazione = table.Column<string>(maxLength: 20, nullable: true),
                    Ufficio = table.Column<string>(maxLength: 2, nullable: true),
                    NumeroREA = table.Column<string>(maxLength: 20, nullable: true),
                    CapitaleSociale = table.Column<decimal>(type: "decimal(15,2)", nullable: true),
                    SocioUnico = table.Column<string>(maxLength: 2, nullable: true),
                    StatoLiquidazione = table.Column<string>(maxLength: 2, nullable: true),
                    NumeroLicenzaGuida = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CliFor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CliFor_CliFor_RappresentanteFiscaleId",
                        column: x => x.RappresentanteFiscaleId,
                        principalTable: "CliFor",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contatti",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CliforModelId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(maxLength: 100, nullable: true),
                    Telefono = table.Column<string>(maxLength: 12, nullable: true),
                    Fax = table.Column<string>(maxLength: 12, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    StillActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contatti", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contatti_CliFor_CliforModelId",
                        column: x => x.CliforModelId,
                        principalTable: "CliFor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContiBancari",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CliforModelId = table.Column<int>(nullable: false),
                    IBAN = table.Column<string>(maxLength: 34, nullable: false),
                    ABI = table.Column<string>(maxLength: 5, nullable: true),
                    CAB = table.Column<string>(maxLength: 5, nullable: true),
                    BIC = table.Column<string>(maxLength: 11, nullable: true),
                    IstitutoFinanziario = table.Column<string>(maxLength: 80, nullable: true),
                    StillActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContiBancari", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContiBancari_CliFor_CliforModelId",
                        column: x => x.CliforModelId,
                        principalTable: "CliFor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Metadata",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrasmittenteId = table.Column<int>(nullable: false),
                    IntermediarioOEmittenteId = table.Column<int>(nullable: true),
                    ProgressivoInvio = table.Column<string>(maxLength: 10, nullable: false),
                    FormatoTrasmissione = table.Column<string>(maxLength: 5, nullable: false),
                    CodiceDestinatario = table.Column<string>(maxLength: 7, nullable: false),
                    SoggettoEmittente = table.Column<string>(maxLength: 2, nullable: true),
                    PECDestinatario = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Metadata", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Metadata_CliFor_IntermediarioOEmittenteId",
                        column: x => x.IntermediarioOEmittenteId,
                        principalTable: "CliFor",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Metadata_CliFor_TrasmittenteId",
                        column: x => x.TrasmittenteId,
                        principalTable: "CliFor",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Sedi",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CliforModelId = table.Column<int>(nullable: true),
                    StabileOrganizzazioneId = table.Column<int>(nullable: true),
                    Description = table.Column<string>(maxLength: 100, nullable: true),
                    Indirizzo = table.Column<string>(maxLength: 60, nullable: false),
                    NumeroCivico = table.Column<string>(maxLength: 8, nullable: true),
                    CAP = table.Column<string>(maxLength: 5, nullable: false),
                    Comune = table.Column<string>(maxLength: 60, nullable: false),
                    Provincia = table.Column<string>(maxLength: 2, nullable: true),
                    Nazione = table.Column<string>(maxLength: 2, nullable: false),
                    StillActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sedi", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sedi_CliFor_CliforModelId",
                        column: x => x.CliforModelId,
                        principalTable: "CliFor",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Sedi_CliFor_StabileOrganizzazioneId",
                        column: x => x.StabileOrganizzazioneId,
                        principalTable: "CliFor",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Bodies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CedentePrestatoreId = table.Column<int>(nullable: false),
                    CessionarioCommittenteId = table.Column<int>(nullable: false),
                    VettoreId = table.Column<int>(nullable: true),
                    MetadataId = table.Column<int>(nullable: false),
                    LuogoResaId = table.Column<int>(nullable: true),
                    TipoDocumento = table.Column<string>(maxLength: 4, nullable: false),
                    Divisa = table.Column<string>(maxLength: 3, nullable: false),
                    Data = table.Column<DateTime>(nullable: false),
                    Numero = table.Column<string>(maxLength: 20, nullable: false),
                    BolloVirtuale = table.Column<string>(maxLength: 2, nullable: true),
                    ImportoBollo = table.Column<decimal>(type: "decimal(15,2)", nullable: true),
                    ImportoTotaleDocumento = table.Column<decimal>(type: "decimal(15,2)", nullable: true),
                    Arrotondamento = table.Column<decimal>(type: "decimal(15,2)", nullable: true),
                    Art73 = table.Column<string>(maxLength: 2, nullable: true),
                    NumeroFatturaPrincipale = table.Column<string>(maxLength: 20, nullable: true),
                    DataFatturaPrincipale = table.Column<DateTime>(nullable: true),
                    MezzoTrasporto = table.Column<string>(maxLength: 80, nullable: true),
                    CausaleTrasporto = table.Column<string>(maxLength: 100, nullable: true),
                    NumeroColli = table.Column<int>(maxLength: 4, nullable: true),
                    Descrizione = table.Column<string>(maxLength: 100, nullable: true),
                    UnitaMisuraPeso = table.Column<string>(maxLength: 10, nullable: true),
                    PesoLordo = table.Column<decimal>(type: "decimal(7,2)", nullable: true),
                    PesoNetto = table.Column<decimal>(type: "decimal(7,2)", nullable: true),
                    DataOraRitiro = table.Column<DateTime>(nullable: true),
                    DataInizioTrasporto = table.Column<DateTime>(nullable: true),
                    TipoResa = table.Column<string>(maxLength: 3, nullable: true),
                    DataOraConsegna = table.Column<DateTime>(nullable: true),
                    NumeroFR = table.Column<string>(nullable: true),
                    DataFR = table.Column<DateTime>(nullable: true),
                    ElementiRettificati = table.Column<string>(nullable: true),
                    Simplified = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bodies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bodies_CliFor_CedentePrestatoreId",
                        column: x => x.CedentePrestatoreId,
                        principalTable: "CliFor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bodies_CliFor_CessionarioCommittenteId",
                        column: x => x.CessionarioCommittenteId,
                        principalTable: "CliFor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bodies_Sedi_LuogoResaId",
                        column: x => x.LuogoResaId,
                        principalTable: "Sedi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bodies_Metadata_MetadataId",
                        column: x => x.MetadataId,
                        principalTable: "Metadata",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bodies_CliFor_VettoreId",
                        column: x => x.VettoreId,
                        principalTable: "CliFor",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Causale",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BodyModelId = table.Column<int>(nullable: false),
                    Causale = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Causale", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Causale_Bodies_BodyModelId",
                        column: x => x.BodyModelId,
                        principalTable: "Bodies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Dati",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BodyModelId = table.Column<int>(nullable: false),
                    DataType = table.Column<string>(maxLength: 20, nullable: false),
                    IdDocumento = table.Column<string>(maxLength: 20, nullable: false),
                    Data = table.Column<DateTime>(nullable: true),
                    NumItem = table.Column<string>(maxLength: 20, nullable: true),
                    CodiceCommessaConvenzione = table.Column<string>(maxLength: 100, nullable: true),
                    CodiceCUP = table.Column<string>(maxLength: 15, nullable: true),
                    CodiceCIG = table.Column<string>(maxLength: 15, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dati", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dati_Bodies_BodyModelId",
                        column: x => x.BodyModelId,
                        principalTable: "Bodies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DatiBeniServizi",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BodyModelId = table.Column<int>(nullable: false),
                    Descrizione = table.Column<string>(maxLength: 1000, nullable: false),
                    Importo = table.Column<decimal>(type: "decimal(15,2)", nullable: false),
                    Imposta = table.Column<decimal>(type: "decimal(15,2)", nullable: false),
                    Aliquota = table.Column<decimal>(type: "decimal(6,2)", nullable: true),
                    Natura = table.Column<string>(maxLength: 2, nullable: true),
                    RiferimentoNormativo = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DatiBeniServizi", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DatiBeniServizi_Bodies_BodyModelId",
                        column: x => x.BodyModelId,
                        principalTable: "Bodies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DatiCassaPrevidenziale",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BodyModelId = table.Column<int>(nullable: false),
                    TipoCassa = table.Column<string>(maxLength: 4, nullable: false),
                    AlCassa = table.Column<decimal>(type: "decimal(6,2)", nullable: false),
                    ImportoContributoCassa = table.Column<decimal>(type: "decimal(15,2)", nullable: false),
                    ImponibileCassa = table.Column<decimal>(type: "decimal(15,2)", nullable: false),
                    AliquotaIVA = table.Column<decimal>(type: "decimal(6,2)", nullable: false),
                    Ritenuta = table.Column<string>(maxLength: 2, nullable: true),
                    Natura = table.Column<string>(maxLength: 2, nullable: true),
                    RiferimentoAmministrazione = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DatiCassaPrevidenziale", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DatiCassaPrevidenziale_Bodies_BodyModelId",
                        column: x => x.BodyModelId,
                        principalTable: "Bodies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DatiDDT",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BodyModelId = table.Column<int>(nullable: false),
                    DataDDT = table.Column<DateTime>(nullable: false),
                    NumeroDDT = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DatiDDT", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DatiDDT_Bodies_BodyModelId",
                        column: x => x.BodyModelId,
                        principalTable: "Bodies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DatiPagamento",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BodyModelId = table.Column<int>(nullable: false),
                    CondizioniPagamento = table.Column<string>(maxLength: 4, nullable: false),
                    Active = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DatiPagamento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DatiPagamento_Bodies_BodyModelId",
                        column: x => x.BodyModelId,
                        principalTable: "Bodies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DatiRiepilogo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BodyModelId = table.Column<int>(nullable: false),
                    AliquotaIVA = table.Column<decimal>(type: "decimal(6,2)", nullable: false),
                    Natura = table.Column<string>(maxLength: 2, nullable: true),
                    SpeseAccessorie = table.Column<decimal>(type: "decimal(15,2)", nullable: true),
                    Arrotondamento = table.Column<decimal>(type: "decimal(21,2)", nullable: true),
                    ImponibileImporto = table.Column<decimal>(type: "decimal(15,2)", nullable: false),
                    Imposta = table.Column<decimal>(type: "decimal(15,2)", nullable: false),
                    EsigibilitaIVA = table.Column<string>(maxLength: 1, nullable: true),
                    RiferimentoNormativo = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DatiRiepilogo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DatiRiepilogo_Bodies_BodyModelId",
                        column: x => x.BodyModelId,
                        principalTable: "Bodies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DatiRitenuta",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BodyModelId = table.Column<int>(nullable: false),
                    TipoRitenuta = table.Column<string>(maxLength: 4, nullable: true),
                    ImportoRitenuta = table.Column<decimal>(type: "decimal(15,2)", nullable: true),
                    AliquotaRitenuta = table.Column<decimal>(type: "decimal(6,2)", nullable: true),
                    CausalePagamento = table.Column<string>(maxLength: 1, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DatiRitenuta", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DatiRitenuta_Bodies_BodyModelId",
                        column: x => x.BodyModelId,
                        principalTable: "Bodies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DatiSAL",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BodyModelId = table.Column<int>(nullable: false),
                    RiferimentoFase = table.Column<int>(maxLength: 3, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DatiSAL", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DatiSAL_Bodies_BodyModelId",
                        column: x => x.BodyModelId,
                        principalTable: "Bodies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DettaglioLinee",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BodyModelId = table.Column<int>(nullable: false),
                    NumeroLinea = table.Column<int>(maxLength: 4, nullable: false),
                    TipoCessionePrestazione = table.Column<string>(maxLength: 2, nullable: true),
                    Descrizione = table.Column<string>(maxLength: 1000, nullable: false),
                    Quantita = table.Column<decimal>(type: "decimal(21,8)", nullable: true),
                    UnitaMisura = table.Column<string>(maxLength: 10, nullable: true),
                    DataInizioPeriodo = table.Column<DateTime>(nullable: true),
                    DataFinePeriodo = table.Column<DateTime>(nullable: true),
                    PrezzoUnitario = table.Column<decimal>(type: "decimal(21,8)", nullable: false),
                    PrezzoTotale = table.Column<decimal>(type: "decimal(21,8)", nullable: false),
                    AliquotaIVA = table.Column<decimal>(type: "decimal(6,2)", nullable: false),
                    Ritenuta = table.Column<string>(maxLength: 2, nullable: true),
                    Natura = table.Column<string>(maxLength: 2, nullable: true),
                    RiferimentoAmministrazione = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DettaglioLinee", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DettaglioLinee_Bodies_BodyModelId",
                        column: x => x.BodyModelId,
                        principalTable: "Bodies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RiferimentoNumeroLinea",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DatiModelId = table.Column<int>(nullable: true),
                    DatiDDTModelId = table.Column<int>(nullable: true),
                    ModelReference = table.Column<string>(maxLength: 10, nullable: true),
                    RiferimentoNumeroLinea = table.Column<int>(maxLength: 4, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RiferimentoNumeroLinea", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RiferimentoNumeroLinea_DatiDDT_DatiDDTModelId",
                        column: x => x.DatiDDTModelId,
                        principalTable: "DatiDDT",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RiferimentoNumeroLinea_Dati_DatiModelId",
                        column: x => x.DatiModelId,
                        principalTable: "Dati",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DettaglioPagamento",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DatiPagamentoModelId = table.Column<int>(nullable: false),
                    PaymentDate = table.Column<DateTime>(nullable: true),
                    TRNCode = table.Column<string>(maxLength: 30, nullable: true),
                    ContoBancarioId = table.Column<int>(nullable: true),
                    ModalitaPagamento = table.Column<string>(maxLength: 4, nullable: false),
                    DataRiferimentoTerminiPagamento = table.Column<DateTime>(nullable: true),
                    GiorniTerminiPagamento = table.Column<int>(maxLength: 3, nullable: true),
                    DataScadenzaPagamento = table.Column<DateTime>(nullable: true),
                    ImportoPagamento = table.Column<decimal>(type: "decimal(15,2)", nullable: false),
                    CodUfficioPostale = table.Column<string>(maxLength: 20, nullable: true),
                    CognomeQuietanzante = table.Column<string>(maxLength: 60, nullable: true),
                    NomeQuietanzante = table.Column<string>(maxLength: 60, nullable: true),
                    CFQuietanzante = table.Column<string>(maxLength: 16, nullable: true),
                    TitoloQuietanzante = table.Column<string>(maxLength: 10, nullable: true),
                    ScontoPagamentoAnticipato = table.Column<decimal>(type: "decimal(15,2)", nullable: true),
                    DataLimitePagamentoAnticipato = table.Column<DateTime>(nullable: true),
                    PenalitaPagamentiRitardati = table.Column<decimal>(type: "decimal(15,2)", nullable: true),
                    DataDecorrenzaPenale = table.Column<DateTime>(nullable: true),
                    CodicePagamento = table.Column<string>(maxLength: 15, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DettaglioPagamento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DettaglioPagamento_DatiPagamento_DatiPagamentoModelId",
                        column: x => x.DatiPagamentoModelId,
                        principalTable: "DatiPagamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AltriDatiGestionali",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DettaglioLineeModelId = table.Column<int>(nullable: false),
                    TipoDato = table.Column<string>(maxLength: 10, nullable: false),
                    RiferimentoTesto = table.Column<string>(maxLength: 60, nullable: true),
                    RiferimentoNumero = table.Column<decimal>(type: "decimal(21,8)", nullable: true),
                    RiferimentoData = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AltriDatiGestionali", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AltriDatiGestionali_DettaglioLinee_DettaglioLineeModelId",
                        column: x => x.DettaglioLineeModelId,
                        principalTable: "DettaglioLinee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CodiceArticolo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DettaglioLineeId = table.Column<int>(nullable: false),
                    CodiceTipo = table.Column<string>(maxLength: 35, nullable: false),
                    CodiceValore = table.Column<string>(maxLength: 35, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CodiceArticolo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CodiceArticolo_DettaglioLinee_DettaglioLineeId",
                        column: x => x.DettaglioLineeId,
                        principalTable: "DettaglioLinee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScontoMaggiorazione",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DettaglioLineeId = table.Column<int>(nullable: false),
                    Tipo = table.Column<string>(maxLength: 2, nullable: false),
                    Percentuale = table.Column<decimal>(type: "decimal(6,2)", nullable: true),
                    Importo = table.Column<decimal>(type: "decimal(15,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScontoMaggiorazione", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScontoMaggiorazione_DettaglioLinee_DettaglioLineeId",
                        column: x => x.DettaglioLineeId,
                        principalTable: "DettaglioLinee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AltriDatiGestionali_DettaglioLineeModelId",
                table: "AltriDatiGestionali",
                column: "DettaglioLineeModelId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Bodies_CedentePrestatoreId",
                table: "Bodies",
                column: "CedentePrestatoreId");

            migrationBuilder.CreateIndex(
                name: "IX_Bodies_CessionarioCommittenteId",
                table: "Bodies",
                column: "CessionarioCommittenteId");

            migrationBuilder.CreateIndex(
                name: "IX_Bodies_LuogoResaId",
                table: "Bodies",
                column: "LuogoResaId",
                unique: true,
                filter: "[LuogoResaId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Bodies_MetadataId",
                table: "Bodies",
                column: "MetadataId");

            migrationBuilder.CreateIndex(
                name: "IX_Bodies_VettoreId",
                table: "Bodies",
                column: "VettoreId");

            migrationBuilder.CreateIndex(
                name: "IX_Bodies_Numero_Data_CedentePrestatoreId",
                table: "Bodies",
                columns: new[] { "Numero", "Data", "CedentePrestatoreId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Causale_BodyModelId",
                table: "Causale",
                column: "BodyModelId");

            migrationBuilder.CreateIndex(
                name: "IX_CliFor_CodiceFiscale",
                table: "CliFor",
                column: "CodiceFiscale",
                unique: true,
                filter: "[CodiceFiscale] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CliFor_RappresentanteFiscaleId",
                table: "CliFor",
                column: "RappresentanteFiscaleId");

            migrationBuilder.CreateIndex(
                name: "IX_CliFor_IdCodice_IdPaese",
                table: "CliFor",
                columns: new[] { "IdCodice", "IdPaese" },
                unique: true,
                filter: "[IdCodice] IS NOT NULL AND [IdPaese] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CodiceArticolo_DettaglioLineeId",
                table: "CodiceArticolo",
                column: "DettaglioLineeId");

            migrationBuilder.CreateIndex(
                name: "IX_CodiceArticolo_CodiceTipo_CodiceValore",
                table: "CodiceArticolo",
                columns: new[] { "CodiceTipo", "CodiceValore" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contatti_CliforModelId",
                table: "Contatti",
                column: "CliforModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Contatti_Email",
                table: "Contatti",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Contatti_Fax",
                table: "Contatti",
                column: "Fax",
                unique: true,
                filter: "[Fax] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Contatti_Telefono",
                table: "Contatti",
                column: "Telefono",
                unique: true,
                filter: "[Telefono] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ContiBancari_CliforModelId",
                table: "ContiBancari",
                column: "CliforModelId");

            migrationBuilder.CreateIndex(
                name: "IX_ContiBancari_IBAN",
                table: "ContiBancari",
                column: "IBAN",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Dati_BodyModelId",
                table: "Dati",
                column: "BodyModelId");

            migrationBuilder.CreateIndex(
                name: "IX_DatiBeniServizi_BodyModelId",
                table: "DatiBeniServizi",
                column: "BodyModelId");

            migrationBuilder.CreateIndex(
                name: "IX_DatiCassaPrevidenziale_BodyModelId",
                table: "DatiCassaPrevidenziale",
                column: "BodyModelId");

            migrationBuilder.CreateIndex(
                name: "IX_DatiDDT_BodyModelId",
                table: "DatiDDT",
                column: "BodyModelId");

            migrationBuilder.CreateIndex(
                name: "IX_DatiPagamento_BodyModelId",
                table: "DatiPagamento",
                column: "BodyModelId");

            migrationBuilder.CreateIndex(
                name: "IX_DatiRiepilogo_BodyModelId",
                table: "DatiRiepilogo",
                column: "BodyModelId");

            migrationBuilder.CreateIndex(
                name: "IX_DatiRitenuta_BodyModelId",
                table: "DatiRitenuta",
                column: "BodyModelId");

            migrationBuilder.CreateIndex(
                name: "IX_DatiSAL_BodyModelId",
                table: "DatiSAL",
                column: "BodyModelId");

            migrationBuilder.CreateIndex(
                name: "IX_DatiSAL_Id_RiferimentoFase",
                table: "DatiSAL",
                columns: new[] { "Id", "RiferimentoFase" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DettaglioLinee_BodyModelId_NumeroLinea",
                table: "DettaglioLinee",
                columns: new[] { "BodyModelId", "NumeroLinea" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DettaglioPagamento_DatiPagamentoModelId",
                table: "DettaglioPagamento",
                column: "DatiPagamentoModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Metadata_IntermediarioOEmittenteId",
                table: "Metadata",
                column: "IntermediarioOEmittenteId");

            migrationBuilder.CreateIndex(
                name: "IX_Metadata_TrasmittenteId",
                table: "Metadata",
                column: "TrasmittenteId");

            migrationBuilder.CreateIndex(
                name: "IX_RiferimentoNumeroLinea_DatiDDTModelId",
                table: "RiferimentoNumeroLinea",
                column: "DatiDDTModelId");

            migrationBuilder.CreateIndex(
                name: "IX_RiferimentoNumeroLinea_DatiModelId",
                table: "RiferimentoNumeroLinea",
                column: "DatiModelId");

            migrationBuilder.CreateIndex(
                name: "IX_ScontoMaggiorazione_DettaglioLineeId",
                table: "ScontoMaggiorazione",
                column: "DettaglioLineeId");

            migrationBuilder.CreateIndex(
                name: "IX_Sedi_CliforModelId",
                table: "Sedi",
                column: "CliforModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Sedi_StabileOrganizzazioneId",
                table: "Sedi",
                column: "StabileOrganizzazioneId");

            migrationBuilder.CreateIndex(
                name: "IX_Sedi_Indirizzo_NumeroCivico_CAP_Comune_Provincia_Nazione_StillActive",
                table: "Sedi",
                columns: new[] { "Indirizzo", "NumeroCivico", "CAP", "Comune", "Provincia", "Nazione", "StillActive" },
                unique: true,
                filter: "[Indirizzo] IS NOT NULL AND [NumeroCivico] IS NOT NULL AND [CAP] IS NOT NULL AND [Comune] IS NOT NULL AND [Provincia] IS NOT NULL AND [Nazione] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AltriDatiGestionali");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Causale");

            migrationBuilder.DropTable(
                name: "CodiceArticolo");

            migrationBuilder.DropTable(
                name: "Contatti");

            migrationBuilder.DropTable(
                name: "ContiBancari");

            migrationBuilder.DropTable(
                name: "DatiBeniServizi");

            migrationBuilder.DropTable(
                name: "DatiCassaPrevidenziale");

            migrationBuilder.DropTable(
                name: "DatiRiepilogo");

            migrationBuilder.DropTable(
                name: "DatiRitenuta");

            migrationBuilder.DropTable(
                name: "DatiSAL");

            migrationBuilder.DropTable(
                name: "DettaglioPagamento");

            migrationBuilder.DropTable(
                name: "RiferimentoNumeroLinea");

            migrationBuilder.DropTable(
                name: "ScontoMaggiorazione");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "DatiPagamento");

            migrationBuilder.DropTable(
                name: "DatiDDT");

            migrationBuilder.DropTable(
                name: "Dati");

            migrationBuilder.DropTable(
                name: "DettaglioLinee");

            migrationBuilder.DropTable(
                name: "Bodies");

            migrationBuilder.DropTable(
                name: "Sedi");

            migrationBuilder.DropTable(
                name: "Metadata");

            migrationBuilder.DropTable(
                name: "CliFor");
        }
    }
}
