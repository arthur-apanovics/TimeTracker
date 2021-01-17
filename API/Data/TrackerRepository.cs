using System;
using System.Collections.Generic;
using System.Linq;
using API.Models;
using API.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class TrackerRepository
    {
        public ITrackerUser TrackerUser { get; set; } // todo: validate
        private readonly TrackerContext _context;

        public TrackerRepository(TrackerContext context)
        {
            _context = context;
        }

        // TASK

        public int GetTaskCount()
        {
            return _context.Tasks.Local.Count;
        }

        public IList<TrackerTask> GetAllTasks()
        {
            return _context.Tasks.Include(t => t.Activities).ToList();
        }

        public TrackerTask GetTaskOrNull(int id)
        {
            return _context.Tasks.FirstOrDefault(t => t.Id == id);
        }

        public TrackerTask GetTaskOrNull(string title)
        {
            title = title.ToLower();
            return _context.Tasks.FirstOrDefault(t => t.Title.ToLower() == title);
        }

        public TrackerTask GetTask(int id)
        {
            var task = _context.Tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
            {
                throw new KeyNotFoundException(
                    $"No task exists with id {task.Id}"
                );
            }

            return task;
        }

        public TrackerTask InsertTask(TrackerTask task)
        {
            _context.Tasks.Add(task);
            _context.SaveChanges();

            return task;
        }

        public TrackerTask UpdateTask(TrackerTask task)
        {
            var taskEntity = GetTask(task.Id);
            _context.Entry(taskEntity).CurrentValues.SetValues(task);
            _context.SaveChanges();

            return task;
        }

        public void DeleteTask(int id)
        {
            var task = GetTask(id);

            foreach (var activity in task.Activities)
            {
                DeleteActivity(activity.Id);
            }

            _context.Tasks.Remove(task);
            _context.SaveChanges();
        }

        // ACTIVITY

        public ITrackerActivity GetActivity(int id)
        {
            var activity = _context.Activities.FirstOrDefault(a => a.Id == id);
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
            return _context.Activities.FirstOrDefault(a => a.Id == id);
        }

        public ITrackerActivity CreateActivity(string description, int taskId)
        {
            var task = GetTask(taskId);
            var activity = new TrackerActivity(_context)
            {
                Description = description,
                Task = GetTask(taskId)
            };

            task.AddActivity(activity);
            _context.SaveChanges();

            return activity;
        }

        public ITrackerActivity UpdateActivity(TrackerTask task)
        {
            throw new NotImplementedException();
        }

        public void DeleteActivity(int id)
        {
            var activity = GetActivity(id);
            var task = _context.Tasks.First(
                t => t.Activities.Contains((TrackerActivity) activity)
            );

            task.DeleteActivity(activity.Id);
            _context.Activities.Remove((TrackerActivity) activity);

            _context.SaveChanges();
        }

        // PRESENTATION

        public Dictionary<TrackerTask, TimeSpan> GetTaskTotalReport()
        {
            throw new NotImplementedException();
        }
    }
}
