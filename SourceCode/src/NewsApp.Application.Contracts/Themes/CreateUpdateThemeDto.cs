using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using NewsApp.KeyWords;

namespace NewsApp.Themes
{
    public class CreateUpdateThemeDto
    {
        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        public int? Id { get; set; }

        public int? ParentThemeId { get; set; }

        // public List<KeyWordDto>? KeyWords { get; set; }


    }
}
