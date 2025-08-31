using System;
using System.Collections.Generic;

namespace Presentation.Dtos
{
    public class PlaceOrderRequest
    {
        public string PlanName { get; set; }
        public uint DurationInDays { get; set; }
        public uint NumberOfLunchMeals { get; set; }
        public decimal BreakfastPrice { get; set; }
        public decimal DinnerPrice { get; set; }
        public uint PastaCarbGrams { get; set; }
        public uint RiceCarbGrams { get; set; }
        public DateTime StartDate { get; set; }
        public bool IsActive { get; set; }
        public List<PlaceOrderPlanCategoryDto> LunchCategories { get; set; } = new();
        public Guid? PromoCodeId { get; set; }
        public string? PromoCode { get; set; }
    }

    public class PlaceOrderPlanCategoryDto
    {
        public int? SubCategoryId { get; set; }
        public string Name { get; set; }
        public uint NumberOfMeals { get; set; }
        public uint ProteinGrams { get; set; }
        public decimal PricePerGram { get; set; }
        public bool AllowProteinChange { get; set; }
        public uint MaxProteinGrams { get; set; }
    }
}
