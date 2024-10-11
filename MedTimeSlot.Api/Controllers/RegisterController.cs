using MedTimeSlot.Core.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedTimeSlot.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class RegisterController : ControllerBase
{
    private readonly IValidationTokenService _validationTokenService;

    public RegisterController(IValidationTokenService validationTokenService)
    {
        _validationTokenService = validationTokenService;
    }

    [Authorize]
    [HttpGet]
    public IActionResult Test()
    {
        return Ok(new { Message = "Hello World!" });
    }

    [Authorize(Policy = "Doctor")]
    [HttpPost]
    public IActionResult Test2()
    {
        var validstringtoken = _validationTokenService.ValidateToken("doctor1");
        return Ok(new { Message = "What!" + validstringtoken });
    }
}