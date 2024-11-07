using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NewsApp.News
{
    public interface INewsAppService
    {
        Task<ICollection<NewsDto>> Search(string query);
    }
}