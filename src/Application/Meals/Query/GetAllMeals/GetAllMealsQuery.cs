
using Domain.Enums;
using ErrorOr;
using MediatR;

namespace Application.Meals.Query.GetAllMeals
{
    public record GetAllMealsQuery(int pageNumber,int pageSize) : IRequest<ErrorOr<List<GetAllMealsQueryResponse>>>;
    public record GetAllMealsQueryResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public decimal Calories { get; set; }
        public decimal Protein { get; set; }
        public decimal Carbs { get; set; }
        public decimal Fats { get; set; }
        public MealType? MealType { get; set; }
        public string SubcategoryName { get; set; }
        public decimal? DefaultQuantityGrams { get; set; }
    }
}
