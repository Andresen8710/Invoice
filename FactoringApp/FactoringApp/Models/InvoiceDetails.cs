using System;
using System.Collections.Generic;

#nullable disable

namespace FactoringApp.Models
{
    public partial class InvoiceDetails
    {
        public int InvoiceId { get; set; }
        public int RowNumber { get; set; }
        public int ProductId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal TaxValue { get; set; }
        public decimal? SubTotal { get; set; }
        public decimal? Total { get; set; }

        public virtual Invoices Invoice { get; set; }
        public virtual Products Product { get; set; }
    }
}
