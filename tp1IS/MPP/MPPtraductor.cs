using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using System.Collections;
using System.Data;
using servicios.ClasesMultiLenguaje;
using System.Data.SqlClient;
namespace MPP
{
   public class MPPtraductor
    {
        public MPPtraductor()
        {
            Datos = new Acceso();
        }
        Acceso Datos;
        Hashtable Hdatos;
        
        public List<Idioma> ObtenerIdiomas()
        {
            List<Idioma> ListaIdiomas = new List<Idioma>();
            try
            {
                DataTable DT = new DataTable();
                string consulta="S_Listar_idioma";
                DT= Datos.Leer(consulta,null);
                
                foreach(DataRow fila in DT.Rows)
                {
                    Idioma OIdioma = new Idioma();
                    OIdioma.ID = Convert.ToInt32(fila["ID"].ToString());
                    OIdioma.Nombre = fila["Idioma"].ToString();
                     OIdioma.Default = bool.Parse(fila["predeterminado"].ToString());
                    ListaIdiomas.Add(OIdioma);
                }

            }
            catch (NullReferenceException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ListaIdiomas;
        }

        public Idioma ObtenerIdiomaBase()
        {

            string consulta = "S_listar_idioma";
            DataTable DT = new DataTable();
            DT = Datos.Leer(consulta, null);
            Idioma Oidioma = new Idioma();
            foreach(DataRow fila in DT.Rows)
            {
                if (Convert.ToBoolean(fila["predeterminado"]) == true)
                {
                    Oidioma.ID = Convert.ToInt32(fila["id"]);
                    Oidioma.Nombre = fila["idioma"].ToString();
                    Oidioma.Default = Convert.ToBoolean(fila["predeterminado"]);
                }
            }
            return Oidioma;
        }
        public Dictionary<string, Traduccion> obtenertraducciones(Idioma Idioma)
        {
            if (Idioma == null)
            {
                Idioma=ObtenerIdiomaBase();
            }

            Dictionary<string, Traduccion> Traducciones = new Dictionary<string, Traduccion>();

            try
            {
                string consulta="S_Traer_Traducciones";
                Hashtable Hdatos = new Hashtable();
                 Hdatos.Add("@ID", Idioma.ID);
                DataTable DT = new DataTable();
                DT = Datos.Leer(consulta, Hdatos);
                foreach(DataRow fila in DT.Rows)
                {
                    var Palabra = fila["Palabra"].ToString();
                    Traduccion OTraduccion = new Traduccion();
                    OTraduccion.texto = fila["traduccion"].ToString();

                    Palabra OPalabra = new Palabra();
                    OPalabra.ID = Convert.ToInt32(fila["ID"].ToString());
                    OPalabra.Nombre = Palabra;
                    Traducciones.Add(Palabra, OTraduccion);
                }

                return Traducciones;
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

        public List<string> obtenerIdiomaOriginal()
        {
            List<string> Palabras = new List<string>();
            try
            {
                string consulta = "S_Traer_Palabras";
                Hashtable Hdatos = new Hashtable();
                DataTable DT = new DataTable();
                DT = Datos.Leer(consulta, null);

                foreach(DataRow fila in DT.Rows)
                {
                    string Palabraa = fila["palabra"].ToString();
                    Palabra Opalabra = new Palabra();
                    Opalabra.ID = Convert.ToInt32(fila["id"].ToString());
                    Opalabra.Nombre = Palabraa;

                    Palabras.Add(Palabraa);
                }
                return Palabras;
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

        public DataTable traerTablaxIdioma(int id)
        {
            try
            {
                string consulta = "S_Traer_Traducciones";
                Hashtable Hdatos = new Hashtable();
                Hdatos.Add("@ID", id);
                DataTable DT = new DataTable();
                DT = Datos.Leer(consulta, Hdatos);
                foreach (DataRow fila in DT.Rows)
                {
                    var Palabra = fila["Palabra"].ToString();
                    Traduccion OTraduccion = new Traduccion();
                    OTraduccion.texto = fila["traduccion"].ToString();

                    Palabra OPalabra = new Palabra();
                    OPalabra.ID = Convert.ToInt32(fila["ID"].ToString());
                    OPalabra.Nombre = Palabra;
               
                }

                return DT;
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
        public Idioma TrarIdioma(string Oidioma)
        {
            Idioma IdiomaSelec = new Idioma();

            try
            {
                string consulta = "S_Traer_unIdioma";
                Hashtable Hdatos = new Hashtable();
                Hdatos.Add("@Idioma", Oidioma);
                DataTable tabla = new DataTable();
                tabla = Datos.Leer(consulta, Hdatos);

                foreach(DataRow fila in tabla.Rows)
                {
                    IdiomaSelec.ID = Convert.ToInt32(fila["ID"].ToString());
                    IdiomaSelec.Nombre = fila["idioma"].ToString();
                    IdiomaSelec.Default = Convert.ToBoolean(fila["predeterminado"]);
                }

                return IdiomaSelec;
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

        public bool CrearIdioma(Idioma Oidioma)
        {
            try
            {
                Hashtable hdatos = new Hashtable();
                string consulta = "S_Crear_Idioma";
                hdatos.Add("idioma", Oidioma.Nombre);
                hdatos.Add("predeterminado", Oidioma.Default);
                Datos = new Acceso();
                return Datos.Escribir(consulta, hdatos);
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

        public bool idiomaExistente(string idioma)
        {
            string consulta = "S_Listar_Idioma";

            DataTable DT = new DataTable();

            DT = Datos.Leer(consulta, null);
            foreach (DataRow fila in DT.Rows)
            {
                if (idioma == fila["idioma"].ToString()) return true;
               
            }

            return false;

        }

        public Palabra TraerPalbra(string palabra)
        {
            Palabra Opalabra = new Palabra();
            try {  
            string consulta = "S_Traer_UnaPalabra";
            DataTable DT = new DataTable();
            Hashtable Hdatos = new Hashtable();
            Hdatos.Add("@palabra", palabra);
            DT = Datos.Leer(consulta,Hdatos);
            foreach(DataRow fila in DT.Rows)
            {
                if(palabra == fila["palabra"].ToString())
                {
                    Opalabra.ID = Convert.ToInt32(fila["ID"]);
                    Opalabra.Nombre = palabra;
                    return Opalabra;
                }
                else
                {
                    return null;
                }
            }
            }
            catch (NullReferenceException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Opalabra;
        }
        public bool TraduccionExistente(int id_Idioma,int id_Palabra)
        {
            try { 
            string consulta = "S_Listar_TraduccionesxIdioma";
            Hashtable Hdatos = new Hashtable();
            Hdatos.Add("@id",id_Idioma);
            DataTable DT = new DataTable();
            DT = Datos.Leer(consulta, Hdatos);
            foreach (DataRow fila in DT.Rows)
            {
                if (id_Palabra == Convert.ToInt32(fila["IDpalabra"])) return true;
            }
        }
            catch (NullReferenceException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }

return false;
        }

        public bool CrearTraduccion(int ID_idioma,Traduccion Otraduccion)
        {

            try
            {
                string consulta = "S_altaTraduccion";
                Hdatos = new Hashtable();
                Hdatos.Add("@IDidioma", ID_idioma);
                Hdatos.Add("@IDpalabra", Otraduccion.Palabra.ID);
                Hdatos.Add("@traduccion", Otraduccion.texto);
                Datos = new Acceso();
                return Datos.Escribir(consulta, Hdatos);
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

        public List<Palabra> obtenerPalabrasSinTraducir(int idioma)
        {
            try
            {
                List<Palabra> Palabras = new List<Palabra>();
                string consulta = "ObtenerPalabrasSinTraduccion";

                DataTable DT = new DataTable();
                Hashtable Hdatos = new Hashtable();
                Hdatos.Add("@idIdioma", idioma);
                DT = Datos.Leer(consulta, Hdatos);

                if (DT.Rows.Count == 0)
                {
                    return null;
                }
                else
                {
                    foreach (DataRow fila in DT.Rows)
                    {
                        Palabra Opalabra = new Palabra();
                        Opalabra.Nombre = fila["palabra"].ToString();
                        Opalabra.ID = Convert.ToInt32(fila["id"].ToString());
                        Palabras.Add(Opalabra);

                    }
                    return Palabras;
                }

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


        public List<Palabra> obtenerPalabras()
        {
            try
            {
                List<Palabra> Palabras = new List<Palabra>();
                string consulta = "S_Traer_Palabras";
                
                DataTable DT = new DataTable();
              
                DT = Datos.Leer(consulta, null);
                foreach (DataRow fila in DT.Rows)
                {
                    Palabra Opalabra = new Palabra();
                    Opalabra.Nombre = fila["palabra"].ToString();
                    Opalabra.ID = Convert.ToInt32(fila["id"].ToString());
                    Palabras.Add(Opalabra);
                   
                }
                return Palabras;
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


        public bool ActualizarTraduccion(int IDidioma, int IDpalabra, string Otraduccion)
        {
            try
            {
                string consulta = "S_actz_Traduccion";
                Hdatos = new Hashtable();
                Hdatos.Add("@IDidioma", IDidioma);
                Hdatos.Add("@IDpalabra", IDpalabra);
                Hdatos.Add("@traduccion", Otraduccion);
                Datos = new Acceso();
              
                return Datos.Escribir(consulta, Hdatos);


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
