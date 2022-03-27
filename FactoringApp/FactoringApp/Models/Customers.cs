using System;
using System.Collections.Generic;

#nullable disable

namespace FactoringApp.Models
{
    public partial class Customers
    {
        public Customers()
        {
            Invoices = new HashSet<Invoices>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Identification { get; set; }
        public DateTime Birthdate { get; set; }
        public string Email { get; set; }
        public int? Age { get; set; }

        public virtual ICollection<Invoices> Invoices { get; set; }
    }
}
