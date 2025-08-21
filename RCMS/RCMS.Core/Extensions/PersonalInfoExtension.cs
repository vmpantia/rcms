using RCMS.Infrastructure.DataAccess.Entities.Contracts;

namespace RCMS.Core.Extensions;

public static class PersonalInfoExtension
{
    public static string GetFormattedName(this IPersonalInfoEntity entity)
    {
        var middleInitial = string.IsNullOrWhiteSpace(entity.MiddleName)
            ? string.Empty : $" {entity.MiddleName.Trim()[0].ToString().ToUpper()}.";

        return $"{entity.LastName}, {entity.FirstName}{middleInitial}";
    }
    
    public static string GetInitials(this IPersonalInfoEntity entity)
    {
        var firstInitial = string.IsNullOrWhiteSpace(entity.FirstName) ? string.Empty : entity.FirstName.Trim()[0].ToString().ToUpper();
        var lastInitial = string.IsNullOrWhiteSpace(entity.LastName) ? string.Empty : entity.LastName.Trim()[0].ToString().ToUpper();

        return $"{firstInitial}{lastInitial}";
    }
}