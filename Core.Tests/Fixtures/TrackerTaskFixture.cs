using System;
using System.Collections.Generic;
using Core.API.Data;
using Core.API.Models;
using Core.Tests.Mock;
using Microsoft.EntityFrameworkCore;

namespace Core.Tests.Fixtures
{
    public class TrackerTaskFixture : IDisposable
    {
        public TrackerTask EmptyTask => GenerateTaskWithActivities(0);
        public TrackerTask TaskWithOneActivity => GenerateTaskWithActivities(1);

        public TrackerTask TaskWithManyActivities =>
            GenerateTaskWithActivities((uint) new Random().Next(5, 20));

        private readonly TrackerContext _context = new TrackerTestContext();
        private readonly List<TrackerTask> _spawnedTasks = new();

        public void Dispose()
        {
            foreach (var task in _spawnedTasks)
            {
                _context.Tasks.Remove(task);
            }

            _context.SaveChanges();
        }

        public TrackerTask GenerateTaskWithActivities(uint numOfActivities)
        {
            // var task = TrackerTask.Create(MockData.Buzzword);
            var task = new TrackerTask(_context)
            {
                Title = MockData.Buzzword
            };

            if (numOfActivities != 0)
            {
                var seed = new Random();
                var seedDate =
                    DateTime.Now.Subtract(TimeSpan.FromDays(numOfActivities));
                var maxActDurationSeconds =
                    (int) TimeSpan.FromHours(8).TotalSeconds;

                for (int i = 0; i < numOfActivities; i++)
                {
                    // var act = TrackerActivity.Create(MockData.Slogan);
                    var act = new TrackerActivity(_context)
                    {
                        DateStart = seedDate,
                        DateEnd = seedDate.Add(
                            TimeSpan.FromSeconds(
                                seed.Next(60, maxActDurationSeconds)
                            )
                        )
                    };

                    task.AddActivity(act);

                    // one activity per day
                    seedDate = seedDate.Add(TimeSpan.FromDays(1));
                }
            }

            _context.SaveChanges();
            _spawnedTasks.Add(task);

            return task;
        }
    }
}
