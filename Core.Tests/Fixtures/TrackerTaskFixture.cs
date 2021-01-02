using System;
using Core.Interfaces;
using Core.Models;

namespace Core.Tests.Fixtures
{
    public class TrackerTaskFixture : IDisposable
    {
        public ITrackerTask EmptyTask => GenerateTaskWithActivities(0);
        public ITrackerTask TaskWithOneActivity => GenerateTaskWithActivities(1);
        public ITrackerTask TaskWithManyActivities => GenerateTaskWithActivities(15);

        public void Dispose()
        {
            // clean up test records in database 
        }

        public static ITrackerTask GenerateTaskWithActivities(uint numOfActivities)
        {
            if (numOfActivities == 0)
            {
                return TrackerTask.Create("Empty task");
            }

            var task = TrackerTask.Create(MockData.Buzzword);
            var seed = new Random();
            var seedDate = DateTime.Now.Subtract(TimeSpan.FromDays(numOfActivities));
            var maxActDurationSeconds = TimeSpan.FromHours(8).Seconds;

            for (int i = 0; i < numOfActivities; i++)
            {
                var act = TrackerActivity.Create(MockData.Slogan);
                act.DateStart = seedDate;
                act.DateEnd = seedDate.Add(
                    TimeSpan.FromSeconds(seed.Next(60, maxActDurationSeconds))
                );
                task.AddActivity(act);

                // one activity per day
                seedDate = seedDate.Add(TimeSpan.FromDays(1));
            }

            return task;
        }
    }
}