using Core.Entities;
using Core.Interfaces;

namespace Core.Models
{
    public class AppUser : AppUserEntity, IAppUser
    {
        // email n' stuff will go here

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