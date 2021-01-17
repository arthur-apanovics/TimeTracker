namespace API.Models.Interfaces
{
    public interface ITrackerUser
    {
        public string FullName { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string Email { get; }
        public string Office { get; }
    }
}
