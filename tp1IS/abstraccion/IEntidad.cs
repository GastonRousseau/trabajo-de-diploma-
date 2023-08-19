using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstraccion
{
    public interface IEntidad
    {
        int id { get; set; }
        string user { get; set; }
    }
}
