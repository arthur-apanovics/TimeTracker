using System;
using System.Collections.Generic;
using Core.API.Data;
using Core.Models;
using Core.Tests.Mock;
using Microsoft.EntityFrameworkCore;

namespace Core.Tests.Fixtures
{
    public class TrackerRepositoryFixture : IDisposable
    {
        public TrackerRepository EmptyTrackerRepository => GenerateTestApp(0);
        public TrackerRepository TrackerRepositoryWithSingleTaskAndNoActivities => GenerateTestApp(1, 0);
        public TrackerRepository TrackerRepositoryWithSingleTaskAndSingleActivity => GenerateTestApp(1, 1);
        public TrackerRepository TrackerRepositoryWithManyTasksAndActivities => GenerateTestApp(12);

        private readonly TrackerTestContext _context = new();
        private readonly List<TrackerRepository> _spawnedRepositories = new();

        /// <param name="tasks">number of tasks in app</param>
        /// <param name="activities">number of activities in tasks; -1 = random</param>
        public TrackerRepository GenerateTestApp(uint tasks, int activities = -1)
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
                var numAct = activities == -1
                    ? seed.Next(20)
                    : activities;

                repository.InsertTask(TrackerTaskFixture.GenerateTaskWithActivities((uint) numAct));
            }

            _spawnedRepositories.Add(repository);

            return repository;
        }

        public void Dispose()
        {
            _context.Tasks.FromSqlRaw($"DELETE FROM {nameof(_context.Tasks)}");
            _context.Activities.FromSqlRaw($"DELETE FROM {nameof(_context.Activities)}");
            _context.SaveChanges();
        }
    }
}