using CoreBridge.Data;
using CoreBridge.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoreBridge.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WarehouseController : Controller
    {
        private readonly AppDbContext _context;

        public WarehouseController(AppDbContext context)
        {
            _context = context;
        }

        //To add a new entry in Warehouse Table
        [HttpPost]
        public async Task<ActionResult<Warehouse>> PostWarehouse(Warehouse warehouse)
        {
            _context.Warehouse.Add(warehouse);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWarehouse", new { id = warehouse.WarehouseID }, warehouse);
        }

        //Matching GET operation for retreiving newly added data with POST
        [HttpGet("{id}")]
        public async Task<ActionResult<Warehouse>> GetWarehouse(int warehouseid)
        {
            var warehouse = await _context.Warehouse.FindAsync(warehouseid);
            if (warehouse == null) return NotFound();
            return warehouse;

            //[HttpGet("{id}")]
            //public async Task<ActionResult<Warehouse>> GetWarehouse(int id)
            //{
            //    var warehouse = await _context.Warehouse.FindAsync(id);
            //    if (warehouse == null) return NotFound();
            //    return warehouse;
            //}

        }
    }
}
