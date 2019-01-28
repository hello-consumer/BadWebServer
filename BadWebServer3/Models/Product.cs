using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BadWebServer.Models
{
public class Product
{
    public int? ProductID { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string ProductNumber { get; set; }
    [Required]
    public bool? MakeFlag { get; set; }
    [Required]
    public bool? FinishedGoodsFlag { get; set; }
    public string Color { get; set; }
    [Required]
    [Range(1, double.MaxValue)]
    public short? SafetyStockLevel { get; set; }
    [Required]
    [Range(1, double.MaxValue)]
    public short? ReorderPoint { get; set; }
    [Required]
    [Range(0.01, double.MaxValue)]
    public decimal? StandardCost { get; set; }
    [Required]
    [Range(0.01, double.MaxValue)]
    public decimal? ListPrice { get; set; }
    [Required]
    [Range(1, double.MaxValue)]
    public int? DaysToManufacture { get; set; }
    [Required]
    public DateTime? SellStartDate { get; set; }

}
}
