using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace NewsApp.KeyWords
{
    public class KeyWordAppService : NewsAppAppService, IKeyWordAppService
    {
        private readonly IRepository<KeyWord, int> _keyWordrepository;

        public KeyWordAppService(IRepository<KeyWord, int> keyWordrepository)
        {
            _keyWordrepository = keyWordrepository;
        }

        public async Task<ICollection<KeyWordDto>> GetKeywordsAsync(int themeId)
        {
            var keywords = await _keyWordrepository
                .GetListAsync(keyWord => keyWord.Theme.Id == themeId);

            return ObjectMapper.Map<ICollection<KeyWord>, ICollection<KeyWordDto>>(keywords);
        }

    }
}