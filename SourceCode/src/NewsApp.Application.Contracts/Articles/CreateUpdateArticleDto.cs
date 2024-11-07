using NewsAPI.Constants;
using NewsApp.Themes;
using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text;


namespace NewsApp.Articles
{
    public class CreateUpdateArticleDto
    {
        public int? Id { get; set; }

        [Required]
        public int ThemeId { get; set; } // Tema en el cual el articulo fue guardado

        [Required]
        [StringLength(100)]
        public string Author { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Url { get; set; }

        [Required]
        public string? UrlToImage { get; set; }

        [Required]
        public Languages? Language { get; set; }

        [Required]
        public DateTime PublishedAt { get; set; }

        [Required]
        public string Content { get; set; }
        public string SourceName { get; set; }
    }
}
