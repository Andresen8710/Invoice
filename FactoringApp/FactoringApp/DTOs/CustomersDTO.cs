using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FactoringApp.DTOs
{
    public class CustomersDTO
    {
        public class PostCustomer
        {
            public class RequestCustomers
            {
                public string FirstName { get; set; }
                public string LastName { get; set; }
                public string Identification { get; set; }
                public DateTime Birthdate { get; set; }
                public string Email { get; set; }
                public int? Age { get; set; }
            }
        }
    }
}
