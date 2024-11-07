using System;
using System.Linq;
using System.Threading.Tasks;
using NewsApp.EntityFrameworkCore;
using Shouldly;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Uow;
using Xunit;

namespace NewsApp.Searches
{
    public class SearchAppService_Test : NewsAppApplicationTestBase
    {
        private readonly ISearchAppService _searchAppService;
        private readonly IDbContextProvider<NewsAppDbContext> _dbContextProvider;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        public SearchAppService_Test()
        {
            _searchAppService = GetRequiredService<ISearchAppService>();
            _dbContextProvider = GetRequiredService<IDbContextProvider<NewsAppDbContext>>();
            _unitOfWorkManager = GetRequiredService<IUnitOfWorkManager>();
        }

        [Fact]
        public async Task Should_Save_Search()
        {
            //Arrange            
            var query = "Binance";
            var startTime = DateTime.Now;
            var endTime = DateTime.Now.AddSeconds(5);
            var countOfResults = 12;

            //Act
            var savedSearch = await _searchAppService.SaveSearchAsync(query, startTime, endTime, countOfResults);

            //Assert
            // Se verifican los datos devueltos por el servicio
            savedSearch.ShouldNotBeNull();
            savedSearch.Id.ShouldBePositive();
            // se verifican los datos persistidos por el servicio
            using (var uow = _unitOfWorkManager.Begin())
            {
                var dbContext = await _dbContextProvider.GetDbContextAsync();
                dbContext.Searches.FirstOrDefault(t => t.Id == savedSearch.Id).ShouldNotBeNull();
            }
        }
    }
}
