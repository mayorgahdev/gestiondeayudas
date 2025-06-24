using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaSocial.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaSocial.ViewModel
{
    public class InformeSocialView
    {
        public int ID { get; set; }

        // CLIENTES Y GRUPO FAMILIAR
        public List<Clientes> ListaClientes { get; set; }
        public List<Clientes> ListaClientesGrupo { get; set; }


        // GRUPO
        public List<Clientes> ListaGrupo { get; set; }

        public List<Cronicos> ListaCronicos { get; set; }


        // SALUD Y GRUPO FAMILIAR
        public List<Salud> ListaSalud { get; set; }
        public List<Salud> ListaSaludGrupo { get; set; }


        // SOCIOECONÓMICO Y GRUPO FAMILIAR
        public List<Socioeconomico> ListaSocioeconomico { get; set; }
        public List<Socioeconomico> ListaSocioeconomicoGrupo { get; set; }


        // HABITACIONALES Y GRUPO FAMILIAR
        public List<Habitacionales> ListaHabitacionales { get; set; }
        public List<Habitacionales> ListaHabitacionalesGrupo { get; set; }


        // SITUACION ACTUAL Y GRUPO FAMILIAR
        public List<Situacion> listaSituacion { get; set; }
        public List<Situacion> listaSituacionGrupo { get; set; }


        // GASTOS FAMILIARES Y GRUPO FAMILIAR
        public List<GastosFamiliares> ListaGastosFamiliares { get; set; }
        public List<GastosFamiliares> ListaGastosFamiliaresGrupo { get; set; }


        // AYUDAS SOCIALES
        public List<AyudaSocial> ListaAyudaSocial { get; set; }
        public List<AyudaSocial> ListaAyudaSocialGrupo { get; set; }

        // PETICION AYUDAS
        public List<PeticionAyuda> ListPeticionAyudas { get; set; }
        public List<PeticionAyuda> ListPeticionAyudasGrupo { get; set; }


        // MEDIO DE HACER EFECTIVA LA AYUDA SOCIAL
        public List<MedioAyuda> ListaMedio { get; set; }
        public List<MedioAyuda> ListaMedioGrupo { get; set; }


        // ASIGNACION DE CUENTAS
        public List<AsignacionDeCuenta> ListaAsignacion { get; set; }
        public List<AsignacionDeCuenta> ListaAsignacionGrupo { get; set; }


        public List<InformeSocial> ListaInformeSocial { get; set; }
        public List<Usuario> ListaUsuarios { get; set; }


        //BUSQUEDA
        public SelectList Nombre { get; set; }
        public string RutCliente { get; set; }
        public string Busqueda { get; set; }


        //PAGINADO
        public int Paginas { get; set; }
        public int CantidadPaginas { get; set; }
    }
}
