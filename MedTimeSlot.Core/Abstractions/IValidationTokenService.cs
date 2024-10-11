namespace MedTimeSlot.Core.Abstractions;

public interface IValidationTokenService
{
    bool ValidateToken(string login);
}