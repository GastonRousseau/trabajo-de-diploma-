using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
   public class BECamion
    {
        public int id { get; set; }
        public string patente { get; set; }

        public string tipo { get; set; }

        public int capacidad_Pallets { get; set; }

        public BEUsuario conductor { get; set; }
    }
}
