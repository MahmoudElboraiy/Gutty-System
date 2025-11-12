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

namespace Application.Authentication.Common.EditName
{
    public class EditNameCommandHandler : IRequestHandler<EditNameCommand, ErrorOr<ResultMessage>>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly UserManager<User> _userManager;
        public EditNameCommandHandler(
            ICurrentUserService currentUserService,
            UserManager<User> userManager
            )
        {
            _currentUserService = currentUserService;
            _userManager = userManager;
        }
        public async Task<ErrorOr<ResultMessage>> Handle(EditNameCommand request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                return new ResultMessage(false, "User not found.");
            }
            // Here you would update the user's name based on the request data
            // For example:
            user.Name = string.IsNullOrEmpty(request.Name) ? user.Name : request.Name;

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return new ResultMessage(true, "Name updated successfully.");
            }
            else
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return new ResultMessage(false, $"Failed to update name: {errors}");
            }
        }

    }
}
