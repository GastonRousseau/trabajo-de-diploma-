using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using MPP;
using Negocio;

namespace BLL
{
    public class BLLComposite
    {
        MPPComposite mPPComposite = new MPPComposite();
        BLLBitacora oBit = new BLLBitacora();
        public bool escribir_relacion(int hijo, int padre)
        {
            try
            {
                return mPPComposite.escribir_relacion(hijo, padre);

            }
            catch (Exception ex) {
                oBit.guardar_accion(ex.Message, 1);
                throw ex; 
            }
        }
        public bool escribir(Componente componente)
        {
            try
            {
                return mPPComposite.escribir(componente);

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
        public IList<Componente> GetAll(int familia)
        {
            try
            {
                return mPPComposite.GetAll(familia);

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
        public bool hacer_patente(Componente componente, bool patente)
        {
            try
            {
                return mPPComposite.hacer_patente(componente, patente);

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
        public IList<Componente> GetFamilias()
        {
            try
            {
                return mPPComposite.GetFamilias();

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
        public IList<Componente> GetPermisos()
        {
            try
            {
                return mPPComposite.GetPermisos();

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
        public IList<Componente> GetPermisosdeUser(int id)
        {
            try
            {
                IList<Componente> permisos = mPPComposite.get_permisos_usuario(id);
                IList<Componente> hijos = null;
                foreach (Componente p in permisos)
                {
                    hijos = GetAll(p.Id);
                    if (hijos != null)
                    {
                        foreach (Componente o in hijos)
                        {
                            p.AgregarHijo(o);
                        }
                    }
                }
                return permisos;

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
        public List<int> get_permisos(int rol)
        {
            try
            {
                return mPPComposite.get_permisos(rol);

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
        public int buscar_id(string nombre)
        {
            try
            {
                return mPPComposite.buscar_id(nombre);

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
        public int escribir_retorno_id(string nombre)
        {
            try
            {
                return mPPComposite.escribir_retorno_id(nombre);

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
        public bool es_patente(string nombre)
        {
            try
            {
                return mPPComposite.es_patente(nombre);

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
        public bool borrar(int id)
        {
            try
            {
                return mPPComposite.borrar(id);

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
        public bool buscar_rol_usado(int rol)
        {
            try
            {
                return mPPComposite.buscar_rol_usado(rol);

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

        public bool contiene(Componente padre, Componente Hijo)
        {
            try
            {
                foreach (Componente comp in padre.Hijos)
                {
                    if (comp.Nombre == Hijo.Nombre) return true;
                    else if (comp.Hijos != null) {

                        if (contiene(comp, Hijo)) return true; 
                    
                    }
                    
                }
                return false;

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
        public Componente llenar_padre(Componente padre)
        {
            try
            {
                IList<Componente> HijosFamilia = GetAll(padre.Id);


                foreach (var item in HijosFamilia)
                {
                    padre.AgregarHijo(item);
                }

                return padre;

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
        public bool evitar_loop(Componente padre, Componente hijo)
        {
            try
            {
                padre = llenar_padre(padre);
                hijo = llenar_padre(hijo);
                if (padre.Nombre == hijo.Nombre) return true;
                else return (padre.Hijos.Contains(hijo) || contiene(hijo, padre));

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
