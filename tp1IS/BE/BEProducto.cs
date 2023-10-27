using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
   public class BEProducto
    {
        
        public int id { get; set; }

        public string nombre { get; set; }
        public int CantPallets { get; set; }
        public BEUsuario cliente { get; set; }
        
        public string Estado { get; set; }
    }
}
