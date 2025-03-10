﻿using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data.Contracts;
using DataTransferObjects.DTOs.BaseDtos;
using DataTransferObjects.DTOs.Shared;
using Entities.BaseModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebFramework.Api
{
    [ApiVersion("1")]
    public class CrudController<TDto, TSelectDto, TEntity, TKey> : BaseController
        where TDto : BaseDto<TDto, TEntity, TKey>, new()
        where TSelectDto : BaseDto<TSelectDto, TEntity, TKey>, new()
        where TEntity : class, IEntity<TKey>, new()
    {
        protected readonly IRepository<TEntity> Repository;
        protected readonly IMapper Mapper;

        public CrudController(IRepository<TEntity> repository, IMapper mapper)
        {
            Repository = repository;
            Mapper = mapper;
        }

        //[HttpGet]
        //public virtual async Task<ActionResult<List<TSelectDto>>> Get(CancellationToken cancellationToken)
        //{
        //    var list = await EntityFrameworkQueryableExtensions.ToListAsync<TSelectDto>(Repository.TableNoTracking.ProjectTo<TSelectDto>(Mapper.ConfigurationProvider), cancellationToken);

        //    return Ok(list);
        //}

        [HttpGet]
        public virtual async Task<ActionResult<List<TDto>>> Get(CancellationToken cancellationToken)
        {
            var list = await EntityFrameworkQueryableExtensions.ToListAsync<TDto>(Extensions.ProjectTo<TDto>(Repository.TableNoTracking, Mapper.ConfigurationProvider), cancellationToken);

            return Ok(list);
        }


        [HttpGet("{id}")]
        public virtual async Task<ApiResult<TSelectDto>> Get(TKey id, CancellationToken cancellationToken)
        {
            var dto = await EntityFrameworkQueryableExtensions.SingleOrDefaultAsync<TSelectDto>(Extensions.ProjectTo<TSelectDto>(Repository.TableNoTracking, Mapper.ConfigurationProvider), p => p.Id.Equals(id), cancellationToken);

            if (dto == null)
                return NotFound();

            return dto;
        }

        [HttpPost]
        public virtual async Task<ApiResult<TSelectDto>> Create(TDto dto, CancellationToken cancellationToken)
        {
            var model = dto.ToEntity(Mapper);

            await Repository.AddAsync(model, cancellationToken);

            var resultDto = await EntityFrameworkQueryableExtensions.SingleOrDefaultAsync<TSelectDto>(Extensions.ProjectTo<TSelectDto>(Repository.TableNoTracking, Mapper.ConfigurationProvider), p => p.Id.Equals(model.Id), cancellationToken);

            return resultDto;
        }

        [HttpPut]
      //public virtual async Task<ApiResult<TSelectDto>> Update(TKey id, TDto dto, CancellationToken cancellationToken)
      public virtual async Task<ApiResult<TSelectDto>> Update(TDto dto, CancellationToken cancellationToken)

      {
         var model = await Repository.GetByIdAsync(cancellationToken, dto.Id);

            model = dto.ToEntity(Mapper, model);

            await Repository.UpdateAsync(model, cancellationToken);

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

    public class CrudController<TDto, TSelectDto, TEntity> : CrudController<TDto, TSelectDto, TEntity, long>
        where TDto : BaseDto<TDto, TEntity, long>, new()
        where TSelectDto : BaseDto<TSelectDto, TEntity, long>, new()
        where TEntity : class, IEntity<long>, new()
    {
        public CrudController(IRepository<TEntity> repository, IMapper mapper)
            : base(repository, mapper)
        {
        }
    }

    public class CrudController<TDto, TEntity> : CrudController<TDto, TDto, TEntity, long>
        where TDto : BaseDto<TDto, TEntity, long>, new()
        where TEntity : class, IEntity<long>, new()
    {
        public CrudController(IRepository<TEntity> repository, IMapper mapper)
            : base(repository, mapper)
        {
        }
    }
}
