using System;
using System.ComponentModel.DataAnnotations;
using Core.Interfaces;

namespace Core.Entities
{
    public class TrackerActivityEntity : ITrackerActivityEntity
    {
        public int Id { get; set; }

        [Required] 
        public string Description { get; set; }

        [Required] 
        public DateTime DateStart { get; set; }

        [Required] 
        public DateTime DateEnd { get; set; }
    }
}