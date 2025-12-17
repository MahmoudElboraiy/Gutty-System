using System;
using System.Collections.Generic;

namespace Presentation.Dtos
{
    public class PlaceSubscriptionRequest
    {
        public Guid PlanId { get; set; }
        public uint DaysLeft { get; set; }
        public uint LunchMealsLeft { get; set; }
        public uint CarbGrams { get; set; }
        public DateTime StartDate { get; set; }
        public bool IsCurrent { get; set; }
        public bool IsPaused { get; set; }
        public List<PlaceSubscriptionPlanCategoryDto> LunchCategories { get; set; } = new();
        public string? PromoCode { get; set; }
    }

    public class PlaceSubscriptionPlanCategoryDto
    {
        public int? SubCategoryId { get; set; }
        public uint NumberOfMeals { get; set; }
        public uint NumberOfMealsLeft { get; set; }
        public uint ProteinGrams { get; set; }
        public decimal PricePerGram { get; set; }
        public bool AllowProteinChange { get; set; }
        public uint MaxProteinGrams { get; set; }
        public uint MaxMeals { get; set; }
    }
}
