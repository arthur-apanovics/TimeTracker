using System;
using System.Threading;
using Core.Models;
using Core.Tests.Fixtures;
using FluentAssertions;
using Xunit;

namespace Core.Tests
{
    public class ActivityTests : IClassFixture<TrackerTaskFixture>
    {
        private readonly TrackerTaskFixture _taskFixture;

        public ActivityTests(TrackerTaskFixture taskFixture)
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
            var sut = TrackerActivity.Create(nameof(Tracking_RecordsPassedTime));
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