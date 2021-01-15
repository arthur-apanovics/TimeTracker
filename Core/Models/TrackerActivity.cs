using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using Core.Interfaces;

namespace Core.Models
{
    public class TrackerActivity : ITrackerActivity
    {
        public int Id { get; set; }
        
        [Required]
        public TrackerTask Task { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime DateStart { get; set; }

        [Required]
        public DateTime DateEnd { get; set; }

        public TimeSpan TimeSpent => GetTimeSpent();

        private bool IsTracking => _stopwatch.IsRunning;
        private readonly Stopwatch _stopwatch = new();

        public static TrackerActivity Create(string description)
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

        public static TrackerActivity Create(ITrackerActivityEntity entity)
        {
            return new()
            {
                Id = entity.Id,
                Description = entity.Description,
                DateEnd = entity.DateEnd,
                DateStart = entity.DateStart
            };
        }

        public static List<TrackerActivity> Create(
            IEnumerable<ITrackerActivityEntity> entities
        )
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