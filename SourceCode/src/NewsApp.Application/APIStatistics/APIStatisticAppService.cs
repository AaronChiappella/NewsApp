using System;
using System.Threading.Tasks;
using NewsApp.Searches;
using Volo.Abp.Domain.Repositories;

namespace NewsApp.APIStatistics
{
    public class APIStatisticsAppService : NewsAppAppService, IAPIStatisticAppService
    {
        private readonly IRepository<Search, int> _searchRepository;
        public APIStatisticsAppService(IRepository<Search, int> searchRepository)
        {
            _searchRepository = searchRepository;
        }

        public async Task<double> GetAverageAPIAccessTimeAsync()
        {
            var searches = await _searchRepository.GetListAsync();

            var sum = 0.0;

            foreach (var search in searches)
            {
                var searchTime = search.EndDateTime - search.StartDateTime;
                sum += searchTime.TotalSeconds;
            }

            var average = sum / searches.Count;
            return average;
        }

        public async Task<int> GetAmountOfAPIAccessesAsync()
        {
            var searches = await _searchRepository.GetListAsync();

            var amountOfAccesses = searches.Count;

            return amountOfAccesses;
        }

        public async Task<int> GetLast30DaysAmountOfAPIAccessesAsync()
        {
            DateTime hace30Dias = DateTime.Now.AddDays(-30);

            var searches = await _searchRepository.GetListAsync(s => s.StartDateTime >= hace30Dias);

            var amountOfAccesses = searches.Count;

            return amountOfAccesses;
        }

        public async Task<APIStatisticDto> GetAPIStatisticsAsync()
        {
            var average = await GetAverageAPIAccessTimeAsync();
            var allTimeAmountOfAccesses = await GetAmountOfAPIAccessesAsync();
            var last30DaysAmountOfAccesses = await GetAmountOfAPIAccessesAsync();
            // llamar al resto de métodos de métricas aquí y agregarlos en el constructor
            var apiStatistic = new APIStatisticDto 
            { 
                AverageAccessTime = average,
                AllTimeAmountOfAccesses = allTimeAmountOfAccesses,
                Last30DaysAmountOfAccesses = last30DaysAmountOfAccesses 
            };
            return apiStatistic;
        }

    }
}
