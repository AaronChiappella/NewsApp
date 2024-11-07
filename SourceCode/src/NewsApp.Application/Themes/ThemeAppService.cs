using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using NewsApp.KeyWords;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;
using Volo.Abp.ObjectMapping;

namespace NewsApp.Themes
{
    [Authorize]
    public class ThemeAppService : NewsAppAppService, IThemeAppService
    {
        private readonly IRepository<Theme, int> _themeRepository;
        private readonly IRepository<KeyWord, int> _keyWordRepository;
        private readonly UserManager<Volo.Abp.Identity.IdentityUser> _userManager;
        private readonly ThemeManager _themeManager;

        public ThemeAppService(
            IRepository<Theme, int> themeRepository,
            IRepository<KeyWord, int> keyWordRepository, 
            UserManager<Volo.Abp.Identity.IdentityUser> userManager, 
            ThemeManager themeManager)
        {
            _themeRepository = themeRepository;
            _keyWordRepository = keyWordRepository;
            _userManager = userManager;
            _themeManager = themeManager;
        }

        public async Task<ICollection<ThemeDto>> GetThemesAsync()
        {
            var themes = await _themeRepository.GetListAsync(includeDetails: true);

            return ObjectMapper.Map<ICollection<Theme>, ICollection<ThemeDto>>(themes);
        }

        public async Task<ThemeDto> GetThemeAsync(int id)
        {
            var queryable = await _themeRepository.WithDetailsAsync(x => x.Themes);

            var query = queryable.Where(x => x.Id == id);

            var theme = await AsyncExecuter.FirstOrDefaultAsync(query);

            return ObjectMapper.Map<Theme, ThemeDto>(theme);

        }


        public async Task<ThemeDto> CreateUpdateThemeAsync(CreateUpdateThemeDto input)
        {
            var userGuid = CurrentUser.Id.GetValueOrDefault();

            var identityUser = await _userManager.FindByIdAsync(userGuid.ToString());

            var theme = await _themeManager.CreateAsyncOrUpdate(input.Id, input.Name, input.ParentThemeId, identityUser);

            if (input.Id is null)
            {
                theme = await _themeRepository.InsertAsync(theme, autoSave: true);
            }
            else
            {
                await _themeRepository.UpdateAsync(theme, autoSave: true);
            }

            return ObjectMapper.Map<Theme, ThemeDto>(theme);
        }

        public async Task<ICollection<KeyWordDto>> AddKeyWordsAsync(ICollection<string> newKeyWords, int themeId)
        {
            var theme = await _themeRepository.GetAsync(themeId);
            if (theme != null)
            {
                var keyWordsToAdd = new Collection<KeyWord>();

                foreach (var keyWord in newKeyWords)
                {
                    var keyWordToAdd = new KeyWord { Keyword = keyWord, Theme = theme };
                    keyWordToAdd = await _keyWordRepository.InsertAsync(keyWordToAdd, autoSave: true);
                    keyWordsToAdd.Add(keyWordToAdd);
                }
                theme.KeyWords.Concat(keyWordsToAdd);

                await _themeRepository.UpdateAsync(theme);

                return ObjectMapper.Map<ICollection<KeyWord>, ICollection<KeyWordDto>>(keyWordsToAdd);
            }
            else
            {
                throw new ArgumentException($"No se encontró un tema con id {themeId}.");
            }
        }

        public async Task DeleteThemeAsync(int themeId)
        {
            var theme = await _themeRepository.GetAsync(themeId);

            if (theme != null)
            {
                // Realizar lógica adicional antes de eliminar, si es necesario
                await _themeRepository.DeleteAsync(themeId);
            }
            else
            {
                throw new ArgumentException($"No se encontró un tema con id {themeId}.");
            }
        }
    }
}