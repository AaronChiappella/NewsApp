using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace NewsApp.APIStatistics;
public interface IAPIStatisticAppService : IApplicationService
{
    Task<double> GetAverageAPIAccessTimeAsync();
    Task<int> GetAmountOfAPIAccessesAsync();
    Task<int> GetLast30DaysAmountOfAPIAccessesAsync();
    Task<APIStatisticDto> GetAPIStatisticsAsync();
}

