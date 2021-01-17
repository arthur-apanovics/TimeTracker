using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using API.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Models
{
    public class TrackerTask : ITrackerTask
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(2048)]
        public string Title { get; set; }

        [Required]
        [ForeignKey("TaskId")]
        public virtual List<TrackerActivity> Activities { get; protected set; }
            = new();

        public TimeSpan TotalTime =>
            Activities.Aggregate(
                TimeSpan.Zero,
                (tally, next) => tally + next.TimeSpent
            );

        private readonly DbContext _context;

        public TrackerTask(DbContext context)
        {
            _context = context;
        }

        public TrackerActivity GetActivity(string description)
        {
            description = description.ToLower();
            var activity = Activities.FirstOrDefault(
                a => a.Description.ToLower() == description
            );

            if (activity == null)
            {
                throw new KeyNotFoundException(
                    $"No activity exists with description '{description}'"
                );
            }

            return activity;
        }

        public TrackerActivity GetActivity(int id)
        {
            var activity = Activities.FirstOrDefault(a => a.Id == id);

            if (activity == null)
            {
                throw new KeyNotFoundException(
                    $"No activity exists with id {id}"
                );
            }

            return activity;
        }

        public TrackerActivity CreateActivity(string description)
        {
            if (string.IsNullOrEmpty(description))
            {
                throw new ArgumentNullException(nameof(description));
            }

            var activity = new TrackerActivity(_context)
            {
                Description = description,
                Task = this
            };

            Activities.Add(activity);
            _context.Add(activity);
            _context.SaveChanges();

            return activity;
        }

        public TrackerTask AddActivity(ITrackerActivity activity)
        {
            Activities.Add((TrackerActivity) activity);

            _context.SaveChanges();

            return this;
        }

        public TrackerTask DeleteActivity(int id)
        {
            Activities.Remove(GetActivity(id));

            _context.SaveChanges();

            return this;
        }

        public TrackerTask Rename(string newName)
        {
            if (string.IsNullOrEmpty(newName))
            {
                throw new ArgumentException(
                    "Task title cannot be empty",
                    nameof(newName)
                );
            }

            Title = newName;

            _context.SaveChanges();

            return this;
        }
    }
}
