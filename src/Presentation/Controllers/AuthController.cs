using Application.Authentication.Commands.AddRole;
using Application.Authentication.Commands.ForgetPassword.ResetPassword;
using Application.Authentication.Commands.ForgetPassword.VerifyForgetPasswordOtp;
using Application.Authentication.Commands.Otp;
using Application.Authentication.Commands.Otp.SendOtp;
using Application.Authentication.Commands.Otp.VerifyOtp;
using Application.Authentication.Commands.RemoveUser;
using Application.Authentication.Commands.UserRegister;
using Application.Authentication.Commands.UserRegisterTemp;
using Application.Authentication.Commands.VerifyRegisterOtp;
using Application.Authentication.Common.EditAddress;
using Application.Authentication.Common.EditName;
using Application.Authentication.Common.EditPhoneNumber;
using Application.Authentication.Common.EditUser;
using Application.Authentication.Queries.GetCoustmors;
using Application.Authentication.Queries.UserLogin;
using Application.Authentication.Queries.UserVerify;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Presentation.Controllers;

[ApiController]
[Route("auth")]
public class AuthController : Controller
{
    private readonly ISender _mediator;

    public AuthController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserRegisterCommand command)
    {
        var result = await _mediator.Send(command);
        return result.Match<IActionResult>(Ok, BadRequest);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginQuery query)
    {
        var result = await _mediator.Send(query);
        return result.Match<IActionResult>(Ok, BadRequest);
    }

    [HttpGet("verify")]
    [Authorize]
    public async Task<IActionResult> Verify()
    {
        var identity = HttpContext.User.Identity.Name;
        var query = new UserVerifyQuery(identity);
        var result = await _mediator.Send(query);
        return result.Match<IActionResult>(Ok, BadRequest);
    }
    [HttpPost("SendOtp")]
    public async Task<IActionResult> SendOtp(string phone)
    {
        var result = await _mediator.Send(new SendOtpCommand(phone));
        return result.Match<IActionResult>(Ok, BadRequest);
    }
    //[HttpPost("UserRegisterTemp")]
    //public async Task<IActionResult> UserRegisterTemp([FromBody] UserRegisterTempCommand command)
    //{
    //    var result = await _mediator.Send(command);
    //    return result.Match<IActionResult>(Ok, BadRequest);
    //}
    [HttpPost("VerifyOtp")]
    public async Task<IActionResult> VerifyOtp([FromBody] VerifyOtpCommand command)
    {
        var result = await _mediator.Send(command);
        return result.Match<IActionResult>(Ok, BadRequest);
    }
    //[HttpPost("SendOtp({phoneNumber})")]
    //public async Task<IActionResult> SendOtp(string phoneNumber)
    //{
    //    var result = await _mediator.Send(new SendOtpCommand(phoneNumber));
    //    return result.Match<IActionResult>(Ok, BadRequest);
    //}
    //[HttpPost("VerifyForgetPasswordOtp")]
    //public async Task<IActionResult> VerifyForgetPasswordOtp([FromBody] VerifyForgetPasswordOtpCommand command)
    //{
    //    var result = await _mediator.Send(command);
    //    return result.Match<IActionResult>(Ok, BadRequest);
    //}
    [HttpPost("ResetPassword")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordCommand command)
    {
        var result = await _mediator.Send(command);
        return result.Match<IActionResult>(Ok, BadRequest);
    }

    [HttpPost("AddRole") ,Authorize(Roles = "Admin")]
    public async Task<IActionResult> AddRole([FromBody] AddRoleCommand command)
    {
        var result = await _mediator.Send(command);
        return result.Match<IActionResult>(Ok, BadRequest);
    }
    [HttpPost("RemoveUser"), Authorize(Roles = "Admin")]
    public async Task<IActionResult> RemoveUser([FromBody] RemoveUserCommand command)
    {
        var result = await _mediator.Send(command);
        return result.Match<IActionResult>(Ok, BadRequest);
    }
    [HttpPost("EditUser"), Authorize]
    public async Task<IActionResult> EditUser([FromBody] EditUserCommand command)
    {
        var result = await _mediator.Send(command);
        return result.Match<IActionResult>(Ok, BadRequest);
    }
    [HttpPost("EditPhoneNumber"), Authorize]
    public async Task<IActionResult> EditPhoneNumber([FromBody] EditPhoneNumberCommand command)
    {
        var result = await _mediator.Send(command);
        return result.Match<IActionResult>(Ok, BadRequest);
    }
    [HttpGet("GetCoustmors")]//, Authorize]
    public async Task<IActionResult> GetCoustmor([FromRoute] int pageNumber = 1, int pageSize = 10)
    {
        var result = await _mediator.Send(new GetCoustmorsQuery(pageNumber, pageSize));
        return result.Match<IActionResult>(Ok, BadRequest);
    }
}
