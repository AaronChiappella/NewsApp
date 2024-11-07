using System;
using NewsApp.Failures;
using Volo.Abp.Application.Dtos;
using NewsApp.Users;
using NewsApp.AlertsSearches;

namespace NewsApp.Searches
{
    public class SearchDto : EntityDto<int>
    {
        public string SearchString {  get; set; }
        public int ResultsAmount { get; set; }
        public DateTime StartDateTime {  get; set; }
        public DateTime EndDateTime {  get; set; }

        // relaciones
        public FailureDto? Failure { get; set; }
        public AlertSearchDto? Alert { get; set; }
        public UserDto User { get; set; }
        // public ICollection<ArticleDto> Articles { get; set; }
    }
}
