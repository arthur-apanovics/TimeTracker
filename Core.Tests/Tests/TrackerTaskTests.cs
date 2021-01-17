using System;
using System.Linq;
using Core.Tests.Fixtures;
using Core.Tests.Mock;
using FluentAssertions;
using Xunit;

namespace Core.Tests.Tests
{
    public class TrackerTaskTests: IClassFixture<TrackerTaskFixture>, IDisposable
    {
        private readonly TrackerTaskFixture _taskFixture;

        public TrackerTaskTests(TrackerTaskFixture taskFixture)
        {
            _taskFixture = taskFixture;
        }

        public void Dispose()
        {
            _taskFixture.Dispose();
        }

        [Fact]
        public void CreateActivity_SavesToDatabase_WhenValid()
        {
            var task = _taskFixture.EmptyTask;
            string desc = MockData.Slogan;

            var sut = task.CreateActivity(desc);

            sut.Id.Should().BeGreaterThan(0);
            task.Activities.Count.Should().Be(1);
            task.Activities.Should().Contain(sut);
        }
    }
}
