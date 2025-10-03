using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class InvalidTransactionException :Exception
    {
        public InvalidTransactionException(string statusRequest,string statusBD):base($"No se puede cambiar de {statusRequest} a {statusBD}")
        {
            
        }
    }
}
