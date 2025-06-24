using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SistemaSocial.Models
{
    public class AppDbContext : IdentityDbContext<Usuario>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            /* LOCAL DB */
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB; Initial Catalog=BDSocial; Integrated Security=True;");

            /* SQL EXPRESS */
            //optionsBuilder.UseSqlServer("Data Source=DESKTOP-FB4H9LQ\\SQLEXPRESS; Initial Catalog=BDSocial; Integrated Security=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            /*CLIENTES*/
                //GRUPO FAMILIAR
                modelBuilder.HasSequence<int>("GrupoFamiliar").StartsAt(1).IncrementsBy(1);
                modelBuilder.Entity<Clientes>().Property(x => x.GrupoFamiliar).HasDefaultValueSql("NEXT VALUE FOR GrupoFamiliar");

            /*SALUD*/
                //GRUPO FAMILIAR
                modelBuilder.HasSequence<int>("GrupoFamiliarSalud").StartsAt(1).IncrementsBy(1);
                modelBuilder.Entity<Salud>().Property(x => x.GrupoFamiliar).HasDefaultValueSql("NEXT VALUE FOR GrupoFamiliar");

            /*CRONICOS*/
                //GRUPO FAMILIAR
                modelBuilder.HasSequence<int>("GrupoFamiliarCronicos").StartsAt(1).IncrementsBy(1);
                modelBuilder.Entity<Cronicos>().Property(x => x.GrupoFamiliar).HasDefaultValueSql("NEXT VALUE FOR GrupoFamiliar");

            /*SOCIOECONOMICOS*/
                //GRUPO FAMILIAR
                modelBuilder.HasSequence<int>("GrupoFamiliarSocioeconomico").StartsAt(1).IncrementsBy(1);
                modelBuilder.Entity<Socioeconomico>().Property(x => x.GrupoFamiliar).HasDefaultValueSql("NEXT VALUE FOR GrupoFamiliar");


/*-----------------------------------------------------------------------------------------------------------------------------------------------------------------------*/


            /*HABITACIONALES*/
                //GRUPO FAMILIAR
                modelBuilder.HasSequence<int>("GrupoFamiliarHabitacionales").StartsAt(1).IncrementsBy(1);
                modelBuilder.Entity<Habitacionales>().Property(x => x.GrupoFamiliar).HasDefaultValueSql("NEXT VALUE FOR GrupoFamiliar");

            /*SITUACION ACTUAL*/
                //GRUPO FAMILIAR
                modelBuilder.HasSequence<int>("GrupoFamiliarSituacion").StartsAt(1).IncrementsBy(1);
                modelBuilder.Entity<Situacion>().Property(x => x.GrupoFamiliar).HasDefaultValueSql("NEXT VALUE FOR GrupoFamiliar");

            /*GASTOS FAMILIARES*/
                //GRUPO FAMILIAR
                modelBuilder.HasSequence<int>("GrupoFamiliarGastos").StartsAt(1).IncrementsBy(1);
                modelBuilder.Entity<GastosFamiliares>().Property(x => x.GrupoFamiliar).HasDefaultValueSql("NEXT VALUE FOR GrupoFamiliar");

            /*DECLARACION DE INGRESOS*/
                //GRUPO FAMILIAR
                modelBuilder.HasSequence<int>("GrupoFamiliarIngresos").StartsAt(1).IncrementsBy(1);
                modelBuilder.Entity<Ingresos>().Property(x => x.GrupoFamiliar).HasDefaultValueSql("NEXT VALUE FOR GrupoFamiliar");


/*-----------------------------------------------------------------------------------------------------------------------------------------------------------------------*/


            /*AYUDAS SOCIALES*/
                modelBuilder.HasSequence<int>("NumAyuda").StartsAt(1).IncrementsBy(1);
                modelBuilder.Entity<AyudaSocial>().Property(x => x.NumAyuda).HasDefaultValueSql("NEXT VALUE FOR NumAyuda");

                //GRUPO FAMILIAR
                modelBuilder.HasSequence<int>("GrupoFamiliarAyuda").StartsAt(1).IncrementsBy(1);
                modelBuilder.Entity<AyudaSocial>().Property(x => x.GrupoFamiliar).HasDefaultValueSql("NEXT VALUE FOR GrupoFamiliar");

            //PETICION AYUDA
                //GRUPO FAMILIAR
                modelBuilder.HasSequence<int>("GrupoFamiliarPeticionAyuda").StartsAt(1).IncrementsBy(1);
                modelBuilder.Entity<PeticionAyuda>().Property(x => x.GrupoFamiliar).HasDefaultValueSql("NEXT VALUE FOR GrupoFamiliar");

            /*MEDIO AYUDA*/
            modelBuilder.HasSequence<int>("NumMedio").StartsAt(1).IncrementsBy(1);
                modelBuilder.Entity<MedioAyuda>().Property(x => x.NumMedio).HasDefaultValueSql("NEXT VALUE FOR NumMedio");

                //GRUPO FAMILIAR
                modelBuilder.HasSequence<int>("GrupoFamiliarMedioAyuda").StartsAt(1).IncrementsBy(1);
                modelBuilder.Entity<MedioAyuda>().Property(x => x.GrupoFamiliar).HasDefaultValueSql("NEXT VALUE FOR GrupoFamiliar");

            /*ASIGNACION DE CUENTAS*/
                modelBuilder.HasSequence<int>("NumCuenta").StartsAt(1).IncrementsBy(1);
                modelBuilder.Entity<AsignacionDeCuenta>().Property(x => x.NumCuenta).HasDefaultValueSql("NEXT VALUE FOR NumCuenta");

            //GRUPO FAMILIAR
                modelBuilder.HasSequence<int>("GrupoFamiliarAsignacion").StartsAt(1).IncrementsBy(1);
                modelBuilder.Entity<AsignacionDeCuenta>().Property(x => x.GrupoFamiliar).HasDefaultValueSql("NEXT VALUE FOR GrupoFamiliar");

            /*INFORME*/
                //GRUPO FAMILIAR
                modelBuilder.HasSequence<int>("GrupoFamiliarInforme").StartsAt(1).IncrementsBy(1);
                modelBuilder.Entity<InformeSocial>().Property(x => x.GrupoFamiliar).HasDefaultValueSql("NEXT VALUE FOR GrupoFamiliar");
        }



        //TABLAS PARA USUARIOS DEL SISTEMA
        public DbSet<TipoUsuario> tblTipoUsuario { get; set; }
        public DbSet<Profesion> tblProfesion { get; set; }


        //TABLAS PARA DATOS DE CLIENTES DEL SISTEMA
        public DbSet<Clientes> tblClientes { get; set; }
        public DbSet<Nacionalidad> tblNacionalidad { get; set; }
        public DbSet<PrevisionSocial> tblPrevisionSocial { get; set; }
        public DbSet<PrevisionSalud> tblPrevisionSalud { get; set; }
        public DbSet<IndiceEscolaridad> tblIndiceEscolaridad { get; set; }
        public DbSet<Parentesco> tblParentesco { get; set; }
        public DbSet<EstadoCivil> tblEstadoCivil { get; set; }


        //TABLAS PARA DATOS DE SALUD DEL SISTEMA
        public DbSet<Salud> tblSalud { get; set; }
        public DbSet<Procedencia> tblProcedencia { get; set; }
        public DbSet<Discapacidad> tblDiscapacidad { get; set; }
        public DbSet<Acreditacion> tblAcreditacion { get; set; }


        //TABLAS PARA DATOS CRONICOS DEL SISTEMA
        public DbSet<Cronicos> tblCronicos { get; set; }
        public DbSet<RSH> tblRSH { get; set; }
        public DbSet<Tramo> tblTramo { get; set; }


        //TABLAS PARA DATOS SOCIOECONOMICOS DEL SISTEMA
        public DbSet<Socioeconomico> tblSocioeconomico { get; set; }
        public DbSet<TipoPension> tblTipoPension { get; set; }
        public DbSet<TipoSubsidio> tblTipoSubsidio { get; set; }


        //TABLAS PARA DATOS HABITACIONALES DEL SISTEMA
        public DbSet<Habitacionales> tblHabitacionales { get; set; }
        public DbSet<SituacionCasa> tblSituacionCasa { get; set; }
        public DbSet<TipoVivienda> tblTipoVivienda { get; set; }
        public DbSet<MaterialVivienda> tblMaterialVivienda { get; set; }
        public DbSet<TenenciaTerreno> tblTenenciaTerreno { get; set; }


        //SITUACION ACTUAL DE DATOS HABITACIONALES
        public DbSet<Situacion> tblSituacionActual { get; set; }


        //TABLAS PARA DATOS DE GASTOS FAMILIARES DEL SISTEMA
        public DbSet<GastosFamiliares> tblGastosFamiliares { get; set; }
        public DbSet<Ingresos> tblIngresos { get; set; }


        //TABLAS PARA DATOS DE INFORME SOCIAL Y AYUDA SOCIAL DEL SISTEMA
        public DbSet<AyudaSocial> tblAyudaSocial { get; set; }
        public DbSet<Prestaciones> tblPrestaciones { get; set; }
        public DbSet<TipoPeticion> tblTipoPeticion { get; set; }

        public DbSet<PeticionAyuda> tblPeticionAyuda { get; set; }
        public DbSet<Peticion> tblPeticion { get; set; }
    

        //TABLAS PARA ASIGNACION DE CUENTA
        public DbSet<AsignacionDeCuenta> tblAsignacion { get; set; }
        public DbSet<AreaGestion> tblAreaGestion { get; set; }
        public DbSet<Programa> tblPrograma { get; set; }
        public DbSet<Cuenta> tblCuenta { get; set; }
        public DbSet<Presupuesto> tblPresupuesto { get; set; }


        //TABLAS PARA MEDIO DE HACER EFECTIVA LA AYUDA.
        public DbSet<MedioAyuda> tblMedioAyuda { get; set; }


        //TABLAS PARA DATOS DE INFORME SOCIAL
        public DbSet<InformeSocial> tblInformeSocial { get; set; }


        public AppDbContext() {}
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}
    }
}
