using System;
using Core.Interfaces;
using Core.Models;

namespace Core.Tests.Fixtures
{
    public class TrackerTaskFixture : IDisposable
    {
        public ITrackerTask EmptyTrackerTask { get; private set; }

        public TrackerTaskFixture()
        {
            EmptyTrackerTask = TrackerTask.Create("Fixture task");
        }

        public void Dispose()
        {
            // clean up test records in database 
        }
    }
}