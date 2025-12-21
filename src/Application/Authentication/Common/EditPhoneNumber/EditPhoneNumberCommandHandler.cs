
using Application.Cache;
using Application.Interfaces;
using Domain.Models.Identity;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Authentication.Common.EditPhoneNumber;

public class EditPhoneNumberCommandHandler : IRequestHandler<EditPhoneNumberCommand, ErrorOr<ResultMessage>>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly UserManager<User> _userManager;
    private readonly ICacheService _cacheService;
    //   private readonly IOtpRepository _otpRepository;

    public EditPhoneNumberCommandHandler(
         ICurrentUserService currentUserService,
         UserManager<User> userManager,
         ICacheService cacheService
         //         IOtpRepository otpRepository
         )
    {
        _currentUserService = currentUserService;
        _userManager = userManager;
        _cacheService = cacheService;
        //   _otpRepository = otpRepository;
    }
    public async Task<ErrorOr<ResultMessage>> Handle(EditPhoneNumberCommand request, CancellationToken cancellationToken)
    {
     //   var verified = await _otpRepository.GetOtpAsync($"{request.NewPhoneNumber}");

       // if (verified == null || !verified.IsVerified)
       //     return Error.Validation("Otp.NotVerified", "The code must be verified first before resetting.");

        var userId = _currentUserService.UserId;
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return new ResultMessage(false, "User not found.");
        }
        user.PhoneNumber = request.NewPhoneNumber;
        user.UserName = request.NewPhoneNumber;

        var result = await _userManager.UpdateAsync(user);       
        if (result.Succeeded)
        {
            //  await _otpRepository.RemoveOtpAsync($"{request.NewPhoneNumber}");
            _cacheService.IncrementVersion(CacheKeys.CustomersVersion);
            return new ResultMessage(true, "Phone number updated successfully.");
        }
        else
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            return new ResultMessage(false, $"Failed to update phone number: {errors}");
        }
    }
}
