using System;
using System.Collections.Generic;

#nullable disable

namespace FactoringApp.Models
{
    public partial class Products
    {
        public Products()
        {
            InvoiceDetails = new HashSet<InvoiceDetails>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }

        public virtual ICollection<InvoiceDetails> InvoiceDetails { get; set; }
    }
}
