using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abstraccion
{
   public interface IdateFilters
    {
        Nullable<DateTime> From { get; set; }
        Nullable<DateTime> To { get; set; }
    }
}
