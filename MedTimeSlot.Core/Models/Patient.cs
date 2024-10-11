namespace MedTimeSlot.Core.Models;

public class Patient : Person
{
  
    public List<CellTime> CellTimes { get; set; }
}