namespace Presentation.Dtos
{
    public class CalculateCategoryRequest
    {
        public Guid CategoryId { get; set; }
        public uint? NumberOfMeals { get; set; }
        public uint? ProteinGrams { get; set; }
    }
}
