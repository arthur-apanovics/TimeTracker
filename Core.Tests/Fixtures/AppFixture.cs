using System;
using Core.Interfaces;
using Core.Models;

namespace Core.Tests.Fixtures
{
    public class AppFixture : IDisposable
    {
        public App EmptyApp { get; set; }

        public AppFixture()
        {
            var user = AppUser.Create( "John", "Doe", "j@d.com", "CHC");
            EmptyApp = new App {AppUser = user};
        }
        
        public void Dispose()
        {
            // clean up test records in database 
        }
    }
}