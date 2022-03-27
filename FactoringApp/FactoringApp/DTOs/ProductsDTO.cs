using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FactoringApp.DTOs
{
    public class ProductsDTO
    {
        public class PostProducts
        {
            public class RequestProducts
            {
                public string Name { get; set; }
                public string Description { get; set; }
                public decimal Price { get; set; }
                public int Stock { get; set; }
            }

        }
    }
}
