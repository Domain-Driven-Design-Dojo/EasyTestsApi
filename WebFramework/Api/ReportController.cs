using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common;
using Common.Exceptions;
using Data.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DataTransferObjects.CustomExpressions;
using DataTransferObjects.DTOs.Shared;
using X.PagedList;
using Common.Utilities;
using Entities.BaseModels;
using DataTransferObjects.DTOs.BaseDtos;

namespace WebFramework.Api
{
    [ApiVersion("2")]
    [Authorize]
    public class ReportController<TListDto, TSearchDto, TEntity, TKey> : BaseController
        where TListDto : BaseDto<TListDto, TEntity, TKey>, new()
        where TEntity : BaseEntityWithActors<TKey>, new()
    where TSearchDto : BaseSearchDto, IHaveCustomExpression<TEntity, TSearchDto, TKey>
    {
        protected readonly IReportRepository<TEntity, TKey> Repository;

        protected readonly IMapper Mapper;

        public ReportController(IReportRepository<TEntity, TKey> repository, IMapper mapper)
        {
            Repository = repository;
            Mapper = mapper;
        }

        [HttpGet]
        public virtual async Task<ActionResult<List<TListDto>>> Get(CancellationToken cancellationToken)
        {
            var list = await Repository.TableNoTracking.ProjectTo<TListDto>(Mapper.ConfigurationProvider).ToListAsync<TListDto>(cancellationToken);

            return Ok(list);
        }

        [HttpPost("Search")]
        public virtual async Task<ApiResult<IPagedList<TListDto>>> Get(TSearchDto searchDto, CancellationToken cancellationToken)
        {
            var expression = searchDto.GenerateExpression(searchDto);

            var result = await Repository.TableNoTracking
                .Where(expression).ProjectTo<TListDto>(Mapper.ConfigurationProvider)
                .ToPagedListAsync(searchDto.PageNumber ?? 1, searchDto.RecordsPerPage ?? 15, cancellationToken);
            if (result.Count == 0)
                return new ApiResult<IPagedList<TListDto>>(true, ApiResultStatusCode.NotFound, result, ApiResultStatusCode.NotFound.ToDisplay());

            return new ApiResult<IPagedList<TListDto>>(true, ApiResultStatusCode.Success, result, null,
                result.TotalItemCount,
                result.PageNumber, result.PageCount);
        }

        [HttpGet("{id}")]
        public virtual async Task<ApiResult<TListDto>> Get(TKey id, CancellationToken cancellationToken)
        {
            var dto = await Repository.TableNoTracking.ProjectTo<TListDto>(Mapper.ConfigurationProvider).SingleOrDefaultAsync(p => p.Id.Equals(id), cancellationToken);

            if (dto == null)
                return NotFound();
            return dto;
        }

    }
}
