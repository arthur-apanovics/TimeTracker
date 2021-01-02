using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Core.Entities;
using Core.Interfaces;

namespace Core.Models
{
    public class TrackerActivity : TrackerActivityEntity, ITrackerActivity
    {
        public TimeSpan TimeSpent => GetTimeSpent();

        private bool IsTracking => _stopwatch.IsRunning;
        private readonly Stopwatch _stopwatch = new();

        public static ITrackerActivity Create(string description)
        {
            if (string.IsNullOrEmpty(description))
            {
                throw new ArgumentNullException(nameof(description));
            }

            return new TrackerActivity
            {
                Description = description
            };
        }

        public static ITrackerActivity Create(TrackerActivityEntity entity)
        {
            return new TrackerActivity()
            {
                Id = entity.Id,
                Description = entity.Description,
                DateEnd = entity.DateEnd,
                DateStart = entity.DateStart
            };
        }

        public static IList<ITrackerActivity> Create(
            IEnumerable<TrackerActivityEntity> entities)
        {
            return entities.Select(Create).ToList();
        }

        public DateTime StartTracking()
        {
            _stopwatch.Start();
            DateStart = DateTime.Now;

            return DateStart;
        }

        public DateTime StopTracking()
        {
            _stopwatch.Stop();
            DateEnd = DateTime.Now;

            return DateEnd;
        }

        private TimeSpan GetTimeSpent()
        {
            if (_stopwatch.Elapsed > TimeSpan.Zero)
            {
                return _stopwatch.Elapsed;
            }

            if (DateStart > DateTime.MinValue && DateEnd > DateTime.MinValue)
            {
                return TimeSpan.FromTicks((DateEnd - DateStart).Ticks);
            }

            return TimeSpan.Zero;
        }
    }
}