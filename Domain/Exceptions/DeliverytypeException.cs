using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class DeliverytypeException : Exception
    {
        public DeliverytypeException():base ("Debe especificar un tipo de entrega válido")
        
        {
            
        }
    }
}
