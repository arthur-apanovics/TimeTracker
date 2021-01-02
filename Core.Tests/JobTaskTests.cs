using Core.Entities;
using Core.Models;
using Core.Tests.Fixtures;
using FluentAssertions;
using Xunit;

namespace Core.Tests
{
    public class TrackerTaskTests: IClassFixture<TrackerTaskFixture>
    {
        private readonly TrackerTaskFixture _taskFixture;
        
        public TrackerTaskTests(TrackerTaskFixture taskFixture)
        {
            _taskFixture = taskFixture;
        }
        
        [Fact]
        public void Init_Constructor_WhenValid()
        {
            const string expected = nameof(Init_Constructor_WhenValid);
            var sut = TrackerTask.Create(expected);

            sut.Title.Should().Be(expected);
        }
        
        [Fact]
        public void Init_ActivitiesIsEmpty()
        {
            var sut = _taskFixture.EmptyTask;

            sut.Activities.Should().BeEmpty();
        }
        
        [Fact]
        public void CreateActivity_CreatesAndAddsActivityFromDescription_WhenValid()
        {
            var sut = _taskFixture.EmptyTask;
            var activityDescription = MockData.Slogan;
            var activity = TrackerActivity.Create(activityDescription);

            sut.AddActivity(activity);
            
            // assert count
            sut.Activities.Count.Should().Be(1);
            
            // assert object
            sut.GetActivity(activityDescription).Should().Be(activity);
        }
    }
}