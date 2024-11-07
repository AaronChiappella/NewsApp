using System;
using System.Threading.Tasks;
using NewsApp.AlertsSearches;
using NewsApp.Notifications;
using NewsApp.Searches;
using NewsApp.Themes;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;

namespace NewsApp;

public class NewsAppTestDataSeedContributor : IDataSeedContributor, ITransientDependency
{
    private readonly IRepository<Theme, int> _themeRepository;
    private readonly IRepository<Search, int> _searchRepository;
    private readonly IRepository<AlertSearch, int> _alertSearchRepository;
    private readonly IRepository<Notification, int> _notificationRepository;

    private readonly IdentityUserManager _identityUserManager;


    public NewsAppTestDataSeedContributor(
        IRepository<Theme, int> themeRepository,
        IRepository<Search, int> searchRepository,
        IRepository<AlertSearch, int> alertSearchRepository,
        IRepository<Notification, int> notificationRepository,
        IdentityUserManager identityUserManager)
    {
        _themeRepository = themeRepository;
        _searchRepository = searchRepository;
        _alertSearchRepository = alertSearchRepository;
        _notificationRepository = notificationRepository;
        _identityUserManager = identityUserManager;
    }

    public async Task SeedAsync(DataSeedContext context)
    {
        // Add User        
        IdentityUser identityUser = new IdentityUser(Guid.Parse("2e701e62-0953-4dd3-910b-dc6cc93ccb0d"), "admin", "admin@abp.io");
        await _identityUserManager.CreateAsync(identityUser, "1q2w3E*");
        // await _identityUserManager.AddToRoleAsync(identityUser, "Admin");

        // Add Themes
        await _themeRepository.InsertAsync(new Theme { Name = "Primer tema", User = identityUser });

        await _themeRepository.InsertAsync(new Theme { Name = "Segundo tema", User = identityUser });

        await _themeRepository.InsertAsync(new Theme { Name = "Tercer tema", User = identityUser });

        await _themeRepository.InsertAsync(new Theme { Name = "Cuarto tema", User = identityUser });

        // Add Searches

        Search search1 = await _searchRepository.InsertAsync(new Search
        {
            SearchString = "Cryptocurrencies",
            StartDateTime = DateTime.Now,
            EndDateTime = DateTime.Now.AddSeconds(3),
            ResultsAmount = 15,
            User = identityUser
        });

        Search search2 = await _searchRepository.InsertAsync(new Search
        {
            SearchString = "Christmas presents",
            StartDateTime = DateTime.Now,
            EndDateTime = DateTime.Now.AddSeconds(3),
            ResultsAmount = 20,
            User = identityUser
        });

        await _searchRepository.InsertAsync(new Search
        {
            SearchString = "Car crashes into a building",
            StartDateTime = DateTime.Now,
            EndDateTime = DateTime.Now.AddSeconds(3),
            ResultsAmount = 0,
            User = identityUser
        });

        // Add Alert
        AlertSearch alert1 = await _alertSearchRepository.InsertAsync(new AlertSearch
        {
            Search = search1,
            User = identityUser,
            AlertOfSearchId = 1,
            Active = true,
            CreatedDate = DateTime.Now.AddSeconds(20)
        }
        );

        AlertSearch alert2 = await _alertSearchRepository.InsertAsync(new AlertSearch
        {
            Search = search2,
            User = identityUser,
            AlertOfSearchId = 1,
            Active = true,
            CreatedDate = DateTime.Now.AddSeconds(20)
        }
);

        // Add Notifications
        await _notificationRepository.InsertAsync(new Notification
        {
            Active = true,
            Title = "Primera Notificacion",
            DateTime = DateTime.Now,
            Alert = alert1,
            User = identityUser
        }
        );

        await _notificationRepository.InsertAsync(new Notification
        {
            Active = true,
            Title = "Segunda Notificacion",
            DateTime = DateTime.Now,
            Alert = alert2,
            User = identityUser
        }
);
    }
}
