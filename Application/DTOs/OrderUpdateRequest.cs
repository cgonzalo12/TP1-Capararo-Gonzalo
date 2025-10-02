using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class OrderUpdateRequest
    {
        public IEnumerable<Items>? Items { get; set; }
    }
}
