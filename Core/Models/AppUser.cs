using Core.Interfaces;

namespace Core.Models
{
    public class AppUser : IAppUser
    {
        public string FullName => $"{FirstName} {LastName}";
        public string FirstName { get; protected set; }
        public string LastName { get; protected set; }
        public string Email { get; protected set; }
        public string Office { get; protected set; }

        public static IAppUser Create(string firstName, string lastName, string email, string office)
        {
            return new AppUser
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Office = office
            };
        }
    }
}