namespace FocusFM.Common.Enum
{
    public static class Status
    {
        public const int Success = 0;
        public const int Failed = 1;
        public const int AlredyExists = 2;
        public const int DataUpdated = 3;
        public const int DataInUse = 4;
        public const int InvalidSortOrder = 5;
        public const int AlreadyAddedButPending = -3;
        public const int DocumentCountGraterThenTen = 6;
    }

    public static class ActiveStatus
    {
        public const int Active = 1;
        public const int Inactive = 0;
    }

    public static class LoginFrom
    {
        public const int AdminPanel = 1;
        public const int App = 2;
    }

    public static class IsApproved
    {
        public const int Pending = 0;
        public const int Success = 1;
        public const int Rejected =2;
    }
}