namespace Core.Entities
{
    public class AppUserEntity
    {
        public string FullName => $"{FirstName} {LastName}";
        public string FirstName { get; protected set; }
        public string LastName { get; protected set; }
        public string Email { get; protected set; }
        public string Office { get; protected set; }
    }
}