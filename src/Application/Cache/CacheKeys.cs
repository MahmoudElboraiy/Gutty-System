
namespace Application.Cache;

public static class CacheKeys
{
    // ===== Versions =====
    public const string CustomersVersion = "cache_version_customers";
    public const string PlansVersion = "cache_version_plans";
    public const string MealsVersion = "cache_version_meals";
    public const string SubCategoriesVersion = "cache_version_subcategories";
    public const string CategoriesVersion = "cache_version_categories";
    public const string IngredientsVersion = "cache_version_ingredients";
    public const string PromoCodesVersion = "cache_version_promocodes";

    // ===== Base Keys =====
    public const string Customers = "customers";
    public const string Plans = "plans";
    public const string Meals = "meals";
    public const string SubCategories = "subcategories";
    public const string CategoriesAll = "categories_all";
    public const string Ingredients = "ingredients";
    public const string PromoCodes = "promocodes";

    // ===== Keys for Get Methods =====
    // Customers
    public const string CustomersAll = "customers_all";          // for Get All (pagination)
    public const string CustomerById = "customer_by_id";         // for Get By Id

    // Plans
    public const string PlansAll = "plans_all";
    public const string PlanById = "plan_by_id";

    // Meals
    public const string MealsAll = "meals_all";
    public const string MealById = "meal_by_id";

    // SubCategories
    public const string SubCategoriesAll = "subcategories_all";
    public const string SubCategoryById = "subcategory_by_id";

    // Categories
    public const string CategoryById = "category_by_id";


    // Ingredients
    public const string IngredientsAll = "ingredients_all";
    public const string IngredientById = "ingredient_by_id";

    // PromoCodes
    public const string PromoCodesAll = "promocodes_all";
    public const string PromoCodeById = "promocode_by_id";
}
