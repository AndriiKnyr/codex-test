using OtpNet;

namespace TotpApi.Infrastructure;

public class TotpService : ITotpService
{
    public bool ValidateCode(string otpPin, string code)
    {
        if (string.IsNullOrWhiteSpace(otpPin) || string.IsNullOrWhiteSpace(code))
            return false;

        var keys = ParseKeys(otpPin);
        foreach (var k in keys)
        {
            if (TryValidate(k, code))
                return true;
        }
        return false;
    }

    private static IEnumerable<string> ParseKeys(string otpPin)
    {
        var parts = otpPin.Split(',', StringSplitOptions.RemoveEmptyEntries);
        foreach (var p in parts)
        {
            var idx = p.IndexOf('=');
            if (idx < 0) continue;
            var value = p[(idx + 1)..];
            value = value.TrimEnd('&');
            if (!string.IsNullOrWhiteSpace(value))
                yield return value;
        }
    }

    private static bool TryValidate(string base64Key, string code)
    {
        try
        {
            var bytes = Convert.FromBase64String(base64Key);
            var totp = new Totp(bytes);
            return totp.VerifyTotp(code, out _);
        }
        catch
        {
            return false;
        }
    }
}
