

using Domain.Enums;
using Domain.Models.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models.Entities;

public class Sales
{
    public int Id { get; set; }
    public  SaleType ItemType { get; set; }
    public  string ItemName { get; set; } 
    public  decimal Quantity { get; set; }
    public UnitType UnitType { get; set; }
    public decimal Price { get; set; } 
    public string? CustomerId { get; set; }

    [ForeignKey("CustomerId")]
    public User? Customer { get; set; }
    public DateOnly SaleDate { get; set; }

}
