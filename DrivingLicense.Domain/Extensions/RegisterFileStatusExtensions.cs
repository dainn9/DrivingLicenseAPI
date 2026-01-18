using DrivingLicense.Domain.Enums;

namespace DrivingLicense.Domain.Extensions
{
    public static class RegisterFileStatusExtensions
    {
        public static string ToText(this RegisterFileStatus registerFileStatus)
        {
            return registerFileStatus switch
            {
                RegisterFileStatus.Created => "Đã tạo",
                RegisterFileStatus.WaitingApproval => "Đang chờ duyệt",
                RegisterFileStatus.Approved => "Đã duyệt",
                RegisterFileStatus.Rejected => "Từ chối",
                RegisterFileStatus.InProgress => "Đang học",
                RegisterFileStatus.Completed => "Hoàn thành khóa",
                _ => "Không xác định"
            };
        }
    }
}
