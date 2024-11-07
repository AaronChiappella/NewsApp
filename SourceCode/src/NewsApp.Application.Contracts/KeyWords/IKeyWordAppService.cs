﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace NewsApp.KeyWords
{
    public interface IKeyWordAppService : IApplicationService
    {
        Task<ICollection<KeyWordDto>> GetKeywordsAsync(int ThemeId);
    }
}
