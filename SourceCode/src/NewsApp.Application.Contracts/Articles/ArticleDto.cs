using NewsAPI.Constants;
using NewsApp.Themes;
using System;
using Volo.Abp.Application.Dtos;

namespace NewsApp.Articles
{
    public class ArticleDto : EntityDto<int>
    {
        public string Author { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string? UrlToImage { get; set; }
        public DateTime PublishedAt { get; set; }
        public string Content { get; set; }

        public string SourceName { get; set; }

        public Languages? Language { get; set; }

        // relaciones
        public ThemeDto Theme { get; set; } // Tema en el cual el articulo fue guardado

    }
}
