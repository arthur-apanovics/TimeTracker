using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using Core.Interfaces;

namespace Core.Entities
{
    public class TrackerActivityEntity : ITrackerActivityEntity
    {
        public int Id { get; protected set; }

        [Required] 
        public string Description { get; protected set; }

        [Required] 
        public DateTime DateStart { get; protected set; }

        [Required] 
        public DateTime DateEnd { get; protected set; }
    }
}