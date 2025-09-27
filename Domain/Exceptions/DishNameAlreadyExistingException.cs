using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class DishNameAlreadyExistingException :Exception
    {
        public DishNameAlreadyExistingException(string name) :base($"Ya existe un plato con ese nombre")
        {
            
        }
    }
}
