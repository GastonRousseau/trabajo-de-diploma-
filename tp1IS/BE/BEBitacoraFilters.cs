using abstraccion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class BEBitacoraFilters : IBitacoraFilters
    {
        public string Username { get; set; }
        public Nullable<DateTime> From { get; set; }
        public Nullable<DateTime> To { get; set; }
        public Nullable<int> Type { get; set; }
        public string Like { get; set; }
    }
}
