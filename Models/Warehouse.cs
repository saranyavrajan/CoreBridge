using System.ComponentModel.DataAnnotations;

namespace CoreBridge.Models
{
    public class Warehouse
    {
        [Key] public int WarehouseID { get; set; }
        [Required, MaxLength(120)] public string WarehouseName { get; set; } = default!;
        [Required, MaxLength(60)] public string WarehouseRegion { get; set; } = default!;
    }
}
