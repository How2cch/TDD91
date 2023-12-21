namespace Butget.Models;

public class Budget
{
    public string YearMonth { get; set; }
    public int Amount { get; set; }
    public DateTime StartOfMonth => DateTime.ParseExact(YearMonth, "yyyyMM", null);

    public int GetPartialAmount(int days)
    {
        return Amount/DateTime.DaysInMonth(StartOfMonth.Year, StartOfMonth.Month) * days;
    }

    public bool IsSameMonth(DateTime startDate)
    {
        return StartOfMonth.Year == startDate.Year &&
               StartOfMonth.Month == startDate.Month;
    }
}