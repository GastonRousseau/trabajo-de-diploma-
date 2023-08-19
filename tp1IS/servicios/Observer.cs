using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using servicios.ClasesMultiLenguaje;
using Patrones.Singleton.Core;
namespace servicios
{
  public static class Observer
    {
        
        
               
        static IList<IdiomaObserver> Observadores = new List<IdiomaObserver>();  //creo lista de observadores
        public static void agregarObservador(IdiomaObserver Observer)    //se agregan observadores
        {
            Observadores.Add(Observer);
        }

        public static void eliminarObservador(IdiomaObserver Observer)  //se eliminan observadores
        {
            Observadores.Remove(Observer);
        }
        
       

        public static void notificarObeservadores(Idioma Idioma)   //se notifica a los observadores
        {
            foreach (var observer in Observadores)
            {
                observer.CambiarIdioma(Idioma);
            }
        }

        public static void cambiarIdioma(Idioma Idioma)    //Cambio de idioma
        {
            
            
              if (SessionManager.GetInstance != null)
              {
                //_session.Usuario.Idioma = Idioma;
                SessionManager.GetInstance.idioma = Idioma;
                  notificarObeservadores(Idioma);
              }
          
            
        }
    }
}
