using Application.Interfaces;
using ErrorOr;
using MediatR;

namespace Application.Authentication.Commands.Otp.SendOtp;

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

        await _otpRepository.SaveOtpAsync(request.PhoneNumber, otp);

        var PhoneNumber = "2" + request.PhoneNumber;

        var restult = await _smsRepository.SendSmsAsync(PhoneNumber, $"Your OTP code is: {otp}");

        if (restult == false)
        {
            return Error.Failure(code: "Sms.SendError", description: "Failed to send OTP SMS.");
        }

        return "OTP was sent successfully";
    }
}
