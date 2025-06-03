using MediatR;
using TotpApi.Infrastructure;

namespace TotpApi.Application;

public class VerifyTotpCommandHandler : IRequestHandler<VerifyTotpCommand, bool>
{
    private readonly IActiveDirectoryService _adService;
    private readonly ITotpService _totpService;

    public VerifyTotpCommandHandler(IActiveDirectoryService adService, ITotpService totpService)
    {
        _adService = adService;
        _totpService = totpService;
    }

    public Task<bool> Handle(VerifyTotpCommand request, CancellationToken cancellationToken)
    {
        var otpPin = _adService.GetOtpPin(request.Username);
        if (otpPin == null)
            return Task.FromResult(false);

        var isValid = _totpService.ValidateCode(otpPin, request.Code);
        return Task.FromResult(isValid);
    }
}
