using System;
using GUI.Data;
using GUI.ViewModels;
using Splat;

namespace GUI
{
    public static class Bootstrapper
    {
        public static void RegisterDependencies(
            IMutableDependencyResolver services,
            IReadonlyDependencyResolver resolver
        )
        {
            services.RegisterLazySingleton(() => new TrackerContext());
            services.RegisterLazySingleton(
                () => new TrackerRepository(
                    resolver.GetRequiredService<TrackerContext>()
                )
            );

            // view models
            services.Register(() => new TaskViewModel());
            services.Register(
                () => new TaskListViewModel(
                    resolver.GetRequiredService<TrackerRepository>()
                )
            );
            services.RegisterLazySingleton(
                () => new MainWindowViewModel(
                    resolver.GetRequiredService<TaskListViewModel>()
                )
            );
        }

        public static TService GetRequiredService<TService>(
            this IReadonlyDependencyResolver resolver
        )
        {
            var service = resolver.GetService<TService>();
            if (service is null) // Splat is not able to resolve type for us
            {
                // throw error with detailed description
                throw new InvalidOperationException(
                    $"Failed to resolve object of type {typeof(TService)}"
                );
            }

            return service; // return instance if not null
        }
    }
}
