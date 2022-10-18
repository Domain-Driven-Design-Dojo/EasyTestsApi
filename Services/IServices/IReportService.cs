﻿using DataTransferObjects.BasicDTOs;
using DataTransferObjects.CustomExpressions;
using DataTransferObjects.SharedModels;
using Entities.DatabaseModels.CommonModels.BaseModels;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using X.PagedList;

namespace Services.IServices
{
    public interface IReportService<TListDto, TSearchDto, TEntity, TKey>
        where TListDto : BaseDto<TListDto, TEntity, TKey>, new()
        where TEntity : BaseEntityWithActors<TKey>, new()
        where TSearchDto : BaseSearchDto, IHaveCustomExpression<TEntity, TSearchDto, TKey>
    {
        Task<ApiResult<IPagedList<TListDto>>> Get(TSearchDto searchDto, CancellationToken cancellationToken);
        Task<ApiResult<TListDto>> Get(TKey id, CancellationToken cancellationToken);
        Task<ApiResult<List<TListDto>>> GetAll(TSearchDto searchDto, CancellationToken cancellationToken);
    }
}
