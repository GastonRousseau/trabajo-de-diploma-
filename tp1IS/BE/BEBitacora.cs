using abstraccion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class BEBitacora : IBitacora
    {
        public int IdBitacora { get; set; }
        public string Username { get; set; }
        public DateTime Date { get; set; }
        public string Message { get; set; }
        public Nullable<int> Type { get; set; }
    }
}
