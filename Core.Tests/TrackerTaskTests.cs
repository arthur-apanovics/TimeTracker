using System;
using System.Linq;
using Core.Interfaces;
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
        public void Create_InitFromTitle_WhenValid()
        {
            const string expected = nameof(Create_InitFromTitle_WhenValid);
            var sut = TrackerTask.Create(expected);

            sut.Id.Should().Be(0);
            sut.Title.Should().Be(expected);
            sut.Activities.Should().BeEmpty();
        }
        
        [Fact]
        public void Create_InitFromEntity_WhenValid()
        {
            var expected = _taskFixture.TaskWithOneActivity;
            expected.Id = new Random().Next(1, 100);
            
            var sut = TrackerTask.Create(expected);

            sut.Id.Should().Be(expected.Id);
            sut.Title.Should().Be(expected.Title);
            sut.Activities.Count.Should().Be(1);
            sut.Activities.First().Should().BeEquivalentTo(expected.Activities.First());
            sut.TotalTime.Should().BeCloseTo(expected.TotalTime, 100);
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