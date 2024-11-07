using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace NewsApp.News
{
    public class NewsApiService_Test
    {
        private readonly NewsApiService _newsApiService;

        public NewsApiService_Test()
        {
            // instancio la clase concreta ya que quiero probar explicitamente la misma.
            _newsApiService = new NewsApiService();
        }

        [Fact]
        public async Task Should_Get_All_News()
        {
            //Arrage            
            var query = "Apple";

            //Act
            var articles = await _newsApiService.GetNewsAsync(query);

            //Assert
            articles.ShouldNotBeNull();
            articles.Count.ShouldBeGreaterThan(1);
        }
    }
}