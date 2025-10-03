using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class ItemNotFundInTheOrderException : Exception
    {
        public ItemNotFundInTheOrderException():base("Item no encontrado en la orden")
        {
            
        }
    }
}
