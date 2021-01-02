using System;
using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Interfaces;

namespace Core.Models
{
    public class TrackerTask : TrackerTaskEntity, ITrackerTask
    {
        public new IList<ITrackerActivity> Activities { get; protected init; } = new List<ITrackerActivity>();

        public TimeSpan TotalTime => Activities.Aggregate(
            TimeSpan.Zero,
            (tally, next) => tally + next.TimeSpent
        );

        public static ITrackerTask Create(string title)
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

        public static IList<ITrackerTask> Create(IEnumerable<TrackerTaskEntity> entities)
        {
            return entities.Select(Create).ToList();
        }

        public static ITrackerTask Create(TrackerTaskEntity entity)
        {
            return new TrackerTask()
            {
                Id = entity.Id,
                Activities = TrackerActivity.Create(entity.Activities)
            };
        }

        public ITrackerActivity GetActivity(string description)
        {
            description = description.ToLower();
            return Activities.First(a => a.Description.ToLower() == description);
        }

        public ITrackerTask Rename(string newName)
        {
            throw new NotImplementedException();
        }

        public ITrackerTask AddActivity(ITrackerActivity trackerActivityEntity)
        {
            // TODO: VALIDATE
            Activities.Add(trackerActivityEntity);

            return this;
        }

        public ITrackerTask DeleteActivity(int id)
        {
            var toRemove = Activities.First(a => a.Id == id);
            Activities.Remove(toRemove);

            return this;
        }

        public ITrackerTask DeleteActivity(ITrackerActivity trackerActivityEntity)
        {
            Activities.Remove(trackerActivityEntity);

            return this;
        }

        public ITrackerTask TransferActivity(ITrackerActivity activityEntity, ITrackerTask targetTaskEntity)
        {
            throw new NotImplementedException();
        }
    }
}