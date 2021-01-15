using Core.API.Controllers;
using Core.API.Data;
using Core.API.GraphQL.Query;
using Core.API.GraphQL.Types;
using GraphiQl;
using GraphQL;
using GraphQL.NewtonsoftJson;
using GraphQL.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Core.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            // services.AddSwaggerGen(
            //     c =>
            //     {
            //         c.SwaggerDoc(
            //             "v1",
            //             new OpenApiInfo {Title = "Core.API", Version = "v1"}
            //         );
            //     }
            // );

            services.AddScoped<TrackerRepository>();
            
            // graphql
            services.AddScoped<IDocumentExecuter, DocumentExecuter>();
            services.AddScoped<IDocumentWriter, DocumentWriter>();
            services.AddScoped<ISchema, GraphQlSchema>();

            services.AddScoped<TrackerTaskType>();
            services.AddScoped<TrackerTaskQuery>();
            // services.AddScoped<TrackerTaskService>();
            
            services.AddScoped<TrackerActivityType>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                
                // app.UseSwagger();
                // app.UseSwaggerUI(
                //     c => c.SwaggerEndpoint(
                //         "/swagger/v1/swagger.json",
                //         "Core.API v1"
                //     )
                // );

                app.UseGraphiQl("/graphql");
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}