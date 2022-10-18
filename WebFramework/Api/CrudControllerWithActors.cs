using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common;
using Data.Contracts;
using DataTransferObjects.BasicDTOs;
using DataTransferObjects.SharedModels;
using Entities.DatabaseModels.CommonModels.BaseModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace WebFramework.Api
{
    [ApiVersion("1")]
    [Authorize]
    public class CrudControllerWithActors<TDto, TSelectDto, TEntity, TKey> : BaseController
        where TDto : BaseDto<TDto, TEntity, TKey>, new()
        where TSelectDto : BaseDto<TSelectDto, TEntity, TKey>, new()
        where TEntity : BaseEntityWithActors<TKey>, new()
    {
        protected readonly IRepositoryWithActors<TEntity, TKey> Repository;
        protected readonly IMapper Mapper;

        public CrudControllerWithActors(IRepositoryWithActors<TEntity, TKey> repository, IMapper mapper)
        {
            Repository = repository;
            Mapper = mapper;
        }

        [HttpGet]
        public virtual async Task<ActionResult<List<TDto>>> Get(CancellationToken cancellationToken)
        {
            var list = await EntityFrameworkQueryableExtensions.ToListAsync<TDto>(Extensions.ProjectTo<TDto>(Repository.TableNoTracking, Mapper.ConfigurationProvider), cancellationToken);

            return Ok(list);
        }

        //[HttpGet("Search")]
        //public virtual async Task<ActionResult<IPagedList<TSelectDto>>> Get(int pageNumber, int recordsNumber, Expression<Func<TEntity, bool>> whereExpr)
        //{
        //    var list = await Repository.GetEntitiesAsync(whereExpr, pageNumber, recordsNumber);

        //    return Ok(list);
        //}


        [HttpGet("{id}")]
        public virtual async Task<ApiResult<TSelectDto>> Get(TKey id, CancellationToken cancellationToken)
        {
            var dto = await EntityFrameworkQueryableExtensions.SingleOrDefaultAsync<TSelectDto>(Extensions.ProjectTo<TSelectDto>(Repository.TableNoTracking, Mapper.ConfigurationProvider), p => p.Id.Equals(id), cancellationToken);

            if (dto == null)
                return NotFound();

            return dto;
        }

        //[HttpGet("{id}")]
        //public virtual async Task<ApiResult<TDto>> Get(TKey id, CancellationToken cancellationToken)
        //{
        //    var dto = await EntityFrameworkQueryableExtensions.SingleOrDefaultAsync<TDto>(Extensions.ProjectTo<TDto>(Repository.TableNoTracking, Mapper.ConfigurationProvider), p => p.Id.Equals(id), cancellationToken);

        //    if (dto == null)
        //        return NotFound();

        //    return dto;
        //}

        [HttpPost]
        public virtual async Task<ApiResult<TSelectDto>> Create(TDto dto, CancellationToken cancellationToken)
        {
            var creatorId = User.Identity.GetUserId();

            var model = dto.ToEntity(Mapper);

            await Repository.AddAsync(model, creatorId, cancellationToken);

            var resultDto = await EntityFrameworkQueryableExtensions.SingleOrDefaultAsync<TSelectDto>(Extensions.ProjectTo<TSelectDto>(Repository.TableNoTracking, Mapper.ConfigurationProvider), p => p.Id.Equals(model.Id), cancellationToken);

            return resultDto;
        }

        [HttpPut]
        //public virtual async Task<ApiResult<TSelectDto>> Update(TKey id, TDto dto, CancellationToken cancellationToken)
        public virtual async Task<ApiResult<TSelectDto>> Update(TDto dto, CancellationToken cancellationToken)
        {
            var modifierId = User.Identity.GetUserId();
            var model = await Repository.GetByIdAsync(cancellationToken, dto.Id);

            model = dto.ToEntity(Mapper, model);

            await Repository.UpdateAsync(model, modifierId, cancellationToken);

            var resultDto = await EntityFrameworkQueryableExtensions.SingleOrDefaultAsync<TSelectDto>(Extensions.ProjectTo<TSelectDto>(Repository.TableNoTracking, Mapper.ConfigurationProvider), p => p.Id.Equals(model.Id), cancellationToken);

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

    public class CrudControllerWithActors<TDto, TSelectDto, TEntity> : CrudControllerWithActors<TDto, TSelectDto, TEntity, long>
        where TDto : BaseDto<TDto, TEntity, long>, new()
        where TSelectDto : BaseDto<TSelectDto, TEntity, long>, new()
        where TEntity : BaseEntityWithActors<long>, new()
    {
        public CrudControllerWithActors(IRepositoryWithActors<TEntity, long> repository, IMapper mapper)
            : base(repository, mapper)
        {
        }
    }

    public class CrudControllerWithActors<TDto, TEntity> : CrudControllerWithActors<TDto, TDto, TEntity, long>
        where TDto : BaseDto<TDto, TEntity, long>, new()
        where TEntity : BaseEntityWithActors<long>, new()
    {
        public CrudControllerWithActors(IRepositoryWithActors<TEntity, long> repository, IMapper mapper)
            : base(repository, mapper)
        {
        }
    }
}
