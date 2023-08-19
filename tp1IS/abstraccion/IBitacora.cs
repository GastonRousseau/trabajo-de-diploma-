using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abstraccion
{
    public interface IBitacora
    {
            int IdBitacora { get; set; }
            string Username { get; set; }
            DateTime Date { get; set; }
            string Message { get; set; }
        Nullable<int> Type { get; set; }
    }
}
