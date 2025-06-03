using MediatR;

namespace TotpApi.Application;

public record VerifyTotpCommand(string Username, string Code) : IRequest<bool>;
