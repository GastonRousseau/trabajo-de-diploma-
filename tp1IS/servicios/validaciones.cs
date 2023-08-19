using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace servicios
{
    public class validaciones
    {
        public bool contrasena(string contra)
        {
            try
            {
                return Regex.IsMatch(contra, "^([a-zA-Z]{5,15})([1-9]{1,10}$)");

            }
            catch (Exception ex) { throw ex; }
        }
        public bool usuario(string usuario)
        {
            try
            {
                return Regex.IsMatch(usuario, "^([a-zA-Z]{1,25}$)");
            }
            catch (Exception ex) { throw ex; }
        }
        public bool traduccion(string Otraduccion)
        {
            try
            {
                return Regex.IsMatch(Otraduccion, "^[a-zA-Z\\s]{1,200}$");
            }
            catch (Exception ex) { throw ex; }
        }

        public bool idioma(string oidioma)
        {
            try
            {
                return Regex.IsMatch(oidioma, @"^[A-Z][a-z]{1,25}$");
            }
            catch(Exception ex) { throw ex; }
        }

        public bool calle(string calle)
        {
            try
            {
                return Regex.IsMatch(calle, "^(?!^\\s+$)[A-Za-z\\s]{2,40}$");

            }
            catch (Exception ex) { throw ex; }
        }
        public bool id (string id)
        {

            try
            {
                return Regex.IsMatch(id, "^([0-9]{1,9}$)");
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
