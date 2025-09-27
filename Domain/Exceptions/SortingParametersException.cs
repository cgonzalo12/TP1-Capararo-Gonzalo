using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class SortingParametersException :Exception
    {
        public SortingParametersException(string? sortByPrice) : base ("parametros de ordenamiento invalidos")
        {
            
        }
    }
}
