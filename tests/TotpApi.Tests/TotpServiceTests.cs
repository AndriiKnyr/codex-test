using OtpNet;
using TotpApi.Infrastructure;
using Xunit;

namespace TotpApi.Tests;

public class TotpServiceTests
{
    [Fact]
    public void ValidateCode_ReturnsTrue_ForValidCode()
    {
        var key = KeyGeneration.GenerateRandomKey(20);
        var base64 = Convert.ToBase64String(key);
        var totp = new Totp(key);
        var code = totp.ComputeTotp();
        var otpPin = $"#@work={base64}&,";
        var service = new TotpService();
        Assert.True(service.ValidateCode(otpPin, code));
    }

    [Fact]
    public void ValidateCode_ReturnsFalse_ForInvalidCode()
    {
        var key = KeyGeneration.GenerateRandomKey(20);
        var base64 = Convert.ToBase64String(key);
        var totp = new Totp(key);
        var code = totp.ComputeTotp();
        var otpPin = $"#@work={base64}&,";
        var service = new TotpService();
        Assert.False(service.ValidateCode(otpPin, "123456"));
    }
}
