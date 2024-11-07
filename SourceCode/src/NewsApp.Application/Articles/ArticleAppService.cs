using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using NewsApp.News;
using NewsApp.Themes;
using Volo.Abp.Domain.Repositories;

namespace NewsApp.Articles;
public class ArticleAppService : NewsAppAppService, IArticleAppService
{
    private readonly IRepository<Article, int> _articleRepository;
    private readonly IRepository<Theme, int> _themeRepository;
    private readonly UserManager<Volo.Abp.Identity.IdentityUser> _userManager;


    public ArticleAppService(IRepository<Article, int> articleRepository, UserManager<Volo.Abp.Identity.IdentityUser> userManager, IRepository<Theme, int> themeRepository)
    {
        _articleRepository = articleRepository;
        _userManager = userManager;
        _themeRepository = themeRepository;
    }

    public async Task<ArticleDto> SaveArticleAsync(NewsDto input, int themeId)
    {
        var theme = await _themeRepository.GetAsync(themeId);

        var article = new Article 
        {
            Author = input.Author,
            Title = input.Title,
            Description = input.Description,
            Url = input.Url,
            UrlToImage = input.UrlToImage,
            PublishedAt = input.PublishedAt,
            SourceName = input.SourceName,
            Content = input.Content,
            // Language = input.Language,
            Theme = theme            
        };

        article = await _articleRepository.InsertAsync(article, autoSave: true);

        theme.Articles.Add(article);

        await _themeRepository.UpdateAsync(theme);

        return ObjectMapper.Map<Article, ArticleDto>(article);
    }
}