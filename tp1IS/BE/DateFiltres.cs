using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using abstraccion;
namespace BE
{
    public class DateFiltres : IdateFilters
    {
        public Nullable<DateTime> From { get; set; }
        public Nullable<DateTime> To { get; set; }
    }
}
