namespace Presentation.Dtos
{
    public class CalculateCategoryRequest
    {
        public int CategoryId { get; set; }
        public uint? NumberOfMeals { get; set; }
        public uint? ProteinGrams { get; set; }
    }
}
