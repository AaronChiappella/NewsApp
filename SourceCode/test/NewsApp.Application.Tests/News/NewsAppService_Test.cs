using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace NewsApp.News
{
    public class NewsAppService_Test : NewsAppApplicationTestBase
    {
        private readonly INewsAppService _newsAppService;

        public NewsAppService_Test()
        {
            _newsAppService = GetRequiredService<INewsAppService>();
        }

        [Fact]
        public async Task Should_Search_News()
        {
            //Arrange
            var query = "Apple";

            //Act
            var news = await _newsAppService.Search(query);

            //Assert
            news.ShouldNotBeNull();
            news.Count.ShouldBeGreaterThan(1);
        }
    }
}