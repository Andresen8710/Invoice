using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FactoringApp.Models;
using FactoringApp.DTOs;

namespace FactoringApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly InvoiceBDAppContext _context;

        public CustomersController(InvoiceBDAppContext context)
        {
            _context = context;
        }

        // GET: api/Customers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customers>>> GetCustomers()
        {
            return await _context.Customers.ToListAsync();
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<List<Customers>>> GetCustomers(int id)
        {
            var customers = await _context.Customers.Where(x => x.Id == id).ToListAsync();

            if (customers == null)
            {
                return NotFound();
            }

            return customers;
        }

        // PUT: api/Customers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomers(int id, Customers customers)
        {
            if (id != customers.Id)
            {
                return BadRequest();
            }

            try
            {
                var customer = _context.Customers.Where(x => x.Id == id).FirstOrDefault();

                //_context.Entry(customers).State = EntityState.Modified;

                if (customer == null)
                {
                    return BadRequest($"CustomerId: '{customers.Id}' Not Found ");
                }
                else
                {
                    customer.Id = id;
                    customer.FirstName = customers.FirstName != null ? customers.FirstName : customer.FirstName;
                    customer.LastName = customers.LastName != null ? customers.LastName : customer.LastName;
                    customer.Identification = customers.Identification != null ? customers.Identification : customer.Identification;
                    customer.Birthdate = customers.Birthdate != null ? customers.Birthdate : customer.Birthdate;
                    customer.Email = customers.Email != null ? customers.Email : customer.Email;
                    customer.Age = customers.Age != null ? customers.Age : customer.Age;

                    _context.Update(customer);
                    await _context.SaveChangesAsync();

                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomersExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        // POST: api/Customers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> PostCustomers(CustomersDTO.PostCustomer.RequestCustomers customers)
        {
            var customer = new Customers()
            {
                FirstName = customers.FirstName,
                LastName = customers.LastName,
                Identification = customers.Identification,
                Birthdate = customers.Birthdate,
                Email = customers.Email,
                Age = customers.Age
            };

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCustomers", new { id = customer.Id }, customers);
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCustomers(int id)
        {
            var customers = await _context.Customers.AnyAsync(x => x.Id == id);
            if (customers == null)
            {
                return NotFound();
            }

            _context.Remove(new Customers() { Id = id });
            await _context.SaveChangesAsync();

            return NoContent();
            //return CreatedAtAction("Delete Success",customers);
        }

        private bool CustomersExists(int id)
        {
            return _context.Customers.Any(e => e.Id == id);
        }
    }
}
