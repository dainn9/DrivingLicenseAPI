namespace DrivingLicense.Domain.Entities
{
    public class Student : Person
    {
        public string Nationality { get; set; } = null!;
        public string IdentityCard { get; set; } = null!;

        // Navigation properties
        public ICollection<RegisterFile> RegisterFiles { get; set; } = new List<RegisterFile>();
    }
}