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
    public class InvoicesController : ControllerBase
    {
        private readonly InvoiceBDAppContext _context;

        public InvoicesController(InvoiceBDAppContext context)
        {
            _context = context;
        }

        // GET: api/Invoices
        [HttpGet]
        public async Task<ActionResult<List<Invoices>>> GetInvoices()
        {
            return await _context.Invoices.Include(x => x.InvoiceDetails).ToListAsync();
        }

        // GET: api/Invoices/5
        [HttpGet("{id}")]
        public async Task<ActionResult<List<Invoices>>> GetInvoices(int id)
        {
            var invoices = await _context.Invoices.Where(x => x.Id == id).Include(x => x.InvoiceDetails).ToListAsync();

            if (invoices == null)
            {
                return NotFound();
            }

            return invoices;
        }

        // PUT: api/Invoices/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInvoices(int id, Invoices invoices)
        {
            if (id != invoices.Id)
            {
                return BadRequest();
            }

            var InvExists = await _context.Invoices.AnyAsync(i => i.Id == id);

            try
            {
                var invExists = await _context.Invoices.AnyAsync(X => X.Id == id);

                if (!invExists)
                {
                    return NotFound("Invoice Id Doesn't Exists");
                }
                else
                {
                    _context.Update(invoices);
                    await _context.SaveChangesAsync();
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InvoicesExists(id))
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

        // POST: api/Invoices
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Invoices>> PostInvoices(InvoicesDTO.PostInvoice.RequestInvoice invoices)
        {
            try
            {
                var invoice = new Invoices()
                {
                    CustomerId = invoices.CustomerId,
                    Date = invoices.Date,
                };

                var productsIds = invoices.Details.Select(d => d.ProductId).ToArray();
                var products = _context.Products.Where(x => productsIds.Contains(x.Id)).ToList();

                int rows = 0;
                var invoiceDetails = new List<InvoiceDetails>();

                if (products.Count == 0)
                {
                    return BadRequest($"No se encontraron Productos en InvoiceDetails");
                }
                else
                {
                    foreach (var invDetails in invoices.Details)
                    {
                        var product = products.Where(p => p.Id == invDetails.ProductId).First();

                        if ((product.Stock - invDetails.Quantity) < 0)
                        {
                            return BadRequest($" Stock no valido para el producto: '{invDetails.ProductId}' Stock: {product.Stock}");
                        }

                        var invoiceDetail = new InvoiceDetails()
                        {
                            Price = product.Price,
                            ProductId = invDetails.ProductId,
                            Quantity = invDetails.Quantity,
                            RowNumber = ++rows,
                            TaxValue = product.Price * 0.16m
                        };

                        //ingresan detalles
                        invoiceDetails.Add(invoiceDetail);
                        //se ajusta el stock
                        product.Stock -= invoiceDetail.Quantity;

                    }

                    //cabecera de acuerdo a la suma de destalles
                    invoice.TaxValue = invoiceDetails.Sum(id => id.TaxValue);
                    invoice.Quantity = invoiceDetails.Sum(id => id.Quantity);
                    invoice.SubTotal = invoiceDetails.Sum(id => id.SubTotal);

                    using (var tran = _context.Database.BeginTransaction())
                    {
                        //se agrega la factura
                        _context.Invoices.Add(invoice);
                        await _context.SaveChangesAsync();

                        //Se agregan los detalles
                        foreach (var id in invoiceDetails)
                        {
                            id.InvoiceId = invoice.Id;
                            _context.InvoiceDetails.Add(id);
                        }

                        //actualiza productos
                        foreach (var product in products)
                        {
                            _context.Entry(product).State = EntityState.Modified;
                        }

                        await _context.SaveChangesAsync();
                        tran.Commit();
                    }
                }
                 return Ok(new { InvoiceId = invoice.Id });
                
            }
            catch (Exception ex)
            {
                return BadRequest($"Error; '{ex.InnerException.InnerException.Message}'");

            }
            
        }

        // DELETE: api/Invoices/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvoices(int id)
        {
            //obtenemos la factura
            var invoice = _context.Invoices.Find(id);

            //obtenemos el detalle de las facturas
            var invoicedetails = _context.InvoiceDetails.Where(x => x.InvoiceId == id).ToList();

            //obtenemos los id de los detalles
            var productsIds = invoicedetails.Select(i => i.ProductId).ToArray();

            var products = _context.Products.Where(p => productsIds.Contains(p.Id)).ToList();

            foreach (var inv in invoicedetails)
            {

                var product = products.Where(pd => pd.Id == inv.ProductId).First();

                product.Stock += inv.Quantity;
                _context.Entry(product).State = EntityState.Modified;

            }

            _context.Entry(invoice).State = EntityState.Deleted;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InvoicesExists(int id)
        {
            return _context.Invoices.Any(e => e.Id == id);
        }
    }
}
