using System;
using Abp.Authorization.Users;
using NewsApp.Articles;
using Volo.Abp.Application.Dtos;

namespace NewsApp.Reads
{
    public class ReadDto : EntityDto<int>
    {
        public DateTime ReadDateTime { get; set; }
        public bool Liked { get; set; }
        public AbpUserBase User { get; set; }
        public ArticleDto Article { get; set; }
    }
}
