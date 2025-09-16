using System.ComponentModel.DataAnnotations;

namespace CoreBridge.Models
{
    public class Products
    {
        [Key] public int ProductID { get; set; }
        [Required, MaxLength(120)] public string ProductName { get; set; } = default!;
        [Required] public string SKU { get; set; } = default!;
    }
}
