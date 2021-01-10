using System;
using System.Collections.Generic;
using Core.Models;

namespace Core.Tests.Fixtures
{
    public class AppFixture : IDisposable
    {
        public App EmptyApp => GenerateTestApp(0);
        public App AppWithSingleTaskAndNoActivities => GenerateTestApp(1, 0);
        public App AppWithSingleTaskAndSingleActivity => GenerateTestApp(1, 1);
        public App AppWithManyTasksAndActivities => GenerateTestApp(12);

        private readonly List<App> _spawnedApps = new();

        /// <param name="tasks">number of tasks in app</param>
        /// <param name="activities">number of activities in tasks; -1 = random</param>
        public App GenerateTestApp(uint tasks, int activities = -1)
        {
            var user = AppUser.Create("John", "Doe", "j@d.com", "CHC");
            var app = new App() {AppUser = user};

            if (tasks == 0)
            {
                return app;
            }

            var seed = new Random();
            for (var i = 0; i < tasks; i++)
            {
                var numAct = activities == -1
                    ? seed.Next(20)
                    : activities;

                app.InsertTask(TrackerTaskFixture.GenerateTaskWithActivities((uint) numAct));
            }

            _spawnedApps.Add(app);

            return app;
        }

        public void Dispose()
        {
            foreach (var app in _spawnedApps)
            {
                foreach (var task in app.GetAllTasks())
                {
                    app.DeleteTask(task.Id);
                }
            }
        }
    }
}