using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class ActiveDishException :Exception
    {
        public ActiveDishException() : base("El plato especificado no existe o no está disponible")
        {
            
        }
    }
}
