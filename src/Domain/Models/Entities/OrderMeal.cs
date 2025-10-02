using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Entities
{
    public class OrderMeal
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public MealType MealType { get; set; }
        // Breakfast,Dinner
        public int? MealId { get; set; }
        public Meal? Meal { get; set; }
        //  (Lunch Protein | Lunch Carb)
        public int? ProteinMealId { get; set; }
        public Meal? ProteinMeal { get; set; }

        public int? CarbMealId { get; set; }
        public Meal? CarbMeal { get; set; }

        public string? Notes { get; set; }
    }
}
