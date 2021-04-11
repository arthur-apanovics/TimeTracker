using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using GUI.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GUI.Models
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
        private readonly DbContext _context;

        public TrackerActivity(DbContext context)
        {
            _context = context;
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
