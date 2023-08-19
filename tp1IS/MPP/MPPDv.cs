using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using System.Data;
using System.Collections;
using BE;
namespace MPP
{
   public class MPPDv
    {
        public MPPDv()
        {
            datos = new Acceso();
        }
        Acceso datos;
        Hashtable Hdatos;

        public bool actualizarDV(string DV)
        {
            try
            {
               

                Hdatos = new Hashtable();
                string Consulta = "S_Actualizar_DV";
                Hdatos.Add("@dv", DV);
                return datos.Escribir(Consulta, Hdatos);
            }
            catch (NullReferenceException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string BuscarDVS()
        {
            try
            {
          
                DataTable Ds2 = new DataTable();
               
                string DigitovBaseDeDatos = string.Empty;
                Hdatos = new Hashtable();
                string Consulta = "S_buscar_DV";
                BE.DigitoV DV = new BE.DigitoV();
                Ds2 = datos.Leer(Consulta,null);
                


                foreach (DataRow fila in Ds2.Rows)
                {
                    
                    DigitovBaseDeDatos= fila["dv"].ToString();
                    
                }
                return DigitovBaseDeDatos;
            }
            catch (NullReferenceException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<string> BuscarDVUsuarios()
        {
            try
            {
                
                
                DataTable Ds2 = new DataTable();
                List<string> ListaDVUsers = new List<string>();
                Hdatos = new Hashtable();
                string Consulta = "S_Buscar_DVxUsuarios";
             
                Ds2 = datos.Leer(Consulta,null);
                foreach (DataRow fila in Ds2.Rows)
                {
                    string DVU;
                    DVU = fila["dv"].ToString();
                    ListaDVUsers.Add(DVU);
                }
                return ListaDVUsers;
            }
            catch (NullReferenceException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
