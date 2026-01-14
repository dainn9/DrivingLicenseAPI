namespace DrivingLicense.Domain.Enums
{
    public enum RegisterFileStatus
    {
        Created = 0,
        WaitingApproval = 1,
        Approved = 2,
        InProgress = 3,
        Completed = 4,
        Rejected = 5,
    }
}