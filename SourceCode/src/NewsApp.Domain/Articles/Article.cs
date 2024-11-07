using Volo.Abp.Domain.Entities;
using NewsAPI.Constants;
using NewsApp.Themes;
using NewsApp.Users;
using System;

namespace NewsApp.Articles
{
    public class Article : Entity<int>
    {
        public string Author { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string UrlToImage { get; set; }
        public DateTime? PublishedAt { get; set; }
        public string SourceName { get; set; }
        public string Content { get; set; }

        // public Languages? Language { get; set; }

        // relaciones
        public Theme Theme { get; set; } // Tema en el cual el articulo fue guardado

        // constructor
        public Article()
        {
            
        }
    }
}


