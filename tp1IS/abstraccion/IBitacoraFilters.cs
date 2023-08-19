using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abstraccion
{
    public interface IBitacoraFilters
    {
        string Username { get; set; }
        Nullable<DateTime> From { get; set; }
        Nullable<DateTime> To { get; set; }
        Nullable<int> Type { get; set; }
        string Like { get; set; }
    }
}
