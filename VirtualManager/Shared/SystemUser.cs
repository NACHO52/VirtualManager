namespace VirtualManager.Shared
{
    public class SystemUser
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public SystemUserStatus Status { get; set; }
    }

    public enum SystemUserStatus
    {
        Active = 1,
        Suspended = 2
    }
}