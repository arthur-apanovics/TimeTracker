using Core.Entities;
using Core.Models;
using FluentAssertions;
using Xunit;

namespace Core.Tests
{
    public class TrackerTaskTests
    {
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
            var sut = TrackerTask.Create(nameof(Init_ActivitiesIsEmpty));

            sut.Activities.Should().BeEmpty();
        }
        
        [Fact]
        public void CreateActivity_CreatesAndAddsActivityFromDescription_WhenValid()
        {
            var sut = TrackerTask.Create("Test Task");
            const string activityDescription = "Test activity";
            var activity = TrackerActivity.Create(activityDescription);

            sut.AddActivity(activity);
            
            // assert count
            sut.Activities.Count.Should().Be(1);
            
            // assert object
            sut.GetActivity(activityDescription).Should().Be(activity);
        }
    }
}