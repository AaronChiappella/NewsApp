using System;
using NewsApp.Searches;
using Volo.Abp.Application.Dtos;

namespace NewsApp.Failures;

public class FailureDto : EntityDto<int>
{
    public DateTime ErrorDateTime { get; set; }
    
    public SearchDto Search { get; set; }
}