using DrivingLicense.Domain.Enums;

namespace DrivingLicense.Application.Validators
{
    public static class RegisterFileValidator
    {
        public static bool IsValidStatusTransition(RegisterFileStatus current, RegisterFileStatus next)
        {
            return current switch
            {
                RegisterFileStatus.Created => next == RegisterFileStatus.WaitingApproval,
                RegisterFileStatus.WaitingApproval => next == RegisterFileStatus.Rejected || next == RegisterFileStatus.Approved,
                RegisterFileStatus.Rejected => next == RegisterFileStatus.WaitingApproval,
                RegisterFileStatus.Approved => next == RegisterFileStatus.InProgress,
                RegisterFileStatus.InProgress => next == RegisterFileStatus.Completed,
                RegisterFileStatus.Completed => false,
                _ => false
            };
        }
    }
}
