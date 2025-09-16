using System.ComponentModel.DataAnnotations;

namespace CoreBridge.Models

{
    public class Stock
    {
        [Key] public int StockID { get; set; }
        [Required] public String SKU { get; set; } = default!;
        public int Available { get; set; }
    }
}
