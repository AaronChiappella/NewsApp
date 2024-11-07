using System.Linq;
using System.Threading.Tasks;
using NewsApp.EntityFrameworkCore;
using Shouldly;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Uow;
using Xunit;

namespace NewsApp.AlertsSearches
{
    public class AlertSearchAppService_Test : NewsAppApplicationTestBase
    {
        private readonly IAlertSearchAppService _alertSearchAppService;
        private readonly IDbContextProvider<NewsAppDbContext> _dbContextProvider;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public AlertSearchAppService_Test()
        {
            _alertSearchAppService = GetRequiredService<IAlertSearchAppService>();
            _dbContextProvider = GetRequiredService<IDbContextProvider<NewsAppDbContext>>();
            _unitOfWorkManager = GetRequiredService<IUnitOfWorkManager>();
        }

        [Fact]
        public async Task Should_Create_AlertSearch()
        {
            //Arrange
            var searchId = 3;

            //Act
            var alertSearch = await _alertSearchAppService.CreateAlertAsync(searchId);

            //Assert
            alertSearch.ShouldNotBeNull();
            alertSearch.Search.Id.ShouldBe(searchId);

            // se verifican los datos persistidos por el servicio
            using (var uow = _unitOfWorkManager.Begin())
            {
                var dbContext = await _dbContextProvider.GetDbContextAsync();
                dbContext.AlertsSearches.FirstOrDefault(a => a.Id == alertSearch.Id).ShouldNotBeNull();
                dbContext.AlertsSearches.FirstOrDefault(a => a.Search.Id == 3).ShouldNotBeNull();
            }
        }

        [Fact]
        public async Task Should_Notify_User_Of_New_Results()
        {
            //Arrange

            //Act
            var notificationDtos = await _alertSearchAppService.NotifyUserOfNewResultsAsync();

            //Assert
            notificationDtos.ShouldNotBeNull();
        }
    }
}
