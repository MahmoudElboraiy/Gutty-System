namespace Application.Authentication.Queries.UserVerify;

public record UserVerifyQueryResponse(
    string UserId,
    string PhoneNumber,
    string Name,
    string MainAddress,
    string? SecondMainAddress,
    string SecondPhoneNumber,
    string? Email,
    bool PhoneNumberConfirmed,
    IList<string> Roles
);
