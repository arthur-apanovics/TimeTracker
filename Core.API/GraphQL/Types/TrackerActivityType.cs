using Core.Models;
using GraphQL.Types;

namespace Core.API.GraphQL.Types
{
    public class TrackerActivityType : ObjectGraphType<TrackerActivity>
    {
        public TrackerActivityType()
        {
            Name = nameof(TrackerActivity);
            
            Field(a => a.Id)
                .Description("Activity id in database");
            
            Field(a => a.Description)
                .Description("Activity description");
            
            Field(a => a.DateStart)
                .Description("Activity started date");
            
            Field(a => a.DateEnd)
                .Description("Activity ended date");
            
            Field(a => a.TimeSpent)
                .Description("Time spent on activity");
        }
    }
}