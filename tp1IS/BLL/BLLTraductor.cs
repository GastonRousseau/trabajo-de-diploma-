using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPP;
using Negocio;
using servicios.ClasesMultiLenguaje;
using System.Collections;
using System.Data;
namespace BLL
{
   public class BLLTraductor
    {

        MPP.MPPtraductor omppT = new MPP.MPPtraductor();
        BLLBitacora oBit = new BLLBitacora();
        public BLLTraductor()
        {
            try
            {
                OMPPtraductor = new MPPtraductor();
            }
            catch (NullReferenceException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                oBit.guardar_accion(ex.Message, 1);
                throw ex;
            }
        }
        MPPtraductor OMPPtraductor;
       public  Idioma ObtenerIdiomaBase()
        {
            try
            {
                return OMPPtraductor.ObtenerIdiomaBase();
            }
            catch (NullReferenceException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {

                oBit.guardar_accion(ex.Message, 1);
                throw ex;

            }
           
        }

        public bool ActualizarTraduccion(int IDidioma,int IDpalabra,string traduccion)
        {
            try
            {
                return OMPPtraductor.ActualizarTraduccion(IDidioma,IDpalabra,traduccion);

            }catch(NullReferenceException ex)
            {
                throw ex;
            }catch(Exception ex)
            {
                oBit.guardar_accion(ex.Message, 1);
                throw ex;
            }
        }

        public List<Palabra> obtenerPalabrasSinTraducir(int idioma)
        {
            try
            {
                return OMPPtraductor.obtenerPalabrasSinTraducir(idioma);
            }
            catch (NullReferenceException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                oBit.guardar_accion(ex.Message, 1);
                throw ex;
            }

        }
            public DataTable traerTablaxIdioma(int id)
            {

            try
            {
                return OMPPtraductor.traerTablaxIdioma(id);
            }
            catch (NullReferenceException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                oBit.guardar_accion(ex.Message, 1);
                throw ex;

            }
            
        }
        public List<Idioma> ObtenerIdiomas()
        {
            try
            {
                return OMPPtraductor.ObtenerIdiomas();
            }
            catch (NullReferenceException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                oBit.guardar_accion(ex.Message, 1);
                throw ex;
            }
        }
        public Dictionary<string,Traduccion> obtenertraducciones(Idioma Idioma)
        {
            try
            {
                return OMPPtraductor.obtenertraducciones(Idioma);
            }
            catch (NullReferenceException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                oBit.guardar_accion(ex.Message, 1);
                throw ex;
            }
        }

        public List<string> obtenerIdiomaOriginal()
        {
            try
            {
                return OMPPtraductor.obtenerIdiomaOriginal();
            }
            catch (NullReferenceException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                oBit.guardar_accion(ex.Message, 1);
                throw ex;
            }
        }

        public List<Palabra> obtenerPalabras()
        {
            try
            {
                return OMPPtraductor.obtenerPalabras();
            }
            catch (NullReferenceException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                oBit.guardar_accion(ex.Message, 1);
                throw ex;
            }
        }
        public Idioma TraerIdioma(string idioma)
        {
            try
            {
                return OMPPtraductor.TrarIdioma(idioma);
            }
            catch (NullReferenceException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                oBit.guardar_accion(ex.Message, 1);
                throw ex;
            }
        }

        public bool CrearIdioma(Idioma Oidioma)
        {
            try
            {
                return OMPPtraductor.CrearIdioma(Oidioma);
            }
            catch (NullReferenceException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                oBit.guardar_accion(ex.Message, 1);
                throw ex;
            }
        }

        public bool IdiomaExistente(string idioma)
        {
            try
            {
                return OMPPtraductor.idiomaExistente(idioma);
            }
            catch (NullReferenceException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                oBit.guardar_accion(ex.Message, 1);
                throw ex;
            }
        }

        public bool TraduccionExistente(int id_idioma,int id_palabra)
        {
            try
            {
                return OMPPtraductor.TraduccionExistente(id_idioma, id_palabra);
            }
            catch (NullReferenceException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                oBit.guardar_accion(ex.Message, 1);
                throw ex;
            }
        }

        public Palabra TraerPalbra(string palabra)
        {
            try
            {
                return OMPPtraductor.TraerPalbra(palabra);
            }
            catch (NullReferenceException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                oBit.guardar_accion(ex.Message, 1);
                throw ex;
            }
        }

        public bool CrearTraduccion(int ID_idioma,Traduccion Otraduccion)
        {
            try
            {
                return OMPPtraductor.CrearTraduccion(ID_idioma, Otraduccion);
            }
            catch (NullReferenceException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                oBit.guardar_accion(ex.Message, 1);
                throw ex;
            }
        }
    }
}
