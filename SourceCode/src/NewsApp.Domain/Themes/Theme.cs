using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;
using NewsApp.Articles;
using Volo.Abp.Identity;
using NewsApp.KeyWords;
using System.Collections.ObjectModel;

namespace NewsApp.Themes
{
    public class Theme : Entity<int>
    {
        public string Name { get; set; }

        // relaciones
        public ICollection<KeyWord>? KeyWords { get; set; } // palabras clave relacionadas al tema
        public ICollection<Theme>? Themes { get; set; } // Lista de temas guardados en este tema
        public ICollection<Article>? Articles { get; set; } // Lista de noticias guardadas en este tema
        public Theme? ParentTheme { get; set; } // Tema padre al cual este tema pertenece
        // public int? ParentThemeId { get; set; } // Tema padre al cual este tema pertenece
        // public AlertTheme? AlertTheme { get; set; }
        // public Guid UserId { get; set; }
        public IdentityUser User { get; set; }

        public Theme()
        {
            this.Themes = new List<Theme>();
            this.Articles = new List<Article>();
            this.KeyWords = new List<KeyWord>();
        }
    }
}

