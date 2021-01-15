using Core.API.Data;
using Core.Models;
using GraphQL.Types;

namespace Core.API.GraphQL.Types
{
    public class TrackerTaskType : ObjectGraphType<TrackerTask>
    {
        public TrackerTaskType(TrackerRepository repository)
        {
            Name = nameof(TrackerTask);

            Field(t => t.Id).Description("Task id in database");

            Field(t => t.Title).Description("Task title");

            Field(t => t.TotalTime)
                .Description("Total time spent working on task");

            Field<ListGraphType<TrackerActivityType>>(
                "activities",
                "Activities in task",
                new QueryArguments(
                    new QueryArgument<IntGraphType>()
                    {
                        Name = "taskId"
                    }
                ),
                context => context.Source.Activities
            );
        }
    }
}