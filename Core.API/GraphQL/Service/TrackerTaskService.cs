using System.Collections.Generic;
using Core.API.Data;
using Core.API.Models;
using Core.API.Models.Interfaces;

namespace Core.API.GraphQL.Service
{
    public class TrackerTaskService
    {
        private readonly TrackerRepository _trackerRepository;

        public TrackerTaskService(TrackerRepository trackerRepository)
        {
            _trackerRepository = trackerRepository;
        }

        public IList<TrackerTask> GetAllTasks()
        {
            return _trackerRepository.GetAllTasks();
        }

        public ITrackerTask GetTaskById(int id)
        {
            return _trackerRepository.GetTask(id);
        }

        public IList<TrackerActivity> GetActivitiesForTask(int id)
        {
            return _trackerRepository.GetTask(id).Activities;
        }
    }
}
