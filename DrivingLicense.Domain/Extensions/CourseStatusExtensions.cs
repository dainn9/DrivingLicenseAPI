using DrivingLicense.Domain.Enums;

namespace DrivingLicense.Domain.Extensions
{
    public static class CourseStatusExtensions
    {
        public static string ToText(this CourseStatus courseStatus)
        {
            return courseStatus switch
            {
                CourseStatus.Open => "Đang mở",
                CourseStatus.Completed => "Hoàn thành",
                CourseStatus.Closed => "Đóng",
                _ => "Không xác định"
            };
        }
    }
}
