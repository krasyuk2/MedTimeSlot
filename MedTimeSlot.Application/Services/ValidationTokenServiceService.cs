using System.Security.Claims;
using MedTimeSlot.Core.Abstractions;
using Microsoft.AspNetCore.Http;

namespace MedTimeSlot.Application.Services;

public class ValidationTokenServiceService : IValidationTokenService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ValidationTokenServiceService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public bool ValidateToken(string login)
    {
        var user = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (user == login) return true;
        return false;
    }
}