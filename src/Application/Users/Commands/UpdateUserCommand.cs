using Application.Plans.Commands.CreatePlan;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Commands
{
    public record UpdateUserCommand(
        string Id,
        string FirstName,
        string MiddleName,
        string LastName,
        int CityId,
        string MainAddress,
        string? SecondaryAddress,
        string SecondPhoneNumber,
        bool IsTestUser,
        string? Email
    ) : IRequest<ErrorOr<UpdateUserCommandResponse>>;


    public record UpdateUserCommandResponse(
        string Id,
        string FirstName,
        string LastName,
        string Email
    );
}
