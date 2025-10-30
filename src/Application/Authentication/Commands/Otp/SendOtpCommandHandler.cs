

using Application.Interfaces;
using ErrorOr;
using MediatR;

namespace Application.Authentication.Commands.Otp;

public class SendOtpCommandHandler : IRequestHandler<SendOtpCommand, ErrorOr<string>>
{
    private readonly ISmsRepository _smsRepository;
    private readonly IOtpRepository _otpRepository;

    public SendOtpCommandHandler(ISmsRepository smsRepository, IOtpRepository otpRepository)
    {
        _smsRepository = smsRepository;
        _otpRepository = otpRepository;
    }

    public async Task<ErrorOr<string>> Handle(SendOtpCommand request, CancellationToken cancellationToken)
    {
        var otp = new Random().Next(100000, 999999).ToString();

        var sent = await _smsRepository.SendSmsAsync(request.PhoneNumber, $"Your verification code is :  {otp}");
        if (!sent)
            return Error.Failure("Sms.Failed", "Failed to send Sms");

        await _otpRepository.SaveOtpAsync(request.PhoneNumber, otp);
        return "Sms was sent successfully";
    }
}
