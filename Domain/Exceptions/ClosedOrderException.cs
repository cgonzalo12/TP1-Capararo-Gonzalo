using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class ClosedOrderException : Exception
    {
        public ClosedOrderException() : base("No se puede modificar una orden que ya está en preparación")
        
        {
            
        }
    }
}
