using ErrorOr;
using MediatR;
using Vonage.Voice;

namespace Application.Orders.Query.GetOrdersByDateWithMeals;

public record GetOrdersByDateWithNutritionQuery(DateOnly DeliveryDate)
   : IRequest<ErrorOr<List<OrderWithNutritionResponse>>>;
public class OrderWithNutritionResponse
{
    public int OrderId { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public List<MealWithNutritionResponse> BreakfastAndDinner { get; set; } = new();
    // الغداء (بروتين ومعاه كارب)
    public List<LunchProteinWithCarbResponse> Lunch { get; set; } = new();
}

public class MealWithNutritionResponse
{
    public string MealName { get; set; } = string.Empty;
    public int Count { get; set; } = 1;
    public decimal Quantity { get; set; }
    public string? Notes { get; set; }

    public NutritionResponse Nutrition { get; set; } = new();
}

public class LunchProteinWithCarbResponse
{
    public string ProteinName { get; set; } = string.Empty;
    public decimal ProteinQuantity { get; set; }
    public string? Notes { get; set; }

    public NutritionResponse ProteinNutrition { get; set; } = new();

    // الكارب المرتبط بالبروتين
    public CarbResponse? Carb { get; set; }
}
public class CarbResponse
{
    public string CarbName { get; set; } = string.Empty;
    public decimal CarbQuantity { get; set; }
    public string? Notes { get; set; }

    public NutritionResponse CarbNutrition { get; set; } = new();
}


public class NutritionResponse
{
    public decimal Calories { get; set; }
    public decimal Protein { get; set; }
    public decimal Carbs { get; set; }
    public decimal Fats { get; set; }
}