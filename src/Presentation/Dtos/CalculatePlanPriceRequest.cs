namespace Presentation.Dtos
{
    public class CalculatePlanPriceRequest
    {
        public uint? CarbGrams { get; set; }
        public string? PromoCode { get; set; }
        public List<CalculateCategoryRequest>? Categories { get; set; }
    }
}
