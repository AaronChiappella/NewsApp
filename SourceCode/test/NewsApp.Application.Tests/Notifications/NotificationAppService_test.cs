using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NewsApp.EntityFrameworkCore;
using NewsApp.News;
using Shouldly;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Uow;
using Xunit;

namespace NewsApp.Notifications
{
    public class NotificationAppService_Test : NewsAppApplicationTestBase
    {
        private readonly INotificationAppService _notificationAppService;
        private readonly IDbContextProvider<NewsAppDbContext> _dbContextProvider;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        public NotificationAppService_Test()
        {
            _notificationAppService = GetRequiredService<INotificationAppService>();
            _dbContextProvider = GetRequiredService<IDbContextProvider<NewsAppDbContext>>();
            _unitOfWorkManager = GetRequiredService<IUnitOfWorkManager>();
        }

        [Fact]
        public async Task Should_Create_Notification()
        {
            //Arrange            
            var input = new CreateUpdateNotificationDto
            {
                Title = "Microsoft goes bankrupt",
                DateTime = DateTime.Now,
                AlertId = 1,
            };

            //Act
            var notification = await _notificationAppService.CreateNotificationAsync(input);

            //assert
            // se verifican los datos devueltos por el servicio
            notification.ShouldNotBeNull();
            notification.Id.ShouldBePositive();
            // se verifican los datos persistidos por el servicio
            using (var uow = _unitOfWorkManager.Begin())
            {
                var dbcontext = await _dbContextProvider.GetDbContextAsync();
                dbcontext.Notifications.FirstOrDefault(t => t.Id == notification.Id).ShouldNotBeNull();
            }
        }


        [Fact]
        public async Task Should_Get_All_Notifications()
        {
            //Act
            var notifications = await _notificationAppService.GetNotificationsAsync();

            //Assert
            notifications.ShouldNotBeNull();
            notifications.Count.ShouldBeGreaterThan(1);
        }
    }
}
