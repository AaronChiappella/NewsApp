using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace NewsApp.Reads
{
    public interface IReadAppService:
        ICrudAppService<
            ReadDto,
            int,
            PagedAndSortedResultRequestDto,
            CreateUpdateReadDto
            >
    {
    }
}
