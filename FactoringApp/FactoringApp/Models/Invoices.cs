using System;
using System.Collections.Generic;

#nullable disable

namespace FactoringApp.Models
{
    public partial class Invoices
    {
        public Invoices()
        {
            InvoiceDetails = new HashSet<InvoiceDetails>();
        }

        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int CustomerId { get; set; }
        public int? Quantity { get; set; }
        public decimal? TaxValue { get; set; }
        public decimal? SubTotal { get; set; }
        public decimal? Total { get; set; }

        public virtual Customers Customer { get; set; }
        public virtual ICollection<InvoiceDetails> InvoiceDetails { get; set; }
    }
}
