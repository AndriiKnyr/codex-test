namespace TotpApi.Infrastructure;

public interface IActiveDirectoryService
{
    string? GetOtpPin(string username);
}
