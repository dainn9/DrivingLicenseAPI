namespace DrivingLicense.Application.DTOs.User
{
    public class UserDto
    {
        public string Id { get; set; } = string.Empty;
        public List<string> Role { get; set; } = new List<string>();
    }
}
