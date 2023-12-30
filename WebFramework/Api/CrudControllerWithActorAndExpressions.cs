using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common;
using Common.Exceptions;
using Common.Utilities;
using Data.Contracts;
using DataTransferObjects.CustomExpressions;
using DataTransferObjects.DTOs.BaseDtos;
using DataTransferObjects.DTOs.Shared;
using Entities.BaseModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace WebFramework.Api
{
    //[ApiVersion("1")]
    [ApiVersion("2")]
    [Authorize]
    public class CrudControllerWithActorAndExpressions<TDto, TListDto, TSearchDto, TEntity, TKey> : BaseController
        where TDto : BaseDto<TDto, TEntity, TKey>, new()
        where TListDto : BaseDto<TListDto, TEntity, TKey>, new()
        where TEntity : BaseEntityWithActors<TKey>, new()
    where TSearchDto : BaseSearchDto, IHaveCustomExpression<TEntity, TSearchDto, TKey>
    {
        protected readonly IRepositoryWithActors<TEntity, TKey> Repository;
        
        protected readonly IMapper Mapper;

        public CrudControllerWithActorAndExpressions(IRepositoryWithActors<TEntity, TKey> repository, IMapper mapper)
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
            if(searchDto.RecordsPerPage > 50)
            {
                return new ApiResult<IPagedList<TListDto>>(true, ApiResultStatusCode.MaximumRecordsPerPageExceeded, null, null);
            }

            var result = await Repository.TableNoTracking
                .Where(expression).ProjectTo<TListDto>(Mapper.ConfigurationProvider)
                .ToPagedListAsync(searchDto.PageNumber?? 1, searchDto.RecordsPerPage ?? 15, cancellationToken);
            if (result.Count == 0)
                throw new AppException(ApiResultStatusCode.NotFound);

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

        //[HttpGet("GetForUpdate/{id}")]
        //public virtual async Task<ApiResult<TDto>> GetForUpdate(TKey id, CancellationToken cancellationToken)
        //{
        //    var dto = await Repository.TableNoTracking.ProjectTo<TDto>(Mapper.ConfigurationProvider).SingleOrDefaultAsync(p => p.Id.Equals(id), cancellationToken);

        //    if (dto == null)
        //        return NotFound();

        //    return dto;
        //}

        [HttpPost]
        public virtual async Task<ApiResult<TListDto>> Create(TDto dto, CancellationToken cancellationToken)
        {
            var creatorId = User.Identity.GetUserId();

            var model = dto.ToEntity(Mapper);

            await Repository.AddAsync(model, creatorId, cancellationToken);

            var resultDto = await Repository.TableNoTracking.ProjectTo<TListDto>(Mapper.ConfigurationProvider).SingleOrDefaultAsync<TListDto>(p => p.Id.Equals(model.Id), cancellationToken);

            return resultDto;
        }

        [HttpPut]
        //public virtual async Task<ApiResult<TListDto>> Update(TKey id, TDto dto, CancellationToken cancellationToken)
        public virtual async Task<ApiResult<TListDto>> Update(TDto dto, CancellationToken cancellationToken)
        {
            var modifierId = User.Identity.GetUserId();
            var model = await Repository.GetByIdAsync(cancellationToken, dto.Id);

            model = dto.ToEntity(Mapper, model);

            await Repository.UpdateAsync(model, modifierId, cancellationToken);

            var resultDto = await Repository.TableNoTracking.ProjectTo<TListDto>(Mapper.ConfigurationProvider).SingleOrDefaultAsync<TListDto>(p => p.Id.Equals(model.Id), cancellationToken);

            return resultDto;
        }

        [HttpDelete("{id}")]
        public virtual async Task<ApiResult> Delete(TKey id, CancellationToken cancellationToken)
        {
            var model = await Repository.GetByIdAsync(cancellationToken, id);

            await Repository.DeleteAsync(model, cancellationToken);

            return Ok();
        }
    }

    //public class CrudControllerWithActorAndExpressions<TDto, TListDto, TSearchDto, TEntity> : CrudControllerWithActorAndExpressions<TDto, TListDto, TSearchDto, TEntity, long>
    //    where TDto : BaseDto<TDto, TEntity, long>, new()
    //    where TListDto : BaseDto<TListDto, TEntity, long>, new()
    //    where TEntity : BaseEntityWithActors<long>, new()
    //{
    //    public CrudControllerWithActorAndExpressions(IRepositoryWithActors<TEntity, long> repository, IMapper mapper)
    //        : base(repository, mapper)
    //    {
    //    }
    //}

    //public class CrudControllerWithActorAndExpressions<TDto, TEntity> : CrudControllerWithActorAndExpressions<TDto, TDto, TEntity, long>
    //    where TDto : BaseDto<TDto, TEntity, long>, new()
    //    where TEntity : BaseEntityWithActors<long>, new()
    //{
    //    public CrudControllerWithActorAndExpressions(IRepositoryWithActors<TEntity, long> repository, IMapper mapper)
    //        : base(repository, mapper)
    //    {
    //    }
    //}
}
