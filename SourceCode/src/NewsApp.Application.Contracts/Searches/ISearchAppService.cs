using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace NewsApp.Searches;
public interface ISearchAppService : IApplicationService
{
    Task<SearchDto> SaveSearchAsync(string query, DateTime start, DateTime end, int count);
}
