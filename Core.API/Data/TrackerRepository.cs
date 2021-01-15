using System;
using System.Collections.Generic;
using System.Linq;
using Core.Interfaces;
using Core.Models;

namespace Core.API.Data
{
    public class TrackerRepository
    {
        public ITrackerUser TrackerUser { get; set; } // todo: validate
        private readonly TrackerContext _ctx;

        public TrackerRepository(TrackerContext context)
        {
            _ctx = context;
        }

        // TASK

        public int GetTaskCount()
        {
            return _ctx.Tasks.Local.Count;
        }

        public IList<TrackerTask> GetAllTasks()
        {
            return TrackerTask.Create(_ctx.Tasks);
        }

        public ITrackerTask GetTaskOrNull(int id)
        {
            return _ctx.Tasks.FirstOrDefault(t => t.Id == id);
        }

        public ITrackerTask GetTaskOrNull(string title)
        {
            title = title.ToLower();
            return _ctx.Tasks.FirstOrDefault(t => t.Title.ToLower() == title);
        }

        public ITrackerTask GetTask(int id)
        {
            var task = _ctx.Tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
            {
                throw new KeyNotFoundException(
                    $"No task exists with id {task.Id}"
                );
            }

            return task;
        }

        public ITrackerTask InsertTask(ITrackerTask task)
        {
            _ctx.Tasks.Add((TrackerTask) task);
            _ctx.SaveChanges();

            return task;
        }

        public ITrackerTask UpdateTask(ITrackerTask task)
        {
            var taskEntity = GetTask(task.Id);
            _ctx.Entry(taskEntity).CurrentValues.SetValues(task);
            _ctx.SaveChanges();

            return task;
        }

        public void DeleteTask(int id)
        {
            var task = GetTask(id);

            foreach (var activity in task.Activities)
            {
                DeleteActivity(activity.Id);
            }

            _ctx.Tasks.Remove((TrackerTask) task);
            _ctx.SaveChanges();
        }

        // ACTIVITY

        public ITrackerActivity GetActivity(int id)
        {
            var activity = _ctx.Activities.FirstOrDefault(a => a.Id == id);
            if (activity == null)
            {
                throw new KeyNotFoundException(
                    $"No activity exists with id {id}"
                );
            }

            return activity;
        }

        public ITrackerActivity GetActivityOrNull(int id)
        {
            return _ctx.Activities.FirstOrDefault(a => a.Id == id);
        }

        public ITrackerActivity CreateActivity(string description, int taskId)
        {
            var task = GetTask(taskId);
            var activity = TrackerActivity.Create(description);
            task.AddActivity(activity);
            _ctx.SaveChanges();

            return activity;
        }

        public ITrackerActivity UpdateActivity(ITrackerTask task)
        {
            throw new NotImplementedException();
        }

        public void DeleteActivity(int id)
        {
            var activity = GetActivity(id);
            var task = _ctx.Tasks.First(
                t => t.Activities.Contains((TrackerActivity) activity)
            );

            task.RemoveActivity(activity.Id);
            _ctx.Activities.Remove((TrackerActivity) activity);

            _ctx.SaveChanges();
        }

        // PRESENTATION

        public Dictionary<ITrackerTask, TimeSpan> GetTaskTotalReport()
        {
            throw new NotImplementedException();
        }
    }
}