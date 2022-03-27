using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FactoringApp.DTOs
{
    public class InvoicesDTO
    {
        public class PostInvoice
        {
            public class RequestInvoice
            {
                public DateTime Date { get; set; }
                public int CustomerId { get; set; }
                public List<DetailRequest> Details { get; set; }
                public class DetailRequest
                {
                    public int ProductId { get; set; }
                    public int Quantity { get; set; }

                }
            }

        }
    }
}
