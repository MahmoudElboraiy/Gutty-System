namespace Presentation.Dtos
{
    public class CalculatePlanPriceRequest
    {
        public uint? RiceCarbGrams { get; set; }
        public uint? PastaCarbGrams { get; set; }
        public List<CalculateCategoryRequest>? Categories { get; set; }
    }
}
