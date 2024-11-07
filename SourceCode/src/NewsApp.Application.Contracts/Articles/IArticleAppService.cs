using System;
using System.Threading.Tasks;
using NewsApp.News;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace NewsApp.Articles;

/* ARTICULOS PARA GUARDAR EN LA BD   */
public interface IArticleAppService : IApplicationService
{
    Task<ArticleDto> SaveArticleAsync(NewsDto input, int themeId);
}