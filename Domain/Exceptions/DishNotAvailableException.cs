﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class DishNotAvailableException : Exception

    {
        public DishNotAvailableException() : base("El plato especificado no está disponible")
        {
            
        }
    }
}
