using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Core.Interfaces;

namespace Core.Entities
{
    public class TrackerTaskEntity : ITrackerTaskEntity
    {
        public int Id { get; protected set; }
        
        [Required]
        [MaxLength(2048)]
        public string Title { get; protected set; }
        
        [ForeignKey("TaskId")]
        public virtual List<TrackerActivityEntity> Activities { get; protected set; } = new();
    }
}