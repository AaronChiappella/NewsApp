using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using NewsApp.EntityFrameworkCore;
using NewsApp.News;
using Shouldly;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Uow;
using Xunit;

namespace NewsApp.Articles
{
    public class ArticleAppService_Test : NewsAppApplicationTestBase
    {
        private readonly IArticleAppService _articleAppService;
        private readonly IDbContextProvider<NewsAppDbContext> _dbContextProvider;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        public ArticleAppService_Test()
        {
            _articleAppService = GetRequiredService<IArticleAppService>();
            _dbContextProvider = GetRequiredService<IDbContextProvider<NewsAppDbContext>>();
            _unitOfWorkManager = GetRequiredService<IUnitOfWorkManager>();
        }

        [Fact]
        public async Task Should_Save_Article_In_Theme()
        {
            //Arrange            
            var input = new NewsDto
            {
                Author = "Mariella Moon",
                Title = "X lawsuit accuses Media Matters of running a campaign to drive advertisers away from its website",
                Description = "X has filed a lawsuit against media watchdog group Media Matters over the latter's research that showed ads on the social network appearing next to antisemitic content. The company's owner, Elon Musk, promised to file a \"thermonuclear lawsuit\" against the org…",
                Url = "https://www.engadget.com/x-lawsuit-accuses-media-matters-of-running-a-campaign-to-drive-advertisers-away-from-its-website-040022933.html",
                UrlToImage = "https://s.yimg.com/ny/api/res/1.2/M_7MuCaINiQFhiJYcaRS5w--/YXBwaWQ9aGlnaGxhbmRlcjt3PTEyMDA7aD04MDA-/https://s.yimg.com/os/creatr-uploaded-images/2023-11/682318e0-8817-11ee-b1be-ebc263c188d2",
                PublishedAt = DateTime.Now,
                Content = "X has filed a lawsuit against media watchdog group Media Matters over the latter's research that showed ads on the social network appearing next to antisemitic content. The company's owner",
                SourceName = "Engadget",
                // Language = NewsAPI.Constants.Languages.EN
            };

            //Act
            var savedArticle = await _articleAppService.SaveArticleAsync(input, 1);

            //Assert
            // Se verifican los datos devueltos por el servicio
            savedArticle.ShouldNotBeNull();
            savedArticle.Id.ShouldBePositive();
            // se verifican los datos persistidos por el servicio
            using (var uow = _unitOfWorkManager.Begin())
            {
                var dbContext = await _dbContextProvider.GetDbContextAsync();
                dbContext.Articles.FirstOrDefault(t => t.Id == savedArticle.Id).ShouldNotBeNull();
                dbContext.Articles.FirstOrDefault(t => t.Theme.Id == 1).ShouldNotBeNull();
            }
        }
    }
}
