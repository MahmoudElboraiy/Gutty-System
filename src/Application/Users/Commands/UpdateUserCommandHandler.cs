using Domain.Models.Identity;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Commands
{
    public class UpdateUserCommandHandler 
        : IRequestHandler<UpdateUserCommand, ErrorOr<UpdateUserCommandResponse>>
    {
        private readonly UserManager<User> _userManager;

        public UpdateUserCommandHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }


        public async Task<ErrorOr<UpdateUserCommandResponse>> Handle(
             UpdateUserCommand request,
             CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.Id);
            if (user is null)
            {
                return Error.NotFound(description: "User not found.");
            }

            user.FirstName = request.FirstName;
            user.MiddleName = request.MiddleName;
            user.LastName = request.LastName;
            user.CityId = request.CityId;
            user.MainAddress = request.MainAddress;
            user.SecondaryAddress = request.SecondaryAddress;
            user.SecondPhoneNumber = request.SecondPhoneNumber;
            user.IsTestUser = request.IsTestUser;
            user.Email = request.Email;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                var errors = result.Errors
                    .Select(e => Error.Validation(code: e.Code, description: e.Description))
                    .ToList();

                return errors;
            }

            var response = new UpdateUserCommandResponse(
                user.Id,
                user.FirstName,
                user.LastName,
                user.Email
            );

            return response;
        }

    }
}
