using System.Collections.Generic;

namespace Core.Interfaces
{
    public interface IAppUser
    {
        public string FullName { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string Email { get; }
        public string Office { get; }
    }
}