using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using NewsApp.Articles;
using NewsApp.Themes;

namespace NewsApp.KeyWords
{
    public class KeyWord : Entity<int>
    {
        public string Keyword { get; set; }
        // relaciones
        public Theme Theme { get; set; }

        //constructor

        public KeyWord()
        {

        }
    }
}
