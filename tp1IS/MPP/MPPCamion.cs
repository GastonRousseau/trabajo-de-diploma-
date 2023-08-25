using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;
using System.Collections;
using System.Data;
using System.Collections;
using servicios;
namespace MPP
{
   public class MPPCamion
    {
        public MPPCamion()
        {
            Odatos = new Acceso();
            Hdatos = new Hashtable();
        }
        Acceso Odatos;
        Hashtable Hdatos;
        public bool CrearCamion(BECamion camion)
        {
            if (camion.id == 0)
            {
                string consulta = "S_Crear_Camion";
                Hdatos = new Hashtable();
                Hdatos.Add("@patente", camion.patente);
                Hdatos.Add("@tipo", camion.tipo);
                Hdatos.Add("@capacidad_pallets", camion.capacidad_Pallets);
              //  Hdatos.Add("@codigo_conductor", camion.conductor.id);
                return Odatos.Escribir(consulta, Hdatos);
            }
            else
            {
                string consulta = "S_Modificar_Camion";
                Hdatos = new Hashtable();
                Hdatos.Add("@codigo", camion.id);
                Hdatos.Add("@patente", camion.patente);
                Hdatos.Add("@tipo", camion.tipo);
                Hdatos.Add("@capacidad_pallets", camion.capacidad_Pallets);
                Hdatos.Add("@codigo_conductor", camion.conductor.id);
                return Odatos.Escribir(consulta, Hdatos);
            }
           
        }

        public IList<BECamion> TraerCamiones(string patente, int pag)
        {
            return Odatos.TraerCamiones(patente, pag);
        }


        public bool Desvincular_ConductoryCamion(int camion)
        {
            string consulta = "S_desvincular_ConductoryCamion";
            Hdatos = new Hashtable();
            Hdatos.Add("@codCamion", camion);
            return Odatos.Escribir(consulta, Hdatos);
        }
        public bool Unicr_ConductoryCamion(int camion,int conductor)
        {
            string consulta = "S_unir_ConductoryCamion";
            Hdatos = new Hashtable();
            Hdatos.Add("@codCamion",camion);
            Hdatos.Add("@codConductor",conductor);
            return Odatos.Escribir(consulta, Hdatos);
        }
        public bool Verificar_Patente(string patente)
        {
            try
            {
                DataTable DT = new DataTable();
                Acceso oDatos = new Acceso();
                Hdatos = new Hashtable();
                string Consulta = "S_Existencia_Patente";
                Hdatos.Add("@patente", patente);
                DT = oDatos.Leer(Consulta, Hdatos);
                foreach (DataRow fila in DT.Rows)
                {
                    if (patente == fila["patente"].ToString()) return true;
                }
                return false;
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

        public List<BECamion> Camiones_Disponibles(DateTime fecha,int pallets)
        {
            string consulta = "S_Traer_Camiones_DIsponibles";
            Hdatos = new Hashtable();
            Hdatos.Add("@fecha",fecha);
            Hdatos.Add("@pallets_deseados",pallets);
            DataTable DT = Odatos.Leer(consulta, Hdatos);
            List<BECamion> Camiones = new List<BECamion>();
            foreach(DataRow fila in DT.Rows)
            {
                BECamion camion = new BECamion();
                camion.id = Convert.ToInt32(fila["codigo"]);
                camion.patente = fila["patente"].ToString();
                camion.tipo = fila["tipo"].ToString();
                camion.capacidad_Pallets = Convert.ToInt32(fila["capacidad_pallets"]);
                string consulta2 = "S_Traer_Conductor";
                Hdatos = new Hashtable();
                Hdatos.Add("@codigo", fila["codigo_conductor"]);
                DataTable dt2 = Odatos.Leer(consulta2, Hdatos);

                foreach(DataRow reader2 in dt2.Rows)
                {
                    BEUsuario user = new BEUsuario();
                    user.id = Convert.ToInt32(reader2["id"]);
                    user.user = reader2["username"].ToString();
                    user.password = reader2["password"].ToString();
                    user.birthDate = reader2["birthdate"].ToString();
                    user.active = Convert.ToInt32(reader2["active"]);
                    string Dencrip = reader2["Direccion"].ToString();
                    user.Direccion = Convert.ToString(encriptar.Desencriptar(Dencrip));
                    camion.conductor = user;
                }
                Camiones.Add(camion);
            }
            return Camiones;
        }
        public bool BorrarCamion(int ID)
        {
            string consulta = "S_Eliminar_Camion";
            Hdatos = new Hashtable();
            Hdatos.Add("@codigo", ID);
            return Odatos.Escribir(consulta, Hdatos);
        }

        
    }

}
