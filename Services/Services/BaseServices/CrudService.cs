using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common;
using Data.Contracts;
using DataTransferObjects.CustomExpressions;
using DataTransferObjects.DTOs.BaseDtos;
using DataTransferObjects.DTOs.Shared;
using Entities.BaseModels;
using Microsoft.EntityFrameworkCore;
using Services.ServicesContracts.BaseServices;
using X.PagedList;

namespace Services.Services.BaseServices
{
    public class CrudService<TDto, TListDto, TSearchDto, TEntity, TKey> : ReportService<TListDto, TSearchDto, TEntity, TKey>, ICrudService<TDto, TListDto, TSearchDto, TEntity, TKey>
         where TDto : BaseDto<TDto, TEntity, TKey>, new()
        where TListDto : BaseDto<TListDto, TEntity, TKey>, new()
        where TEntity : BaseEntityWithActors<TKey>, new()
        where TSearchDto : BaseSearchDto, IHaveCustomExpression<TEntity, TSearchDto, TKey>

    {
        public CrudService(IRepositoryWithActors<TEntity, TKey> repository, IMapper mapper) : base(repository, mapper)
        {

        }

        public virtual async Task<ApiResult<TListDto>> Create(TDto dto, long creatorId, CancellationToken cancellationToken)
        {
            var model = dto.ToEntity(Mapper);

            await Repository.AddAsync(model, creatorId, cancellationToken);

            var resultDto = await Repository.TableNoTracking.ProjectTo<TListDto>(Mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(p => p.Id.Equals(model.Id), cancellationToken);

            return resultDto;
        }

        public virtual async Task<ApiResult<TListDto>> Update(TDto dto, long modifierId, CancellationToken cancellationToken)
        {
            var model = await Repository.GetByIdAsync(cancellationToken, dto.Id);

            model = dto.ToEntity(Mapper, model);

            await Repository.UpdateAsync(model, modifierId, cancellationToken);

            var resultDto = await Repository.TableNoTracking.ProjectTo<TListDto>(Mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(p => p.Id.Equals(model.Id), cancellationToken);

            return resultDto;
        }

        public virtual async Task<ApiResult> Delete(TKey id, long modifierId, CancellationToken cancellationToken)
        {
            var model = await Repository.GetByIdAsync(cancellationToken, id);
            try
            {
                await Repository.DeleteAsync(model, cancellationToken);

                return new ApiResult<TListDto>(true, ApiResultStatusCode.Success, null);
            }
            catch (Exception)
            {
                model.IsActive = false;
                await Repository.UpdateAsync(model, modifierId, cancellationToken);
                return new ApiResult<TListDto>(true, ApiResultStatusCode.Success, null);
            }

        }

        public virtual async Task<ApiResult<TListDto>> SoftUpdate(TDto dto, long modifierId, CancellationToken cancellationToken)
        {
            var model = await Repository.GetByIdAsync(cancellationToken, dto.Id);

            if (model is null)
                return new ApiResult<TListDto>(true, ApiResultStatusCode.NotFound, null); ;

            model.IsActive = false;

            await Repository.UpdateAsync(model, modifierId, cancellationToken);


            var newModel = new TEntity();
            dto.Id = newModel.Id;
            newModel = dto.ToEntity(Mapper, newModel);
            newModel.IsActive = true;
            await Repository.AddAsync(newModel, modifierId, cancellationToken);

            var result = await Repository.TableNoTracking.Where(x => x.Id.Equals(model.Id) && x.IsActive)
               .ProjectTo<TListDto>(Mapper.ConfigurationProvider).FirstOrDefaultAsync(cancellationToken);

            return result;

        }

        public virtual async Task<ApiResult> SoftDelete(TKey id, long modifierId, CancellationToken cancellationToken)
        {
            var exist = await Repository.TableNoTracking.Where(x => x.Id.Equals(id) && x.IsActive).FirstOrDefaultAsync(cancellationToken);

            if (exist is null)
                return new ApiResult<TListDto>(false, ApiResultStatusCode.TripPlanNoExist, null);

            exist.IsActive = false;

            await Repository.UpdateAsync(exist, modifierId, cancellationToken);

            return new ApiResult(true, ApiResultStatusCode.Success, null);
        }

    }
}