using System;
using System.Threading;
using Core.API.Models;
using Core.Tests.Fixtures;
using FluentAssertions;
using Xunit;

namespace Core.Tests.Tests
{
    public class TrackerActivityTests : IClassFixture<TrackerTaskFixture>
    {
        private readonly TrackerTaskFixture _taskFixture;

        public TrackerActivityTests(TrackerTaskFixture taskFixture)
        {
            _taskFixture = taskFixture;
        }

        [Theory]
        [InlineData(1)]
        [InlineData(3)]
        // [InlineData(6)]
        public void Tracking_RecordsPassedTime(int activityLengthSeconds)
        {
            // Arrange
            var sut = new TrackerActivity(null);
            var expected = TimeSpan.FromSeconds(activityLengthSeconds);

            // Act
            sut.StartTracking();
            Thread.Sleep(activityLengthSeconds * 1000);
            sut.StopTracking();

            var actual = sut.TimeSpent;

            // Assert
            actual.Should().BeCloseTo(expected, 500);
        }
    }
}
