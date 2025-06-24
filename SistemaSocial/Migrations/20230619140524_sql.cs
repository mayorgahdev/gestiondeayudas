using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SistemaSocial.Migrations
{
    public partial class sql : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence<int>(
                name: "GrupoFamiliar");

            migrationBuilder.CreateSequence<int>(
                name: "GrupoFamiliarAsignacion");

            migrationBuilder.CreateSequence<int>(
                name: "GrupoFamiliarAyuda");

            migrationBuilder.CreateSequence<int>(
                name: "GrupoFamiliarCronicos");

            migrationBuilder.CreateSequence<int>(
                name: "GrupoFamiliarGastos");

            migrationBuilder.CreateSequence<int>(
                name: "GrupoFamiliarHabitacionales");

            migrationBuilder.CreateSequence<int>(
                name: "GrupoFamiliarInforme");

            migrationBuilder.CreateSequence<int>(
                name: "GrupoFamiliarIngresos");

            migrationBuilder.CreateSequence<int>(
                name: "GrupoFamiliarMedioAyuda");

            migrationBuilder.CreateSequence<int>(
                name: "GrupoFamiliarPeticionAyuda");

            migrationBuilder.CreateSequence<int>(
                name: "GrupoFamiliarSalud");

            migrationBuilder.CreateSequence<int>(
                name: "GrupoFamiliarSituacion");

            migrationBuilder.CreateSequence<int>(
                name: "GrupoFamiliarSocioeconomico");

            migrationBuilder.CreateSequence<int>(
                name: "NumAyuda");

            migrationBuilder.CreateSequence<int>(
                name: "NumCuenta");

            migrationBuilder.CreateSequence<int>(
                name: "NumMedio");

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblAreaGestion",
                columns: table => new
                {
                    AreaGestionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblAreaGestion", x => x.AreaGestionID);
                });

            migrationBuilder.CreateTable(
                name: "tblDiscapacidad",
                columns: table => new
                {
                    DiscapacidadID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblDiscapacidad", x => x.DiscapacidadID);
                });

            migrationBuilder.CreateTable(
                name: "tblEstadoCivil",
                columns: table => new
                {
                    EstadoCivilID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblEstadoCivil", x => x.EstadoCivilID);
                });

            migrationBuilder.CreateTable(
                name: "tblIndiceEscolaridad",
                columns: table => new
                {
                    IndiceEscolaridadID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblIndiceEscolaridad", x => x.IndiceEscolaridadID);
                });

            migrationBuilder.CreateTable(
                name: "tblMaterialVivienda",
                columns: table => new
                {
                    MaterialViviendaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblMaterialVivienda", x => x.MaterialViviendaID);
                });

            migrationBuilder.CreateTable(
                name: "tblNacionalidad",
                columns: table => new
                {
                    NacionalidadID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Abreviacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblNacionalidad", x => x.NacionalidadID);
                });

            migrationBuilder.CreateTable(
                name: "tblParentesco",
                columns: table => new
                {
                    ParentescoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblParentesco", x => x.ParentescoID);
                });

            migrationBuilder.CreateTable(
                name: "tblPeticion",
                columns: table => new
                {
                    PeticionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblPeticion", x => x.PeticionID);
                });

            migrationBuilder.CreateTable(
                name: "tblPrestaciones",
                columns: table => new
                {
                    PrestacionesID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cantidad = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblPrestaciones", x => x.PrestacionesID);
                });

            migrationBuilder.CreateTable(
                name: "tblPrevisionSalud",
                columns: table => new
                {
                    PrevisionSaludID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblPrevisionSalud", x => x.PrevisionSaludID);
                });

            migrationBuilder.CreateTable(
                name: "tblPrevisionSocial",
                columns: table => new
                {
                    PrevisionSocialID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblPrevisionSocial", x => x.PrevisionSocialID);
                });

            migrationBuilder.CreateTable(
                name: "tblProcedencia",
                columns: table => new
                {
                    ProcedenciaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblProcedencia", x => x.ProcedenciaID);
                });

            migrationBuilder.CreateTable(
                name: "tblProfesion",
                columns: table => new
                {
                    ProfesionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblProfesion", x => x.ProfesionID);
                });

            migrationBuilder.CreateTable(
                name: "tblRSH",
                columns: table => new
                {
                    RSHID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblRSH", x => x.RSHID);
                });

            migrationBuilder.CreateTable(
                name: "tblSituacionCasa",
                columns: table => new
                {
                    SituacionCasaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSituacionCasa", x => x.SituacionCasaID);
                });

            migrationBuilder.CreateTable(
                name: "tblTenenciaTerreno",
                columns: table => new
                {
                    TenenciaTerrenoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblTenenciaTerreno", x => x.TenenciaTerrenoID);
                });

            migrationBuilder.CreateTable(
                name: "tblTipoPension",
                columns: table => new
                {
                    TipoPensionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblTipoPension", x => x.TipoPensionID);
                });

            migrationBuilder.CreateTable(
                name: "tblTipoPeticion",
                columns: table => new
                {
                    TipoPeticionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblTipoPeticion", x => x.TipoPeticionID);
                });

            migrationBuilder.CreateTable(
                name: "tblTipoSubsidio",
                columns: table => new
                {
                    TipoSubsidioID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblTipoSubsidio", x => x.TipoSubsidioID);
                });

            migrationBuilder.CreateTable(
                name: "tblTipoUsuario",
                columns: table => new
                {
                    TipoUsuarioID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblTipoUsuario", x => x.TipoUsuarioID);
                });

            migrationBuilder.CreateTable(
                name: "tblTipoVivienda",
                columns: table => new
                {
                    TipoViviendaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblTipoVivienda", x => x.TipoViviendaID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                name: "tblPrograma",
                columns: table => new
                {
                    ProgramaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AreaGestionID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblPrograma", x => x.ProgramaID);
                    table.ForeignKey(
                        name: "FK_tblPrograma_tblAreaGestion_AreaGestionID",
                        column: x => x.AreaGestionID,
                        principalTable: "tblAreaGestion",
                        principalColumn: "AreaGestionID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblAcreditacion",
                columns: table => new
                {
                    AcreditacionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiscapacidadID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblAcreditacion", x => x.AcreditacionID);
                    table.ForeignKey(
                        name: "FK_tblAcreditacion_tblDiscapacidad_DiscapacidadID",
                        column: x => x.DiscapacidadID,
                        principalTable: "tblDiscapacidad",
                        principalColumn: "DiscapacidadID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblClientes",
                columns: table => new
                {
                    ClientesID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoDeCliente = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rut = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nombres = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApellidoPaterno = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApellidoMaterno = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaNacimiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Edad = table.Column<int>(type: "int", nullable: false),
                    NacionalidadID = table.Column<int>(type: "int", nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Correo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstadoCivilID = table.Column<int>(type: "int", nullable: false),
                    PrevisionSocialID = table.Column<int>(type: "int", nullable: false),
                    PrevisionSaludID = table.Column<int>(type: "int", nullable: false),
                    IndiceEscolaridadID = table.Column<int>(type: "int", nullable: false),
                    ParentescoID = table.Column<int>(type: "int", nullable: false),
                    Ocupacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GrupoFamiliar = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR GrupoFamiliar")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblClientes", x => x.ClientesID);
                    table.ForeignKey(
                        name: "FK_tblClientes_tblEstadoCivil_EstadoCivilID",
                        column: x => x.EstadoCivilID,
                        principalTable: "tblEstadoCivil",
                        principalColumn: "EstadoCivilID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblClientes_tblIndiceEscolaridad_IndiceEscolaridadID",
                        column: x => x.IndiceEscolaridadID,
                        principalTable: "tblIndiceEscolaridad",
                        principalColumn: "IndiceEscolaridadID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblClientes_tblNacionalidad_NacionalidadID",
                        column: x => x.NacionalidadID,
                        principalTable: "tblNacionalidad",
                        principalColumn: "NacionalidadID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblClientes_tblParentesco_ParentescoID",
                        column: x => x.ParentescoID,
                        principalTable: "tblParentesco",
                        principalColumn: "ParentescoID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblClientes_tblPrevisionSalud_PrevisionSaludID",
                        column: x => x.PrevisionSaludID,
                        principalTable: "tblPrevisionSalud",
                        principalColumn: "PrevisionSaludID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblClientes_tblPrevisionSocial_PrevisionSocialID",
                        column: x => x.PrevisionSocialID,
                        principalTable: "tblPrevisionSocial",
                        principalColumn: "PrevisionSocialID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblTramo",
                columns: table => new
                {
                    TramoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RSHID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblTramo", x => x.TramoID);
                    table.ForeignKey(
                        name: "FK_tblTramo_tblRSH_RSHID",
                        column: x => x.RSHID,
                        principalTable: "tblRSH",
                        principalColumn: "RSHID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Rut = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TipoUsuarioID = table.Column<int>(type: "int", nullable: false),
                    ProfesionID = table.Column<int>(type: "int", nullable: false),
                    Imagen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Marca = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Firma = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_tblProfesion_ProfesionID",
                        column: x => x.ProfesionID,
                        principalTable: "tblProfesion",
                        principalColumn: "ProfesionID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_tblTipoUsuario_TipoUsuarioID",
                        column: x => x.TipoUsuarioID,
                        principalTable: "tblTipoUsuario",
                        principalColumn: "TipoUsuarioID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblCuenta",
                columns: table => new
                {
                    CuentaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProgramaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblCuenta", x => x.CuentaID);
                    table.ForeignKey(
                        name: "FK_tblCuenta_tblPrograma_ProgramaID",
                        column: x => x.ProgramaID,
                        principalTable: "tblPrograma",
                        principalColumn: "ProgramaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblAyudaSocial",
                columns: table => new
                {
                    AyudaSocialID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientesID = table.Column<int>(type: "int", nullable: false),
                    TipoPeticionID = table.Column<int>(type: "int", nullable: false),
                    PrestacionesID = table.Column<int>(type: "int", nullable: false),
                    CantidadEntregada = table.Column<int>(type: "int", nullable: false),
                    StockBodega = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DetalleRequerimiento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaElaboracion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NumInforme = table.Column<int>(type: "int", nullable: false),
                    NumAyuda = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR NumAyuda"),
                    GrupoFamiliar = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR GrupoFamiliar")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblAyudaSocial", x => x.AyudaSocialID);
                    table.ForeignKey(
                        name: "FK_tblAyudaSocial_tblClientes_ClientesID",
                        column: x => x.ClientesID,
                        principalTable: "tblClientes",
                        principalColumn: "ClientesID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblAyudaSocial_tblPrestaciones_PrestacionesID",
                        column: x => x.PrestacionesID,
                        principalTable: "tblPrestaciones",
                        principalColumn: "PrestacionesID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblAyudaSocial_tblTipoPeticion_TipoPeticionID",
                        column: x => x.TipoPeticionID,
                        principalTable: "tblTipoPeticion",
                        principalColumn: "TipoPeticionID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblGastosFamiliares",
                columns: table => new
                {
                    GastosFamiliaresID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientesID = table.Column<int>(type: "int", nullable: false),
                    Alimentacion = table.Column<int>(type: "int", nullable: false),
                    Aseo = table.Column<int>(type: "int", nullable: false),
                    Arriendo = table.Column<int>(type: "int", nullable: false),
                    Dividendo = table.Column<int>(type: "int", nullable: false),
                    Servicios = table.Column<int>(type: "int", nullable: false),
                    Combustible = table.Column<int>(type: "int", nullable: false),
                    GastosTelefono = table.Column<int>(type: "int", nullable: false),
                    Movilizacion = table.Column<int>(type: "int", nullable: false),
                    Educacion = table.Column<int>(type: "int", nullable: false),
                    Creditos = table.Column<int>(type: "int", nullable: false),
                    Salud = table.Column<int>(type: "int", nullable: false),
                    Varios = table.Column<int>(type: "int", nullable: false),
                    TotalGastosMensuales = table.Column<int>(type: "int", nullable: false),
                    Observaciones = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GrupoFamiliar = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR GrupoFamiliar")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblGastosFamiliares", x => x.GastosFamiliaresID);
                    table.ForeignKey(
                        name: "FK_tblGastosFamiliares_tblClientes_ClientesID",
                        column: x => x.ClientesID,
                        principalTable: "tblClientes",
                        principalColumn: "ClientesID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblHabitacionales",
                columns: table => new
                {
                    HabitacionalesID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientesID = table.Column<int>(type: "int", nullable: false),
                    SituacionCasaID = table.Column<int>(type: "int", nullable: false),
                    RedPublica = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Luzirregular = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SinLuz = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Agua = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AguaCamion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AguaNoria = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AguaVertiente = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Alcantarillado = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fosa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pozo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Otro = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaterialViviendaID = table.Column<int>(type: "int", nullable: false),
                    Definir = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TipoViviendaID = table.Column<int>(type: "int", nullable: false),
                    NumDormitorios = table.Column<int>(type: "int", nullable: false),
                    TotalDependecias = table.Column<int>(type: "int", nullable: false),
                    NivelAsinamiento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenenciaTerrenoID = table.Column<int>(type: "int", nullable: false),
                    Observaciones = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GrupoFamiliar = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR GrupoFamiliar")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblHabitacionales", x => x.HabitacionalesID);
                    table.ForeignKey(
                        name: "FK_tblHabitacionales_tblClientes_ClientesID",
                        column: x => x.ClientesID,
                        principalTable: "tblClientes",
                        principalColumn: "ClientesID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblHabitacionales_tblMaterialVivienda_MaterialViviendaID",
                        column: x => x.MaterialViviendaID,
                        principalTable: "tblMaterialVivienda",
                        principalColumn: "MaterialViviendaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblHabitacionales_tblSituacionCasa_SituacionCasaID",
                        column: x => x.SituacionCasaID,
                        principalTable: "tblSituacionCasa",
                        principalColumn: "SituacionCasaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblHabitacionales_tblTenenciaTerreno_TenenciaTerrenoID",
                        column: x => x.TenenciaTerrenoID,
                        principalTable: "tblTenenciaTerreno",
                        principalColumn: "TenenciaTerrenoID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblHabitacionales_tblTipoVivienda_TipoViviendaID",
                        column: x => x.TipoViviendaID,
                        principalTable: "tblTipoVivienda",
                        principalColumn: "TipoViviendaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblIngresos",
                columns: table => new
                {
                    IngresosID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientesID = table.Column<int>(type: "int", nullable: false),
                    IngresosMensuales = table.Column<int>(type: "int", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaElaboracion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GrupoFamiliar = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR GrupoFamiliar")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblIngresos", x => x.IngresosID);
                    table.ForeignKey(
                        name: "FK_tblIngresos_tblClientes_ClientesID",
                        column: x => x.ClientesID,
                        principalTable: "tblClientes",
                        principalColumn: "ClientesID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblMedioAyuda",
                columns: table => new
                {
                    MedioID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientesID = table.Column<int>(type: "int", nullable: false),
                    Orden = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Monto = table.Column<int>(type: "int", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumInforme = table.Column<int>(type: "int", nullable: false),
                    NumMedio = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR NumMedio"),
                    GrupoFamiliar = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR GrupoFamiliar")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblMedioAyuda", x => x.MedioID);
                    table.ForeignKey(
                        name: "FK_tblMedioAyuda_tblClientes_ClientesID",
                        column: x => x.ClientesID,
                        principalTable: "tblClientes",
                        principalColumn: "ClientesID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblPeticionAyuda",
                columns: table => new
                {
                    PeticionAyudaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientesID = table.Column<int>(type: "int", nullable: false),
                    PeticionTexto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumInforme = table.Column<int>(type: "int", nullable: false),
                    GrupoFamiliar = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR GrupoFamiliar")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblPeticionAyuda", x => x.PeticionAyudaID);
                    table.ForeignKey(
                        name: "FK_tblPeticionAyuda_tblClientes_ClientesID",
                        column: x => x.ClientesID,
                        principalTable: "tblClientes",
                        principalColumn: "ClientesID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblSalud",
                columns: table => new
                {
                    SaludID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientesID = table.Column<int>(type: "int", nullable: false),
                    ProcedenciaID = table.Column<int>(type: "int", nullable: false),
                    DiscapacidadID = table.Column<int>(type: "int", nullable: false),
                    AcreditacionID = table.Column<int>(type: "int", nullable: false),
                    Diagnostico = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Observacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GrupoFamiliar = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR GrupoFamiliar")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSalud", x => x.SaludID);
                    table.ForeignKey(
                        name: "FK_tblSalud_tblAcreditacion_AcreditacionID",
                        column: x => x.AcreditacionID,
                        principalTable: "tblAcreditacion",
                        principalColumn: "AcreditacionID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_tblSalud_tblClientes_ClientesID",
                        column: x => x.ClientesID,
                        principalTable: "tblClientes",
                        principalColumn: "ClientesID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblSalud_tblDiscapacidad_DiscapacidadID",
                        column: x => x.DiscapacidadID,
                        principalTable: "tblDiscapacidad",
                        principalColumn: "DiscapacidadID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblSalud_tblProcedencia_ProcedenciaID",
                        column: x => x.ProcedenciaID,
                        principalTable: "tblProcedencia",
                        principalColumn: "ProcedenciaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblSituacionActual",
                columns: table => new
                {
                    SituacionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientesID = table.Column<int>(type: "int", nullable: false),
                    SituacionActual = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GrupoFamiliar = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR GrupoFamiliar")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSituacionActual", x => x.SituacionID);
                    table.ForeignKey(
                        name: "FK_tblSituacionActual_tblClientes_ClientesID",
                        column: x => x.ClientesID,
                        principalTable: "tblClientes",
                        principalColumn: "ClientesID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblSocioeconomico",
                columns: table => new
                {
                    SocioeconomicoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientesID = table.Column<int>(type: "int", nullable: false),
                    Actividad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IngresoActividad = table.Column<int>(type: "int", nullable: false),
                    OtrosIngresos1 = table.Column<int>(type: "int", nullable: false),
                    TipoIngreso1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OtrosIngresos2 = table.Column<int>(type: "int", nullable: false),
                    TipoIngreso2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TipoPensionID = table.Column<int>(type: "int", nullable: false),
                    OtrosIngresos3 = table.Column<int>(type: "int", nullable: false),
                    TipoSubsidioID = table.Column<int>(type: "int", nullable: false),
                    OtrosIngresos4 = table.Column<int>(type: "int", nullable: false),
                    TotalIngresos = table.Column<int>(type: "int", nullable: false),
                    Observacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GrupoFamiliar = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR GrupoFamiliar")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSocioeconomico", x => x.SocioeconomicoID);
                    table.ForeignKey(
                        name: "FK_tblSocioeconomico_tblClientes_ClientesID",
                        column: x => x.ClientesID,
                        principalTable: "tblClientes",
                        principalColumn: "ClientesID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblSocioeconomico_tblTipoPension_TipoPensionID",
                        column: x => x.TipoPensionID,
                        principalTable: "tblTipoPension",
                        principalColumn: "TipoPensionID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblSocioeconomico_tblTipoSubsidio_TipoSubsidioID",
                        column: x => x.TipoSubsidioID,
                        principalTable: "tblTipoSubsidio",
                        principalColumn: "TipoSubsidioID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblCronicos",
                columns: table => new
                {
                    CronicosID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NCronicos = table.Column<int>(type: "int", nullable: false),
                    NEmbarazadas = table.Column<int>(type: "int", nullable: false),
                    RSHID = table.Column<int>(type: "int", nullable: false),
                    TramoID = table.Column<int>(type: "int", nullable: false),
                    ObservacionGrupo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GrupoFamiliar = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR GrupoFamiliar")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblCronicos", x => x.CronicosID);
                    table.ForeignKey(
                        name: "FK_tblCronicos_tblRSH_RSHID",
                        column: x => x.RSHID,
                        principalTable: "tblRSH",
                        principalColumn: "RSHID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblCronicos_tblTramo_TramoID",
                        column: x => x.TramoID,
                        principalTable: "tblTramo",
                        principalColumn: "TramoID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
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
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
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
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                name: "tblInformeSocial",
                columns: table => new
                {
                    InformeSocialID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientesID = table.Column<int>(type: "int", nullable: false),
                    UsuarioID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    NumInforme = table.Column<int>(type: "int", nullable: false),
                    FechaElaboracion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sintesis = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GrupoFamiliar = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR GrupoFamiliar")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblInformeSocial", x => x.InformeSocialID);
                    table.ForeignKey(
                        name: "FK_tblInformeSocial_AspNetUsers_UsuarioID",
                        column: x => x.UsuarioID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_tblInformeSocial_tblClientes_ClientesID",
                        column: x => x.ClientesID,
                        principalTable: "tblClientes",
                        principalColumn: "ClientesID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblPresupuesto",
                columns: table => new
                {
                    PresupuestoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PresInicial = table.Column<int>(type: "int", nullable: false),
                    PresVigente = table.Column<int>(type: "int", nullable: false),
                    CuentaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblPresupuesto", x => x.PresupuestoID);
                    table.ForeignKey(
                        name: "FK_tblPresupuesto_tblCuenta_CuentaID",
                        column: x => x.CuentaID,
                        principalTable: "tblCuenta",
                        principalColumn: "CuentaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblAsignacion",
                columns: table => new
                {
                    AsignacionDeCuentaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientesID = table.Column<int>(type: "int", nullable: false),
                    AreaGestionID = table.Column<int>(type: "int", nullable: false),
                    ProgramaID = table.Column<int>(type: "int", nullable: false),
                    CuentaID = table.Column<int>(type: "int", nullable: false),
                    PresupuestoID = table.Column<int>(type: "int", nullable: false),
                    PresEntregado = table.Column<int>(type: "int", nullable: false),
                    NumInforme = table.Column<int>(type: "int", nullable: false),
                    NumCuenta = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR NumCuenta"),
                    GrupoFamiliar = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR GrupoFamiliar")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblAsignacion", x => x.AsignacionDeCuentaID);
                    table.ForeignKey(
                        name: "FK_tblAsignacion_tblAreaGestion_AreaGestionID",
                        column: x => x.AreaGestionID,
                        principalTable: "tblAreaGestion",
                        principalColumn: "AreaGestionID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_tblAsignacion_tblClientes_ClientesID",
                        column: x => x.ClientesID,
                        principalTable: "tblClientes",
                        principalColumn: "ClientesID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblAsignacion_tblCuenta_CuentaID",
                        column: x => x.CuentaID,
                        principalTable: "tblCuenta",
                        principalColumn: "CuentaID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_tblAsignacion_tblPresupuesto_PresupuestoID",
                        column: x => x.PresupuestoID,
                        principalTable: "tblPresupuesto",
                        principalColumn: "PresupuestoID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_tblAsignacion_tblPrograma_ProgramaID",
                        column: x => x.ProgramaID,
                        principalTable: "tblPrograma",
                        principalColumn: "ProgramaID",
                        onDelete: ReferentialAction.NoAction);
                });

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
                name: "IX_AspNetUsers_ProfesionID",
                table: "AspNetUsers",
                column: "ProfesionID");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_TipoUsuarioID",
                table: "AspNetUsers",
                column: "TipoUsuarioID");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_tblAcreditacion_DiscapacidadID",
                table: "tblAcreditacion",
                column: "DiscapacidadID");

            migrationBuilder.CreateIndex(
                name: "IX_tblAsignacion_AreaGestionID",
                table: "tblAsignacion",
                column: "AreaGestionID");

            migrationBuilder.CreateIndex(
                name: "IX_tblAsignacion_ClientesID",
                table: "tblAsignacion",
                column: "ClientesID");

            migrationBuilder.CreateIndex(
                name: "IX_tblAsignacion_CuentaID",
                table: "tblAsignacion",
                column: "CuentaID");

            migrationBuilder.CreateIndex(
                name: "IX_tblAsignacion_PresupuestoID",
                table: "tblAsignacion",
                column: "PresupuestoID");

            migrationBuilder.CreateIndex(
                name: "IX_tblAsignacion_ProgramaID",
                table: "tblAsignacion",
                column: "ProgramaID");

            migrationBuilder.CreateIndex(
                name: "IX_tblAyudaSocial_ClientesID",
                table: "tblAyudaSocial",
                column: "ClientesID");

            migrationBuilder.CreateIndex(
                name: "IX_tblAyudaSocial_PrestacionesID",
                table: "tblAyudaSocial",
                column: "PrestacionesID");

            migrationBuilder.CreateIndex(
                name: "IX_tblAyudaSocial_TipoPeticionID",
                table: "tblAyudaSocial",
                column: "TipoPeticionID");

            migrationBuilder.CreateIndex(
                name: "IX_tblClientes_EstadoCivilID",
                table: "tblClientes",
                column: "EstadoCivilID");

            migrationBuilder.CreateIndex(
                name: "IX_tblClientes_IndiceEscolaridadID",
                table: "tblClientes",
                column: "IndiceEscolaridadID");

            migrationBuilder.CreateIndex(
                name: "IX_tblClientes_NacionalidadID",
                table: "tblClientes",
                column: "NacionalidadID");

            migrationBuilder.CreateIndex(
                name: "IX_tblClientes_ParentescoID",
                table: "tblClientes",
                column: "ParentescoID");

            migrationBuilder.CreateIndex(
                name: "IX_tblClientes_PrevisionSaludID",
                table: "tblClientes",
                column: "PrevisionSaludID");

            migrationBuilder.CreateIndex(
                name: "IX_tblClientes_PrevisionSocialID",
                table: "tblClientes",
                column: "PrevisionSocialID");

            migrationBuilder.CreateIndex(
                name: "IX_tblCronicos_RSHID",
                table: "tblCronicos",
                column: "RSHID");

            migrationBuilder.CreateIndex(
                name: "IX_tblCronicos_TramoID",
                table: "tblCronicos",
                column: "TramoID");

            migrationBuilder.CreateIndex(
                name: "IX_tblCuenta_ProgramaID",
                table: "tblCuenta",
                column: "ProgramaID");

            migrationBuilder.CreateIndex(
                name: "IX_tblGastosFamiliares_ClientesID",
                table: "tblGastosFamiliares",
                column: "ClientesID");

            migrationBuilder.CreateIndex(
                name: "IX_tblHabitacionales_ClientesID",
                table: "tblHabitacionales",
                column: "ClientesID");

            migrationBuilder.CreateIndex(
                name: "IX_tblHabitacionales_MaterialViviendaID",
                table: "tblHabitacionales",
                column: "MaterialViviendaID");

            migrationBuilder.CreateIndex(
                name: "IX_tblHabitacionales_SituacionCasaID",
                table: "tblHabitacionales",
                column: "SituacionCasaID");

            migrationBuilder.CreateIndex(
                name: "IX_tblHabitacionales_TenenciaTerrenoID",
                table: "tblHabitacionales",
                column: "TenenciaTerrenoID");

            migrationBuilder.CreateIndex(
                name: "IX_tblHabitacionales_TipoViviendaID",
                table: "tblHabitacionales",
                column: "TipoViviendaID");

            migrationBuilder.CreateIndex(
                name: "IX_tblInformeSocial_ClientesID",
                table: "tblInformeSocial",
                column: "ClientesID");

            migrationBuilder.CreateIndex(
                name: "IX_tblInformeSocial_UsuarioID",
                table: "tblInformeSocial",
                column: "UsuarioID");

            migrationBuilder.CreateIndex(
                name: "IX_tblIngresos_ClientesID",
                table: "tblIngresos",
                column: "ClientesID");

            migrationBuilder.CreateIndex(
                name: "IX_tblMedioAyuda_ClientesID",
                table: "tblMedioAyuda",
                column: "ClientesID");

            migrationBuilder.CreateIndex(
                name: "IX_tblPeticionAyuda_ClientesID",
                table: "tblPeticionAyuda",
                column: "ClientesID");

            migrationBuilder.CreateIndex(
                name: "IX_tblPresupuesto_CuentaID",
                table: "tblPresupuesto",
                column: "CuentaID");

            migrationBuilder.CreateIndex(
                name: "IX_tblPrograma_AreaGestionID",
                table: "tblPrograma",
                column: "AreaGestionID");

            migrationBuilder.CreateIndex(
                name: "IX_tblSalud_AcreditacionID",
                table: "tblSalud",
                column: "AcreditacionID");

            migrationBuilder.CreateIndex(
                name: "IX_tblSalud_ClientesID",
                table: "tblSalud",
                column: "ClientesID");

            migrationBuilder.CreateIndex(
                name: "IX_tblSalud_DiscapacidadID",
                table: "tblSalud",
                column: "DiscapacidadID");

            migrationBuilder.CreateIndex(
                name: "IX_tblSalud_ProcedenciaID",
                table: "tblSalud",
                column: "ProcedenciaID");

            migrationBuilder.CreateIndex(
                name: "IX_tblSituacionActual_ClientesID",
                table: "tblSituacionActual",
                column: "ClientesID");

            migrationBuilder.CreateIndex(
                name: "IX_tblSocioeconomico_ClientesID",
                table: "tblSocioeconomico",
                column: "ClientesID");

            migrationBuilder.CreateIndex(
                name: "IX_tblSocioeconomico_TipoPensionID",
                table: "tblSocioeconomico",
                column: "TipoPensionID");

            migrationBuilder.CreateIndex(
                name: "IX_tblSocioeconomico_TipoSubsidioID",
                table: "tblSocioeconomico",
                column: "TipoSubsidioID");

            migrationBuilder.CreateIndex(
                name: "IX_tblTramo_RSHID",
                table: "tblTramo",
                column: "RSHID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                name: "tblAsignacion");

            migrationBuilder.DropTable(
                name: "tblAyudaSocial");

            migrationBuilder.DropTable(
                name: "tblCronicos");

            migrationBuilder.DropTable(
                name: "tblGastosFamiliares");

            migrationBuilder.DropTable(
                name: "tblHabitacionales");

            migrationBuilder.DropTable(
                name: "tblInformeSocial");

            migrationBuilder.DropTable(
                name: "tblIngresos");

            migrationBuilder.DropTable(
                name: "tblMedioAyuda");

            migrationBuilder.DropTable(
                name: "tblPeticion");

            migrationBuilder.DropTable(
                name: "tblPeticionAyuda");

            migrationBuilder.DropTable(
                name: "tblSalud");

            migrationBuilder.DropTable(
                name: "tblSituacionActual");

            migrationBuilder.DropTable(
                name: "tblSocioeconomico");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "tblPresupuesto");

            migrationBuilder.DropTable(
                name: "tblPrestaciones");

            migrationBuilder.DropTable(
                name: "tblTipoPeticion");

            migrationBuilder.DropTable(
                name: "tblTramo");

            migrationBuilder.DropTable(
                name: "tblMaterialVivienda");

            migrationBuilder.DropTable(
                name: "tblSituacionCasa");

            migrationBuilder.DropTable(
                name: "tblTenenciaTerreno");

            migrationBuilder.DropTable(
                name: "tblTipoVivienda");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "tblAcreditacion");

            migrationBuilder.DropTable(
                name: "tblProcedencia");

            migrationBuilder.DropTable(
                name: "tblClientes");

            migrationBuilder.DropTable(
                name: "tblTipoPension");

            migrationBuilder.DropTable(
                name: "tblTipoSubsidio");

            migrationBuilder.DropTable(
                name: "tblCuenta");

            migrationBuilder.DropTable(
                name: "tblRSH");

            migrationBuilder.DropTable(
                name: "tblProfesion");

            migrationBuilder.DropTable(
                name: "tblTipoUsuario");

            migrationBuilder.DropTable(
                name: "tblDiscapacidad");

            migrationBuilder.DropTable(
                name: "tblEstadoCivil");

            migrationBuilder.DropTable(
                name: "tblIndiceEscolaridad");

            migrationBuilder.DropTable(
                name: "tblNacionalidad");

            migrationBuilder.DropTable(
                name: "tblParentesco");

            migrationBuilder.DropTable(
                name: "tblPrevisionSalud");

            migrationBuilder.DropTable(
                name: "tblPrevisionSocial");

            migrationBuilder.DropTable(
                name: "tblPrograma");

            migrationBuilder.DropTable(
                name: "tblAreaGestion");

            migrationBuilder.DropSequence(
                name: "GrupoFamiliar");

            migrationBuilder.DropSequence(
                name: "GrupoFamiliarAsignacion");

            migrationBuilder.DropSequence(
                name: "GrupoFamiliarAyuda");

            migrationBuilder.DropSequence(
                name: "GrupoFamiliarCronicos");

            migrationBuilder.DropSequence(
                name: "GrupoFamiliarGastos");

            migrationBuilder.DropSequence(
                name: "GrupoFamiliarHabitacionales");

            migrationBuilder.DropSequence(
                name: "GrupoFamiliarInforme");

            migrationBuilder.DropSequence(
                name: "GrupoFamiliarIngresos");

            migrationBuilder.DropSequence(
                name: "GrupoFamiliarMedioAyuda");

            migrationBuilder.DropSequence(
                name: "GrupoFamiliarPeticionAyuda");

            migrationBuilder.DropSequence(
                name: "GrupoFamiliarSalud");

            migrationBuilder.DropSequence(
                name: "GrupoFamiliarSituacion");

            migrationBuilder.DropSequence(
                name: "GrupoFamiliarSocioeconomico");

            migrationBuilder.DropSequence(
                name: "NumAyuda");

            migrationBuilder.DropSequence(
                name: "NumCuenta");

            migrationBuilder.DropSequence(
                name: "NumMedio");
        }
    }
}
