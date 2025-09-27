using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class ExistingCategoryException :Exception
    {
        public ExistingCategoryException(int category): base("La categoría debe existir en el sistema")
        {
            
        }
    }
}
