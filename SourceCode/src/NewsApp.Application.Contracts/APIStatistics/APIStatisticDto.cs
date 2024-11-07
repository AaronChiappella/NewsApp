using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services.Dto;

namespace NewsApp.APIStatistics
{
    public class APIStatisticDto
    {
        public double AverageAccessTime { get; set; }
        public int AllTimeAmountOfAccesses { get; set; }
        public int Last30DaysAmountOfAccesses { get; set; }

    }
}
