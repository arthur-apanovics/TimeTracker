using System;
using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Interfaces;
using Core.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Core
{
    public class App
    {
        public IAppUser AppUser { get; set; } // todo: validate
        private readonly AppContext _ctx;

        public App()
        {
            _ctx = AppContext.Instance;
            // LoadConfiguration();
            // User = user;
        }

        // TASK

        public ITrackerTask GetTaskOrNull(int id)
        {
            var task = _ctx.Tasks.FirstOrDefault(t => t.Id == id);

            return task != null ? TrackerTask.Create(task) : null;
        }

        public ITrackerTask GetTaskOrNull(string title)
        {
            title = title.ToLower();
            var task = _ctx.Tasks.FirstOrDefault(t => t.Title.ToLower() == title);

            return task != null ? TrackerTask.Create(task) : null;
        }git 

        public ITrackerTask InsertTask(ITrackerTask task)
        {
            _ctx.Tasks.Add((task as TrackerTaskEntity)!);
            _ctx.SaveChanges();

            return task;
        }

        public ITrackerTask UpdateTask(ITrackerTask task)
        {
            throw new NotImplementedException();
        }

        public void DeleteTask(int id)
        {
            var task = _ctx.Tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
            {
                // todo: log non-existent task deletion attempt
                return;
            }
            
            _ctx.Tasks.Remove(task);
            _ctx.SaveChanges();
        }

        // ACTIVITY

        public ITrackerActivity GetActivity(int id)
        {
            throw new NotImplementedException();
        }

        public ITrackerActivity CreateActivity()
        {
            throw new NotImplementedException();
        }

        public ITrackerActivity UpdateActivity(ITrackerTask task)
        {
            throw new NotImplementedException();
        }

        public void DeleteActivity(int id)
        {
            throw new NotImplementedException();
        }

        // PRESENTATION

        public Dictionary<ITrackerTask, TimeSpan> GetTaskTotalReport()
        {
            throw new NotImplementedException();
        }
    }
}