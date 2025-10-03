using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class InvalidStatusException :Exception
    {
        public InvalidStatusException() : base ("El estado especificado no es valido")
        {
            
        }
    }
}
