using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;
using System.Data;
using System.Collections;
namespace MPP
{
   public class MPPviaje
    {
        public MPPviaje()
        {
            oDatos = new Acceso();
            Hdatos = new Hashtable();
        }
        Acceso oDatos;
        Hashtable Hdatos;
        public bool Guardar_Viaje(BEViaje viaje)
        {
            if (viaje.id == 0)
            {
                string consulta = "S_Crear_Viaje";
                Hdatos = new Hashtable();
                Hdatos.Add("@codCamion", viaje.camion);
                Hdatos.Add("@codProducto", viaje.producto);
                Hdatos.Add("@cantidadPallets", viaje.cantidad_Pallets);
                Hdatos.Add("@distancia", viaje.cantidad_KM);
                Hdatos.Add("@fecha", viaje.fecha);
                Hdatos.Add("@estado", viaje.estado);
                return oDatos.Escribir(consulta, Hdatos);
            }
            else
            {
                string consulta = "S_Modificar_Viaje";
                Hdatos = new Hashtable();
                Hdatos.Add("codigo", viaje.id);
                Hdatos.Add("@codCamion", viaje.camion);
                Hdatos.Add("@codProoducto", viaje.producto);
                Hdatos.Add("@cantidadPallets", viaje.cantidad_Pallets);
                Hdatos.Add("@distancia", viaje.cantidad_KM);
                Hdatos.Add("@fecha", viaje.fecha);
                Hdatos.Add("@estado", viaje.estado);
                return oDatos.Escribir(consulta, Hdatos);
            }
        }
        
        public bool Eliminar_Viaje(int ID)
        {
            string consulta = "S_Eliminar_Viaje";
            Hdatos = new Hashtable();
            Hdatos.Add("codigo", ID);
            return oDatos.Escribir(consulta, Hdatos);
        }

        public bool actualizar_Estado(int ID,string estado)
        {
            string consulta = "S_Actualizar_Estado_Viaje";
            Hdatos = new Hashtable();
            Hdatos.Add("@codigo", ID);
            Hdatos.Add("@estado", estado);
            return oDatos.Escribir(consulta,Hdatos);
        }
    }
}
