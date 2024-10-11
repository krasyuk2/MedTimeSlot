using MedTimeSlot.DataAccess.DataAccess;

namespace MedTimeSlot.DataAccess.Repositories;

public class RegisterRepository
{
    private readonly MedicalTimeSlotDbContext _context;

    public RegisterRepository(MedicalTimeSlotDbContext context)
    {
        _context = context;
    }
}