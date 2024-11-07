using Abp.Authorization.Users;
using System;
using System.ComponentModel.DataAnnotations;
using NewsApp.AlertsSearches;
using NewsApp.Failures;
using System.Collections.Generic;
using NewsApp.Articles;

namespace NewsApp.Searches
{
    public class CreateUpdateSearchDto
    {

        [Required]
        [StringLength(200)]
        public string SearchString { get; set; }
        [Required]
        public DateTime StartDateTime { get; set; }
        [Required]
        public int ResultsAmount { get; set; }
        [Required]
        public DateTime EndDateTime { get; set; }
        [Required]
        public FailureDto? Failure { get; set; }
        [Required]

        public AlertSearchDto? Alert { get; set; }
        [Required]

        public AbpUserBase User { get; set; }
        [Required]

        public ICollection<ArticleDto> Articles { get; set; }

    }
}