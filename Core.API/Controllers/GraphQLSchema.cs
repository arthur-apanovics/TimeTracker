using System;
using Core.API.GraphQL.Query;
using GraphQL.Types;
using GraphQL.Utilities;

namespace Core.API.Controllers
{
    public class GraphQlSchema : Schema
    {
        // private readonly ILogger _logger;
        
        // https://graphql-dotnet.github.io/docs/getting-started/dependency-injection/
        public GraphQlSchema(IServiceProvider services) : base(services)
        {
            // _logger = services.GetService(typeof(ILogger));

            Query = services.GetRequiredService<TrackerTaskQuery>();

            // Here’s where you can specify
            // Query,
            // Mutation and
            // Subscription
        }
    }
}