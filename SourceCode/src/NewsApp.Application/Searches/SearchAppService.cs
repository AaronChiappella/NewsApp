using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Volo.Abp.Domain.Repositories;

namespace NewsApp.Searches
{
    public class SearchAppService : NewsAppAppService, ISearchAppService
    {
        private readonly IRepository<Search, int> _searchRepository;
        private readonly UserManager<Volo.Abp.Identity.IdentityUser> _userManager;
        public SearchAppService(IRepository<Search, int> searchRepository, UserManager<Volo.Abp.Identity.IdentityUser> userManager)
        {
            _searchRepository = searchRepository;
            _userManager = userManager;
        }

        public async Task<SearchDto> SaveSearchAsync(string query, DateTime start, DateTime end, int count)
        {
            var userGuid = CurrentUser.Id.GetValueOrDefault();

            var identityUser = await _userManager.FindByIdAsync(userGuid.ToString());

            var search = new Search
            {
                SearchString = query,
                StartDateTime = start,
                EndDateTime = end,
                ResultsAmount = count,
                User = identityUser
            };

            search = await _searchRepository.InsertAsync(search, autoSave: true);

            return ObjectMapper.Map<Search, SearchDto>(search);
        }
    }
}