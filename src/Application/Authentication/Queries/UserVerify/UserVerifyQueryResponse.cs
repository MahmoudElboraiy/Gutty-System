namespace Application.Authentication.Queries.UserVerify;

public record UserVerifyQueryResponse(
    string UserId,
    string PhoneNumber,
    string FirstName,
    string MiddleName,
    string LastName,
    string MainAddress,
    string SecondPhoneNumber,
    string? Email,
    bool PhoneNumberConfirmed,
    IList<string> Roles 
    );