using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using NetflixClone.Application.Features.Content.Queries;
using NetflixClone.Application.Features.Genres.Queries;
using NetflixClone.Application.Features.Movies.Queries;
using NetflixClone.Application.Mappings;

namespace NetflixClone.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // Register MediatR
            services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            // register mediatR query handlers
            services.AddMediatR(m => m.RegisterServicesFromAssemblyContaining<GetTrendingMoviesQueryHandler>());
            services.AddMediatR(m => m.RegisterServicesFromAssemblyContaining<GetFeaturedMoviesQueryHandler>());
            services.AddMediatR(m => m.RegisterServicesFromAssemblyContaining<GetMovieByIdQueryHandler>());

            services.AddMediatR(m => m.RegisterServicesFromAssemblyContaining<GetAllGenresQueryHandler>());

            services.AddMediatR(m => m.RegisterServicesFromAssemblyContaining<GetContentByGenreQueryHandler>());
            services.AddMediatR(m => m.RegisterServicesFromAssemblyContaining<SearchContentQueryHandler>());

            // Register AutoMapper
            services.AddAutoMapper(typeof(MappingProfile).Assembly);

            return services;
        }
    }
}