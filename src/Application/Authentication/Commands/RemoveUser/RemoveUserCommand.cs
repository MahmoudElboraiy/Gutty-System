using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authentication.Commands.RemoveUser
{
    public record RemoveUserCommand(string PhoneNumber) : IRequest<ErrorOr<ResultSuccess>>;
    public record ResultSuccess(
        bool IsSuccess,
        string Message
        );
}
