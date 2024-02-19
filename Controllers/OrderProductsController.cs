using Jugueton.Data;
using Jugueton.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Jugueton.Controllers
{
    [Route("api/[controller]")]
    [ApiController]


    public class OrderProductsController : ControllerBase
    {
        private readonly ToyStoreContext _context;

        public OrderProductsController(ToyStoreContext context)
        {
            _context = context;
        }

        // GET: api/orderproducts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderProduct>>> GetOrderProducts()
        {
            return await _context.OrderProducts.ToListAsync();
        }

        // GET: api/orderproducts/1
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderProduct>> GetOrderProduct(int id)
        {
            var orderProduct = await _context.OrderProducts.FindAsync(id);

            if (orderProduct == null)
            {
                return NotFound();
            }

            return orderProduct;
        }

        // POST: api/orderproducts
        [HttpPost]
        public async Task<ActionResult<OrderProduct>> CreateOrderProduct(OrderProduct orderProduct)
        {
            _context.OrderProducts.Add(orderProduct);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetOrderProduct), new { id = orderProduct.OrderId }, orderProduct);
        }

        // PUT: api/orderproducts/1
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrderProduct(int id, OrderProduct orderProduct)
        {
            if (id != orderProduct.OrderId)
            {
                return BadRequest();
            }

            _context.Entry(orderProduct).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderProductExists(id))
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

        // DELETE: api/orderproducts/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderProduct(int id)
        {
            var orderProduct = await _context.OrderProducts.FindAsync(id);
            if (orderProduct == null)
            {
                return NotFound();
            }

            _context.OrderProducts.Remove(orderProduct);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderProductExists(int id)
        {
            return _context.OrderProducts.Any(e => e.OrderId == id);
        }
    }
}