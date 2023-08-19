using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abstraccion;

namespace BE
{
    public class Entidad : IEntidad
    {
        public int id { get; set; }
        public string user { get; set; }
    }
}

