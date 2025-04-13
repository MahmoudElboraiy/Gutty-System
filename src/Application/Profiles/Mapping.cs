using Application.Users.Queries.GetUsers;
using Domain.Models.Identity;

namespace Application.Profiles;

public static class Mapping
{
    public static UserResponse MapUserResponse(this User user) => new(
        user.Id,
        user.PhoneNumber,
        user.FirstName,
        user.MiddleName,
        user.LastName,
        user.MainAddress,
        user.SecondPhoneNumber,
        user.Email,
        user.PhoneNumberConfirmed
    );

}