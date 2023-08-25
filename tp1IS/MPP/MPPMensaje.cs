using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;
using System.Collections;
using System.Data;
namespace MPP
{
  public class MPPMensaje
    {
        public MPPMensaje()
        {
            oDatos = new Acceso();
            hdatos = new Hashtable();
        }
        Acceso oDatos;
        Hashtable hdatos;

        public bool Guardar_Mensaje(int IDr,int IDd,string mensaje,int IDviaje)//?
        {
            string consulta = "S_Guardar_Mensaje";
            hdatos = new Hashtable();
            hdatos.Add("@codRemitente",IDr);
            hdatos.Add("@codDestinatario",IDd);
            hdatos.Add("@mensaje",mensaje);
            hdatos.Add("@codViaje",IDviaje);
            return oDatos.Escribir(consulta, hdatos);
        }
        public bool Escribir_Respesta(int IDmensaje,string repuesta)
        {
            string consulta = "S_Responder_Mensaje";
            hdatos = new Hashtable();
            hdatos.Add("@codMensaje",IDmensaje);
            hdatos.Add("@respuesta",repuesta);
            return oDatos.Escribir(consulta, hdatos);
        }
    }
}
