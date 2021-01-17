using System;
using API.Data;
using API.Models;
using API.Tests.Mock;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace API.Tests.Fixtures
{
    public class TrackerRepositoryFixture : IClassFixture<TrackerTaskFixture>,
        IDisposable
    {
        public TrackerRepository EmptyTrackerRepository =>
            GenerateTestRepository(0);

        public TrackerRepository
            TrackerRepositoryWithSingleTaskAndNoActivities =>
            GenerateTestRepository(1, 0);

        public TrackerRepository
            TrackerRepositoryWithSingleTaskAndSingleActivity =>
            GenerateTestRepository(1, 1);

        public TrackerRepository TrackerRepositoryWithManyTasksAndActivities =>
            GenerateTestRepository(12);

        private readonly TrackerTaskFixture _taskFixture;
        private readonly TrackerTestContext _context = new();

        //todo: fix DI for TrackerTaskFixture
        public TrackerRepositoryFixture(/*TrackerTaskFixture taskFixture*/)
        {
            _taskFixture = new TrackerTaskFixture();
        }

        public void Dispose()
        {
            _context.Tasks.FromSqlRaw($"DELETE FROM {nameof(_context.Tasks)}");
            _context.Activities.FromSqlRaw(
                $"DELETE FROM {nameof(_context.Activities)}"
            );
            _context.SaveChanges();
        }

        /// <param name="tasks">number of tasks in app</param>
        /// <param name="activities">number of activities in tasks; -1 = random</param>
        public TrackerRepository GenerateTestRepository(
            uint tasks,
            int activities = -1
        )
        {
            var user = TrackerUser.Create("John", "Doe", "j@d.com", "CHC");
            var context = new TrackerTestContext();
            var repository = new TrackerRepository(context);

            if (tasks == 0)
            {
                return repository;
            }

            var seed = new Random();
            for (var i = 0; i < tasks; i++)
            {
                var numAct = activities == -1 ? seed.Next(20) : activities;

                repository.InsertTask(
                    _taskFixture.GenerateTaskWithActivities(
                        (uint) numAct
                    )
                );
            }

            return repository;
        }
    }
}
