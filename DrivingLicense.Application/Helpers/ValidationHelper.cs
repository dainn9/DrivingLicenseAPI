using System.Text.RegularExpressions;

namespace DrivingLicense.Application.Helpers
{
    public static class ValidationHelper
    {
        public static bool IsValidCCCD(string cccd)
        {
            if (string.IsNullOrWhiteSpace(cccd)) return false;
            return Regex.IsMatch(cccd, @"^\d{9}$|^\d{12}$");
        }

        public static bool IsValidBirthDate(DateOnly birthDate, int minimumAge = 18)
        {
            var today = DateOnly.FromDateTime(DateTime.Today);

            if (birthDate > today) return false;

            int age = today.Year - birthDate.Year;

            if (birthDate > today.AddYears(-age))
                age--;

            return age >= minimumAge;
        }
    }
}