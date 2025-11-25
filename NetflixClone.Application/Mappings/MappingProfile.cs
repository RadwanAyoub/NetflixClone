using AutoMapper;
using NetflixClone.Application.DTOs;
using NetflixClone.Domain.Entities;

namespace NetflixClone.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Content base mappings
            CreateMap<Content, ContentDto>()
                .ForMember(dest => dest.Genres, opt => 
                    opt.MapFrom(src => src.ContentGenres.Select(cg => cg.Genre.Name).ToList()))
                .ForMember(dest => dest.IsRecentlyReleased, opt => 
                    opt.MapFrom(src => src.ReleaseDate >= DateTime.UtcNow.AddMonths(-6)))
                .ForMember(dest => dest.ReleaseYear, opt => 
                    opt.MapFrom(src => src.ReleaseDate.Year.ToString()))
                .ForMember(dest => dest.FormattedDuration, opt => 
                    opt.MapFrom(src => $"{src.Duration / 60}h {src.Duration % 60}m"));

            // Movie mappings (inherit from Content mapping)
            CreateMap<Movie, MovieDto>()
                .IncludeBase<Content, ContentDto>()
                .ForMember(dest => dest.IsBlockbuster, opt => 
                    opt.MapFrom(src => src.Revenue > 100000000))
                .ForMember(dest => dest.Profit, opt => 
                    opt.MapFrom(src => src.Revenue - src.Budget))
                .ForMember(dest => dest.BudgetFormatted, opt => 
                    opt.MapFrom(src => src.Budget.ToString("C0")))
                .ForMember(dest => dest.RevenueFormatted, opt => 
                    opt.MapFrom(src => src.Revenue.ToString("C0")));

            // TVShow mappings (inherit from Content mapping)
            CreateMap<TVShow, TVShowDto>()
                .IncludeBase<Content, ContentDto>()
                .ForMember(dest => dest.Status, opt => 
                    opt.MapFrom(src => src.IsOngoing ? "Ongoing" : "Completed"))
                .ForMember(dest => dest.HasNewEpisodes, opt => 
                    opt.MapFrom(src => src.IsOngoing && src.LastAirDate >= DateTime.UtcNow.AddMonths(-3)));

            // Genre mappings
            CreateMap<Genre, GenreDto>()
                .ForMember(dest => dest.ContentCount, opt => 
                    opt.MapFrom(src => src.Contents.Count));

            // User mappings
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.FullName, opt => 
                    opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
                .ForMember(dest => dest.IsAdmin, opt => 
                    opt.MapFrom(src => src.Role == Domain.Enums.UserRole.Admin))
                .ForMember(dest => dest.Profiles, opt => 
                    opt.MapFrom(src => src.UserProfiles));

            // UserProfile mappings
            CreateMap<UserProfile, UserProfileDto>()
                .ForMember(dest => dest.DisplayName, opt => 
                    opt.MapFrom(src => $"{src.ProfileName} {(src.IsKidsProfile ? "👶" : "")}"));

            // Rating mappings
            CreateMap<Rating, RatingDto>()
                .ForMember(dest => dest.UserProfileName, opt => 
                    opt.MapFrom(src => src.UserProfile.ProfileName))
                .ForMember(dest => dest.ContentTitle, opt => 
                    opt.MapFrom(src => src.Content.Title))
                .ForMember(dest => dest.StarRating, opt => 
                    opt.MapFrom(src => new string('⭐', src.Score)))
                .ForMember(dest => dest.HasComment, opt => 
                    opt.MapFrom(src => !string.IsNullOrEmpty(src.Comment)));
        }
    }
}