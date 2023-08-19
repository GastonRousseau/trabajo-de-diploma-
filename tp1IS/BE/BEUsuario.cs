using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class BEUsuario : Entidad
    {
        
        public string password { get; set; }
        public string birthDate {get; set;}
        public int rol { get; set;}
        public int active { get; set;}

        public string Direccion { get; set; }
        public string DV { get; set; }
        public IList <Componente> permisos { get; set;}
        public BEUsuario(string usuario_e, string contrasena_e)
        {
            this.user = usuario_e;
            this.password = contrasena_e;
        }
        public BEUsuario(string usuario_e, string contrasena_e, int id, string nacimiento,string adress)
        {
            this.user = usuario_e;
            this.password = contrasena_e;
            this.id= id;
            this.birthDate = nacimiento;
            this.Direccion = adress;
        }
        
        public BEUsuario() { }
    }
}
