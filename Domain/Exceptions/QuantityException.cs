using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class QuantityException :Exception
    {
        public QuantityException() : base("La cantidad debe ser mayor a 0")
        {
            
        }
    }
}
