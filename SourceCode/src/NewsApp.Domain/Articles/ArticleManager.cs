using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.Internal.Mappers;
using NewsApp.News;
using NewsApp.Themes;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Identity;

namespace NewsApp.Articles
{
    public class ArticleManager : DomainService
    {
        private readonly IRepository<Theme, int> _themeRepository;
        public ArticleManager(IRepository<Theme, int> themeRepository)
        {
            _themeRepository = themeRepository;
        }

        public async Task<Article> CreateAsyncOrUpdate(NewsDto news, int? themeId)
        {
            Article article = null;

            // ObjectMapper.Map<NewsDto, ArticleDto>(news);

            article = new Article {};

            var theme = await _themeRepository.GetAsync(themeId.Value, includeDetails: true);
            theme.Articles.Add(article);

            return article;
        }
    }
}