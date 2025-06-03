using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;

namespace TotpApi.Infrastructure;

public class ActiveDirectoryService : IActiveDirectoryService
{
    private readonly string _domain;

    public ActiveDirectoryService(IConfiguration configuration)
    {
        _domain = configuration.GetValue<string>("ActiveDirectory:Domain") ?? string.Empty;
    }

    public string? GetOtpPin(string username)
    {
        if (string.IsNullOrEmpty(username))
            return null;

        using var context = new PrincipalContext(ContextType.Domain, _domain);
        using var user = UserPrincipal.FindByIdentity(context, username);
        if (user == null)
            return null;

        using var dirEntry = user.GetUnderlyingObject() as DirectoryEntry;
        return dirEntry?.Properties["otpPin"]?.Value as string;
    }
}
