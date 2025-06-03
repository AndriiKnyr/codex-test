using MediatR;
using Microsoft.AspNetCore.Mvc;
using TotpApi.Application;

namespace TotpApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VerificationController : ControllerBase
{
    private readonly IMediator _mediator;

    public VerificationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<bool>> Verify(VerifyTotpDto dto)
    {
        var result = await _mediator.Send(new VerifyTotpCommand(dto.Username, dto.Code));
        return Ok(result);
    }
}

public record VerifyTotpDto(string Username, string Code);
