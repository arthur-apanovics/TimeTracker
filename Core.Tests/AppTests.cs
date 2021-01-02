using Core;
using Core.Entities;
using Core.Models;
using Core.Tests.Fixtures;
using FluentAssertions;
using Xunit;

namespace Core.Tests
{
    public class AppTests : IClassFixture<AppFixture>
    {
        private readonly AppFixture _fixture;
        
        public AppTests(AppFixture fixture)
        {
            _fixture = fixture;
        }
        
        [Fact]
        public void CreateTask_SavesToDatabase_WhenValid()
        {
            var sut = _fixture.EmptyApp;
            const string taskTitle = nameof(CreateTask_SavesToDatabase_WhenValid);
            
            var task = TrackerTask.Create(taskTitle);
            var actual = sut.InsertTask(task);

            actual.Id.Should()
                .BeOfType(typeof(int))
                .And.BeGreaterThan(0);
            
            // cleanup
            sut.DeleteTask(actual.Id);
            sut.GetTaskOrNull(actual.Id).Should().BeNull();
        }
    }
}