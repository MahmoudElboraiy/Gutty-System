using ErrorOr;

namespace Domain.DErrors;

public static class DomainErrors
{
    public static class Authentication
    {
        public static Error InvalidCredentials() =>
            Error.Unauthorized("Authentication.InvalidCredentials", "Invalid credentials.");

        public static Error DuplicatePhoneNumber(string phoneNumber) =>
            Error.Conflict(
                "Authentication.DuplicatePhoneNumber",
                $"User with phone number {phoneNumber} already exists."
            );
        public static Error UserNotFound(string userId) =>
            Error.NotFound("Authentication.UserNotFound", $"User with id {userId} not found.");
    }

    public static class Ingredients
    {
        public static Error IngredientNotFound(int requestId) =>
            Error.NotFound("Ingredients.IngredientNotFound", $"Ingredient with id {requestId} not found.");
    }

    public static class Items
    {
        public static Error ItemNotFound(Guid requestId) =>
            Error.NotFound("Items.ItemNotFound", $"Item with id {requestId} not found.");
    }
}
