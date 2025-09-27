using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class InvalidPriceException :Exception
    {
        public InvalidPriceException(decimal? price) :base($"Precio del plato debe ser mayor a cero.")
        {
            
        }
    }
}
