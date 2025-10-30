using Application.Authentication.Commands.ForgetPassword.ResetPassword;
using Application.Authentication.Commands.ForgetPassword.VerifyForgetPasswordOtp;
using Application.Authentication.Commands.Otp;
using Application.Authentication.Commands.UserRegister;
using Application.Authentication.Commands.UserRegisterTemp;
using Application.Authentication.Commands.VerifyRegisterOtp;
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
    [HttpGet("SendOtp")]
    public async Task<IActionResult> SendOtp(string phone)
    {
        var result = await _mediator.Send(new SendOtpCommand(phone));
        return result.Match<IActionResult>(Ok, BadRequest);
    }
    [HttpPost("UserRegisterTemp")]
    public async Task<IActionResult> UserRegisterTemp([FromBody] UserRegisterTempCommand command)
    {
        var result = await _mediator.Send(command);
        return result.Match<IActionResult>(Ok, BadRequest);
    }
    [HttpPost("VerifyRegisterOtp")]
    public async Task<IActionResult> VerifyRegisterOtp([FromBody] VerifyRegisterOtpCommand command)
    {
        var result = await _mediator.Send(command);
        return result.Match<IActionResult>(Ok, BadRequest);
    }
    [HttpPost("SendForgetPasswordOtp({phoneNumber})")]
    public async Task<IActionResult> SendForgetPasswordOtp(string phoneNumber)
    {
        var result = await _mediator.Send(new SendOtpCommand(phoneNumber));
        return result.Match<IActionResult>(Ok, BadRequest);
    }
    [HttpPost("VerifyForgetPasswordOtp")]
    public async Task<IActionResult> VerifyForgetPasswordOtp([FromBody] VerifyForgetPasswordOtpCommand command)
    {
        var result = await _mediator.Send(command);
        return result.Match<IActionResult>(Ok, BadRequest);
    }
    [HttpPost("ResetPassword")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordCommand command)
    {
        var result = await _mediator.Send(command);
        return result.Match<IActionResult>(Ok, BadRequest);
    }

}
