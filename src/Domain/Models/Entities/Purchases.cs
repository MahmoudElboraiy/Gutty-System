using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Entities
{
    public class Purchases
    {
        public int Id { get; set; }
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public UnitType Unit { get; set; }
        public decimal Price { get; set; }
        public DateOnly PurchaseDate { get; set; }
    }
}
