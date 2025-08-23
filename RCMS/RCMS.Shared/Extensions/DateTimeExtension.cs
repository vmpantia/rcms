namespace RCMS.Shared.Extensions;

public static class DateTimeExtension
{
    public static int GetAge(this DateTime? birthDate, DateTime? referenceDate = null)
    {
        if (birthDate == null) return 0;
        
        var today = referenceDate ?? DateTime.Today;
        var age = today.Year - ((DateTime)birthDate).Year;

        // Adjust if a birthday hasn't occurred yet this year
        if (birthDate?.Date > today.AddYears(-age)) age--;

        return age;
    }
}