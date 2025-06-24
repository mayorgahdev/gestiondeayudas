using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.Data;

namespace SistemaSocial.ViewModel.Datos
{
    public class Datos_Retornar
    {

        /*RETORNO DE AYUDAS*/
        public List<RetornarAyudasMes> RetornarAyudasMes()
        {
            List<RetornarAyudasMes> listaAyudas = new List<RetornarAyudasMes>();

            using (SqlConnection _context = new SqlConnection
                ("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = BDSocial; Integrated Security = True;")) {
                //("Data Source=DESKTOP-FB4H9LQ\\SQLEXPRESS; Initial Catalog=BDSocial; Integrated Security=True;")) {
                string query = "RetornarAyudasMes";
                SqlCommand cmd = new SqlCommand(query, _context);
                cmd.CommandType = CommandType.StoredProcedure;

                _context.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        listaAyudas.Add(new RetornarAyudasMes()
                        {
                            mes = dr["mes"].ToString(),
                            cantidad = int.Parse(dr["cantidad"].ToString()),
                        });
                    }
                }
            }
            return listaAyudas;
        }
        public List<RetornarAyudas3Meses> RetornarAyudas3Meses()
        {
            List<RetornarAyudas3Meses> listaAyudas = new List<RetornarAyudas3Meses>();

            using (SqlConnection _context = new SqlConnection
                ("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = BDSocial; Integrated Security = True;")) {
                //("Data Source=DESKTOP-FB4H9LQ\\SQLEXPRESS; Initial Catalog=BDSocial; Integrated Security=True;")) {
                string query = "RetornarAyudas3Meses";
                SqlCommand cmd = new SqlCommand(query, _context);
                cmd.CommandType = CommandType.StoredProcedure;

                _context.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        listaAyudas.Add(new RetornarAyudas3Meses()
                        {
                            mes = dr["mes"].ToString(),
                            cantidad = int.Parse(dr["cantidad"].ToString()),
                        });
                    }
                }
            }
            return listaAyudas;
        }
        public List<RetornarAyudasAnual> RetornarAyudasAnual()
        {
            List<RetornarAyudasAnual> listaAyudas = new List<RetornarAyudasAnual>();

            using (SqlConnection _context = new SqlConnection
                ("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = BDSocial; Integrated Security = True;")) {
                //("Data Source=DESKTOP-FB4H9LQ\\SQLEXPRESS; Initial Catalog=BDSocial; Integrated Security=True;")) {
                string query = "RetornarAyudasAnual";
                SqlCommand cmd = new SqlCommand(query, _context);
                cmd.CommandType = CommandType.StoredProcedure;

                _context.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        listaAyudas.Add(new RetornarAyudasAnual()
                        {
                            mes = dr["mes"].ToString(),
                            cantidad = int.Parse(dr["cantidad"].ToString()),
                        });
                    }
                }
            }
            return listaAyudas;
        }


        /*RETORNO DE PRESTACIONES*/
        public List<RetornarPrestacionesMes> RetornarPrestacionesMes()
        {
            List<RetornarPrestacionesMes> listaPrestaciones = new List<RetornarPrestacionesMes>();

            using (SqlConnection _context = new SqlConnection
                ("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = BDSocial; Integrated Security = True;")) {
                //("Data Source=DESKTOP-FB4H9LQ\\SQLEXPRESS; Initial Catalog=BDSocial; Integrated Security=True;")) {
                string query = "RetornarPrestacionesMes";
                SqlCommand cmd = new SqlCommand(query, _context);
                cmd.CommandType = CommandType.StoredProcedure;

                _context.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        listaPrestaciones.Add(new RetornarPrestacionesMes()
                        {
                            Prestacion = dr["Prestacion"].ToString(),
                            Cantidad = int.Parse(dr["Cantidad"].ToString()),
                        });
                    }
                }
            }
            return listaPrestaciones;
        }
        public List<RetornarPrestaciones3Meses> RetornarPrestaciones3Meses()
        {
            List<RetornarPrestaciones3Meses> listaPrestaciones = new List<RetornarPrestaciones3Meses>();

            using (SqlConnection _context = new SqlConnection
                ("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = BDSocial; Integrated Security = True;")) {
                //("Data Source=DESKTOP-FB4H9LQ\\SQLEXPRESS; Initial Catalog=BDSocial; Integrated Security=True;")) {
                string query = "RetornarPrestaciones3Meses";
                SqlCommand cmd = new SqlCommand(query, _context);
                cmd.CommandType = CommandType.StoredProcedure;

                _context.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        listaPrestaciones.Add(new RetornarPrestaciones3Meses()
                        {
                            Prestacion = dr["Prestacion"].ToString(),
                            Cantidad = int.Parse(dr["Cantidad"].ToString()),
                        });
                    }
                }
            }
            return listaPrestaciones;
        }
        public List<RetornarPrestacionesAnual> RetornarPrestacionesAnual()
        {
            List<RetornarPrestacionesAnual> listaPrestaciones = new List<RetornarPrestacionesAnual>();

            using (SqlConnection _context = new SqlConnection
                ("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = BDSocial; Integrated Security = True;")) {
                //("Data Source=DESKTOP-FB4H9LQ\\SQLEXPRESS; Initial Catalog=BDSocial; Integrated Security=True;")) {
                string query = "RetornarPrestacionesAnual";
                SqlCommand cmd = new SqlCommand(query, _context);
                cmd.CommandType = CommandType.StoredProcedure;

                _context.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        listaPrestaciones.Add(new RetornarPrestacionesAnual()
                        {
                            Prestacion = dr["Prestacion"].ToString(),
                            Cantidad = int.Parse(dr["Cantidad"].ToString()),
                        });
                    }
                }
            }
            return listaPrestaciones;
        }
    }
}
