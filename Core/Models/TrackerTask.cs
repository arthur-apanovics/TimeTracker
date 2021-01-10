using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Core.Interfaces;

namespace Core.Models
{
    public class TrackerTask : ITrackerTask
    {
        public int Id { get; set; }

        [Required] [MaxLength(2048)] public string Title { get; set; }

        [ForeignKey("TaskId")]
        public virtual List<TrackerActivity> Activities { get; protected set; }
            = new();

        public TimeSpan TotalTime => Activities.Aggregate(TimeSpan.Zero,
            (tally, next) => tally + next.TimeSpent);

        public static TrackerTask Create(string title)
        {
            if (string.IsNullOrEmpty(title))
            {
                throw new ArgumentNullException(nameof(title));
            }

            return new TrackerTask()
            {
                Title = title
            };
        }

        public static List<TrackerTask> Create(
            IEnumerable<ITrackerTaskEntity> entities)
        {
            return entities.Select(Create).ToList();
        }

        public static TrackerTask Create(ITrackerTaskEntity entity)
        {
            return new()
            {
                Id = entity.Id,
                Title = entity.Title,
                Activities = TrackerActivity.Create(entity.Activities)
            };
        }

        public TrackerActivity GetActivity(string description)
        {
            description = description.ToLower();
            var activity = Activities.FirstOrDefault(a =>
                a.Description.ToLower() == description);
            if (activity == null)
            {
                throw new KeyNotFoundException(
                    $"No activity exists with description '{description}'");
            }

            return activity;
        }

        public TrackerActivity GetActivity(int id)
        {
            var activity = Activities.FirstOrDefault(a => a.Id == id);
            if (activity == null)
            {
                throw new KeyNotFoundException(
                    $"No activity exists with id {id}");
            }

            return activity;
        }

        public TrackerTask Rename(string newName)
        {
            if (string.IsNullOrEmpty(newName))
            {
                throw new ArgumentException("Task title cannot be empty",
                    nameof(newName));
            }

            Title = newName;

            return this;
        }

        public TrackerTask AddActivity(ITrackerActivity activity)
        {
            // TODO: VALIDATE
            Activities.Add((TrackerActivity) activity);

            return this;
        }

        public TrackerTask RemoveActivity(int id)
        {
            Activities.Remove(GetActivity(id));

            return this;
        }
    }
}