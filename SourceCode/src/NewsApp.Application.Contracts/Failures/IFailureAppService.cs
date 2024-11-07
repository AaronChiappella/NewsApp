using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace NewsApp.Failures;

public interface IFailureAppService :
    ICrudAppService< //Defines CRUD methods
        FailureDto, //Used to show books
        int, //Primary key of the book entity
        PagedAndSortedResultRequestDto, //Used for paging/sorting
        CreateUpdateFailureDto> //Used to create/update a book
        {

        }
