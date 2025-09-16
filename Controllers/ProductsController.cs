using CoreBridge.Data;
using CoreBridge.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace CoreBridge.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        private readonly AppDbContext _context;

        public ProductsController(AppDbContext context)
        {
            _context = context;
        }

        // Get List of all products the company has.
        [HttpGet]
        public async Task<ActionResult<List<Products>>> GetProducts()
        {
            var Products = await _context.Products.ToListAsync();
            return Products;
        }

        // List top 5 product names
        [HttpGet("topnames")]
        public async Task<ActionResult<IEnumerable<String>>> GetnProducts()
        {
            var Products = await _context.Products
                .Take(5)
                .Select(p => p.ProductName)
                .ToListAsync();
            return Products;
        }

        // Get Product Name from Product Id
        [HttpGet("{id:int}/name")]
        public async Task<ActionResult<String>> GetProductName(int id)
        {
            if (id <= 0) return BadRequest("Invalid id.");

            // USING QUERY SYNTAX
            //var ProductName = from p in _context.Products
            //                  where p.ProductID == id
            //                  select p.ProductName; 

            //USING METHOD SYNTAX
            var ProductName = await _context.Products
                                               .AsNoTracking()
                                               .Where(p => p.ProductID == id)
                                               .Select(p => p.ProductName)
                                               .SingleOrDefaultAsync();

            return ProductName is null ? NotFound() : Ok(ProductName);
        }

        //Get Product Availability from Stock Table with Product Id
        [HttpGet("{id:int}/availability")]
        public async Task<ActionResult<IEnumerable<String>>> GetProductAvailability(int id)
        {
            if (id <= 0) return BadRequest("Invalid Id.");

            var row = await _context.Products.AsNoTracking()
                                       .Where(p => p.ProductID == id)
                                       .GroupJoin(_context.Stock.AsNoTracking(),   
                                           p => p.SKU,                       
                                           s => s.SKU,                       
                                           (p, stock) => new                
                                           {
                                               TotalAvailable = stock.Sum(x => (int?)x.Available) ?? 0
                                           })
                                       .SingleOrDefaultAsync();

            return row is null ? NotFound() : Ok(row.TotalAvailable);
        }
    }
}
