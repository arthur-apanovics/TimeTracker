using Core.API.Models.Interfaces;

namespace Core.API.Models
{
    public class TrackerUser : ITrackerUser
    {
        public string FullName => $"{FirstName} {LastName}";
        public string FirstName { get; protected set; }
        public string LastName { get; protected set; }
        public string Email { get; protected set; }
        public string Office { get; protected set; }

        public static ITrackerUser Create(string firstName, string lastName, string email, string office)
        {
            return new TrackerUser
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Office = office
            };
        }
    }
}
