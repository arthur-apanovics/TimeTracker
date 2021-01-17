using System;
using System.Linq;
using Core.API.Models;
using Core.Tests.Fixtures;
using Core.Tests.Mock;
using FluentAssertions;
using Xunit;

namespace Core.Tests.Tests
{
    public class TrackerRepositoryTests : IClassFixture<TrackerRepositoryFixture>,
        IClassFixture<TrackerTaskFixture>, IDisposable
    {
        private readonly TrackerRepositoryFixture _trackerRepositoryFixture;
        private readonly TrackerTaskFixture _taskFixture;

        public TrackerRepositoryTests(
            TrackerRepositoryFixture trackerRepositoryFixture,
            TrackerTaskFixture taskFixture
        )
        {
            _trackerRepositoryFixture = trackerRepositoryFixture;
            _taskFixture = taskFixture;
        }

        public void Dispose()
        {
            _trackerRepositoryFixture.Dispose();
        }

        [Fact]
        public void InsertTask_SavesToDatabase_WhenValid()
        {
            var sut = _trackerRepositoryFixture.EmptyTrackerRepository;
            var taskTitle = MockData.Buzzword;

            var task = new TrackerTask(null)
            {
                Title = taskTitle
            };
            var actual = sut.InsertTask(task);

            actual.Id.Should().BeGreaterThan(0);

            // cleanup
            sut.DeleteTask(actual.Id);
            sut.GetTaskOrNull(actual.Id).Should().BeNull();
            _trackerRepositoryFixture.Dispose();
        }

        [Fact]
        public void UpdateTask_UpdatesExistingEntry_WhenValid()
        {
            var repo = _trackerRepositoryFixture
                .TrackerRepositoryWithManyTasksAndActivities;
            var tasks = repo.GetAllTasks();
            var sut = tasks[new Random().Next(tasks.Count)];
            var expected = MockData.Slogan;

            sut.Rename(expected);
            var actual = repo.UpdateTask(sut);
            var actualLocal = repo.GetTaskOrNull(sut.Id);

            actual.Title.Should().Be(expected);
            actualLocal.Title.Should().Be(expected);
        }

        [Fact]
        public void CreateActivity_CreatesAndAttachesToTask_WhenValid()
        {
            var repo = _trackerRepositoryFixture
                .TrackerRepositoryWithSingleTaskAndNoActivities;
            var descr = MockData.Slogan;
            var task = repo.GetAllTasks().First();

            var sut = repo.CreateActivity(descr, task.Id);

            sut.Id.Should().BePositive();
            repo.GetActivity(sut.Id).Should().BeEquivalentTo(sut);
            // cleanup
            repo.DeleteActivity(sut.Id);
            repo.GetActivityOrNull(sut.Id).Should().BeNull();
        }
    }
}
