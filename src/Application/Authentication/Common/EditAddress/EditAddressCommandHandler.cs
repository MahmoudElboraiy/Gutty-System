using Application.Interfaces;
using Domain.Models.Identity;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authentication.Common.EditAddress
{
    public class EditAddressCommandHandler : IRequestHandler<EditAddressCommand, ErrorOr<ResultMessage>>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly UserManager<User> _userManager;
        public EditAddressCommandHandler(
            ICurrentUserService currentUserService,
            UserManager<User> userManager
            )
        {
            _currentUserService = currentUserService;
            _userManager = userManager;
        }
        public async Task<ErrorOr<ResultMessage>> Handle(EditAddressCommand request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                return new ResultMessage(false, "User not found.");
            }
            // Here you would update the user's address based on the request data
            // For example:
            user.MainAddress = string.IsNullOrEmpty(request.NewMainAddress) ?user.MainAddress :request.NewMainAddress ;
            user.SecondaryAddress = string.IsNullOrEmpty(request.NewSecondaryAddress) ? user.SecondaryAddress : request.NewSecondaryAddress;

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return new ResultMessage(true, "Address updated successfully.");
            }
            else
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return new ResultMessage(false, $"Failed to update address: {errors}");
            }
        }
    }
}
