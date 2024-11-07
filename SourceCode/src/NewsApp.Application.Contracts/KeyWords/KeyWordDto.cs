using System;
using System.Collections.Generic;
using System.Text;
using NewsApp.Themes;
using Volo.Abp.Application.Dtos;

namespace NewsApp.KeyWords
{
    public class KeyWordDto : EntityDto<int>
    {
        public string Keyword { get; set; }
        public ThemeDto Theme { get; set; }
        //public KeyWordDto(string keyword)
        //{
        //    Keyword = keyword;
        //}
    }
}
