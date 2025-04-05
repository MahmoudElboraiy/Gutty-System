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
    }
}
