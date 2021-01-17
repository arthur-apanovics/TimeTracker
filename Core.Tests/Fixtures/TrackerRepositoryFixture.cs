using System;
using System.Collections.Generic;
using Core.API.Data;
using Core.API.Models;
using Core.Tests.Mock;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Core.Tests.Fixtures
{
    public class TrackerRepositoryFixture : IClassFixture<TrackerTaskFixture>,
        IDisposable
    {
        public TrackerRepository EmptyTrackerRepository => GenerateTestApp(0);

        public TrackerRepository
            TrackerRepositoryWithSingleTaskAndNoActivities =>
            GenerateTestApp(1, 0);

        public TrackerRepository
            TrackerRepositoryWithSingleTaskAndSingleActivity =>
            GenerateTestApp(1, 1);

        public TrackerRepository TrackerRepositoryWithManyTasksAndActivities =>
            GenerateTestApp(12);

        private readonly TrackerTaskFixture _trackerTaskFixture;
        private readonly TrackerTestContext _context = new();

        public TrackerRepositoryFixture(TrackerTaskFixture _trackerTaskFixture)
        {
            this._trackerTaskFixture = _trackerTaskFixture;
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
        public TrackerRepository GenerateTestApp(
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
                    _trackerTaskFixture.GenerateTaskWithActivities((uint) numAct)
                );
            }

            return repository;
        }
    }
}
