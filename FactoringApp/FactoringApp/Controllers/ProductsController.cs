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
    public class ProductsController : ControllerBase
    {
        private readonly InvoiceBDAppContext _context;

        public ProductsController(InvoiceBDAppContext context)
        {
            _context = context;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<List<Products>>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<List<Products>>> GetProducts(int id)
        {
            var products = await _context.Products.Where(x => x.Id == id).ToListAsync();

            if (products == null)
            {
                return NotFound();
            }

            return products;
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProducts(int id, Products products)
        {
            if (id != products.Id)
            {
                return BadRequest();
            }

            try
            {
                var product = _context.Products.Where(x => x.Id == id).FirstOrDefault();

                if (product == null)
                {
                    return BadRequest($"ProductId: '{product.Id}' Not Found ");
                }
                else
                {
                    product.Id = id;
                    product.Name = products.Name != null ? products.Name : product.Name;
                    product.Description = products.Description != null ? products.Description : product.Description;
                    product.Price = products.Price != 0 ? products.Price : product.Price;
                    product.Stock = products.Stock != 0 ? products.Stock : product.Stock;

                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductsExists(id))
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

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Products>> PostProducts(ProductsDTO.PostProducts.RequestProducts products)
        {
            var product = new Products()
            {
                Name = products.Name,
                Description = products.Description,
                Price = products.Price,
                Stock = products.Stock
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProducts", new { id = product.Id }, products);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProducts(int id)
        {
            var products = await _context.Products.AnyAsync(x => x.Id == id);
            if (products == null)
            {
                return NotFound();
            }

            _context.Products.Remove(new Products() { Id = id });
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductsExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
