using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class DateRangeException : Exception
    {
        public DateRangeException() : base("Rango de fechas inválido")
        {
            
        }
    }
}
