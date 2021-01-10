using System;
using System.Linq;
using Core.Models;
using Core.Tests.Fixtures;
using FluentAssertions;
using Xunit;

namespace Core.Tests
{
    public class AppTests : IClassFixture<AppFixture>,
        IClassFixture<TrackerTaskFixture>
    {
        private readonly AppFixture _appFixture;
        private readonly TrackerTaskFixture _taskFixture;

        public AppTests(AppFixture appFixture, TrackerTaskFixture taskFixture)
        {
            _appFixture = appFixture;
            _taskFixture = taskFixture;
        }

        [Fact]
        public void CreateTask_SavesToDatabase_WhenValid()
        {
            var sut = _appFixture.EmptyApp;
            const string taskTitle =
                nameof(CreateTask_SavesToDatabase_WhenValid);

            var task = TrackerTask.Create(taskTitle);
            var actual = sut.InsertTask(task);

            actual.Id.Should().BeOfType(typeof(int)).And.BeGreaterThan(0);

            // cleanup
            sut.DeleteTask(actual.Id);
            sut.GetTaskOrNull(actual.Id).Should().BeNull();
        }

        [Fact]
        public void UpdateTask_UpdatesExistingEntry_WhenValid()
        {
            var app = _appFixture.AppWithManyTasksAndActivities;
            var tasks = app.GetAllTasks();
            var sut = tasks[new Random().Next(tasks.Count)];
            var expected = MockData.Slogan;

            sut.Rename(expected);
            var actual = app.UpdateTask(sut);
            var actualLocal = app.GetTaskOrNull(sut.Id);

            actual.Title.Should().Be(expected);
            actualLocal.Title.Should().Be(expected);
        }

        [Fact]
        public void CreateActivity_CreatesAndAttachesToTask_WhenValid()
        {
            var app = _appFixture.AppWithSingleTaskAndNoActivities;
            var descr = MockData.Slogan;
            var task = app.GetAllTasks().First();

            var sut = app.CreateActivity(descr, task.Id);

            sut.Id.Should().BePositive();
            app.GetActivity(sut.Id).Should().BeEquivalentTo(sut);
            // cleanup
            app.DeleteActivity(sut.Id);
            task.Activities.Should().BeEmpty();
        }
    }
}