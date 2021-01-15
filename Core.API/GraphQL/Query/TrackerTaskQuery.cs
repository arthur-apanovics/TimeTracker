using Core.API.Data;
using Core.API.GraphQL.Types;
using GraphQL;
using GraphQL.Types;

namespace Core.API.GraphQL.Query
{
    public class TrackerTaskQuery : ObjectGraphType
    {
        public TrackerTaskQuery(TrackerRepository repository)
        {
            var id = 0;
            
            Name = nameof(TrackerTaskQuery);

            Field<ListGraphType<TrackerTaskType>>(
                name: "tasks",
                resolve: c => repository.GetAllTasks()
            );

            Field<TrackerTaskType>(
                name: "task",
                arguments: new QueryArguments(
                    new QueryArgument<IntGraphType>
                    {
                        Name = "id"
                    }
                ),
                resolve: context =>
                {
                    id = context.GetArgument<int>("id");
                    return repository.GetTask(id);
                }
            );

            Field<ListGraphType<TrackerActivityType>>(
                name: "activities",
                arguments: new QueryArguments(
                    new QueryArgument<IntGraphType>
                    {
                        Name = "id"
                    }
                ),
                resolve: context => repository.GetTask(id).Activities
            );
        }
    }
}