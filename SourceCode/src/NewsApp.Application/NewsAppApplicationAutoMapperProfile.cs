using AutoMapper;
using NewsApp.AlertsSearches;
using NewsApp.Searches;
using NewsApp.Failures;
using NewsApp.Reads;
using NewsApp.Notifications;
using NewsApp.Articles;
using NewsApp.Themes;
using NewsApp.News;
using NewsApp.KeyWords;
using Volo.Abp.Identity;
using NewsApp.Users;

namespace NewsApp;

public class NewsAppApplicationAutoMapperProfile : Profile
{
    public NewsAppApplicationAutoMapperProfile()
    {
        //CreateMap<Alert, AlertDto>();
        //CreateMap<CreateUpdateAlertDto, Alert>();
        CreateMap<IdentityUser, UserDto>();

        CreateMap<KeyWord, KeyWordDto>().ReverseMap();

        CreateMap<AlertSearch, AlertSearchDto>().ReverseMap();
        CreateMap<CreateUpdateAlertSearchDto, AlertSearch>();

        CreateMap<ThemeDto, Theme>().ReverseMap();
        CreateMap<CreateUpdateThemeDto, Theme>();

        CreateMap<ArticleDto, Article>().ReverseMap();
        CreateMap<CreateUpdateArticleDto, Article>();
        CreateMap<CreateUpdateNewsDto, Article>();
        CreateMap<NewsDto, ArticleDto>().ReverseMap();

        CreateMap<Notification, ReadDto>().ReverseMap();

        CreateMap<Notification, NotificationDto>().ReverseMap();
        CreateMap<CreateUpdateNotificationDto, Notification>();

        CreateMap<Search, SearchDto>().ReverseMap();
        CreateMap<CreateUpdateSearchDto, Search>();

        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */


    }
}
