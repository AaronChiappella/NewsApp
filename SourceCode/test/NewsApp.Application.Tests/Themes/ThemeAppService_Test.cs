using NewsApp.EntityFrameworkCore;
using NewsApp.Themes;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Uow;
using Xunit;


namespace NewsApp.Themes
{
    public class ThemeAppService_Test : NewsAppApplicationTestBase
    {
        private readonly IThemeAppService _themeAppService;
        private readonly IDbContextProvider<NewsAppDbContext> _dbContextProvider;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public ThemeAppService_Test()
        {
            _themeAppService = GetRequiredService<IThemeAppService>();
            _dbContextProvider = GetRequiredService<IDbContextProvider<NewsAppDbContext>>();
            _unitOfWorkManager = GetRequiredService<IUnitOfWorkManager>();
        }

        [Fact]
        public async Task Should_Get_All_Themes()
        {

            //Act
            var themes = await _themeAppService.GetThemesAsync();

            //Assert
            themes.ShouldNotBeNull();
            themes.Count.ShouldBeGreaterThan(1);
        }

        [Fact]
        public async Task Should_Create_Theme()
        {
            //Arrange            
            var input = new CreateUpdateThemeDto { Name = "nuevo tema" };

            //Act
            var newTheme = await _themeAppService.CreateUpdateThemeAsync(input);

            //Assert
            // Se verifican los datos devueltos por el servicio
            newTheme.ShouldNotBeNull();
            newTheme.Id.ShouldBePositive();
            // se verifican los datos persistidos por el servicio
            using (var uow = _unitOfWorkManager.Begin())
            {
                var dbContext = await _dbContextProvider.GetDbContextAsync();
                dbContext.Themes.FirstOrDefault(t => t.Id == newTheme.Id).ShouldNotBeNull();
                dbContext.Themes.FirstOrDefault(t => t.Id == newTheme.Id).Name.ShouldBe(input.Name);
            }
        }

        [Fact]
        public async Task Should_Update_Theme()
        {
            //Arrange            
            var input = new CreateUpdateThemeDto { Name = "nuevo tema", Id = 1 };

            //Act
            var newTheme = await _themeAppService.CreateUpdateThemeAsync(input);

            //Assert
            // Se verifican los datos devueltos por el servicio
            newTheme.ShouldNotBeNull();
            newTheme.Id.ShouldBePositive();
            // se verifican los datos persistidos por el servicio
            using (var uow = _unitOfWorkManager.Begin())
            {
                var dbContext = await _dbContextProvider.GetDbContextAsync();
                dbContext.Themes.FirstOrDefault(t => t.Id == newTheme.Id).ShouldNotBeNull();
                dbContext.Themes.FirstOrDefault(t => t.Id == newTheme.Id).Name.ShouldBe(input.Name);
            }
        }

        [Fact]
        public async Task Should_Create_Child_Theme()
        {
            //Arrange            
            var input = new CreateUpdateThemeDto { Name = "nuevo tema hijo", ParentThemeId = 1 };

            //Act
            var newTheme = await _themeAppService.CreateUpdateThemeAsync(input);

            //Assert
            // Se verifican los datos devueltos por el servicio
            newTheme.ShouldNotBeNull();
            newTheme.Id.ShouldBePositive();
            // se verifican los datos persistidos por el servicio
            using (var uow = _unitOfWorkManager.Begin())
            {
                var dbContext = await _dbContextProvider.GetDbContextAsync();
                dbContext.Themes.FirstOrDefault(t => t.Id == newTheme.Id).ShouldNotBeNull();
                dbContext.Themes.FirstOrDefault(t => t.Id == newTheme.Id).Name.ShouldBe(input.Name);
            }
        }

        [Fact]
        public async Task Should_Add_KeyWord_To_Theme()
        {
            //Arrange
            var themeId = 1;
            var inputKeyWords = new List<string> {"keyword1", "keyword2", "keyword3"};
            //Act
            var addedKeyWords = await _themeAppService.AddKeyWordsAsync(inputKeyWords, themeId);
            //Assert
            addedKeyWords.ShouldNotBeNull();
            addedKeyWords.Count.ShouldBe(inputKeyWords.Count);

            foreach (var keywordDto in addedKeyWords)
            {
                keywordDto.ShouldNotBeNull();
                keywordDto.Id.ShouldBePositive();
            }
        }

        [Fact]
        public async Task Should_Delete_Theme()
        {
            // Arrange
            var themeId = 4;

            // Act
            await _themeAppService.DeleteThemeAsync(themeId);

            // Assert

            // Verify that the theme is deleted
            using (var uow = _unitOfWorkManager.Begin())
            {
                var dbContext = await _dbContextProvider.GetDbContextAsync();
                dbContext.Themes.FirstOrDefault(t => t.Id == themeId).ShouldBeNull();
            }
        }

    }
}