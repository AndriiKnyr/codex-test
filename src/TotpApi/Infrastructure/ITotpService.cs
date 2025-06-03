namespace TotpApi.Infrastructure;

public interface ITotpService
{
    bool ValidateCode(string otpPin, string code);
}
