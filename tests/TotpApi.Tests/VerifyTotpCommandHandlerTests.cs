using Moq;
using TotpApi.Application;
using TotpApi.Infrastructure;
using Xunit;

namespace TotpApi.Tests;

public class VerifyTotpCommandHandlerTests
{
    [Fact]
    public async Task Handle_ReturnsFalse_WhenUserNotFound()
    {
        var ad = new Mock<IActiveDirectoryService>();
        ad.Setup(s => s.GetOtpPin(It.IsAny<string>())).Returns((string?)null);
        var totp = new Mock<ITotpService>();
        var handler = new VerifyTotpCommandHandler(ad.Object, totp.Object);
        var result = await handler.Handle(new VerifyTotpCommand("user", "123"), default);
        Assert.False(result);
    }

    [Fact]
    public async Task Handle_ReturnsTrue_WhenValid()
    {
        var ad = new Mock<IActiveDirectoryService>();
        ad.Setup(s => s.GetOtpPin("user")).Returns("data");
        var totp = new Mock<ITotpService>();
        totp.Setup(s => s.ValidateCode("data", "code")).Returns(true);
        var handler = new VerifyTotpCommandHandler(ad.Object, totp.Object);
        var result = await handler.Handle(new VerifyTotpCommand("user", "code"), default);
        Assert.True(result);
    }
}
