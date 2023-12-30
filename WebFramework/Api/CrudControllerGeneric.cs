using Common;
using DataTransferObjects.CustomExpressions;
using DataTransferObjects.DTOs.BaseDtos;
using DataTransferObjects.DTOs.Shared;
using Entities.BaseModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.ServicesContracts.BaseServices;
using System.Threading;
using System.Threading.Tasks;
using X.PagedList;

namespace WebFramework.Api
{
    [ApiVersion("2")]
    [Authorize]
    //public class CrudControllerGeneric<TDto, TListDto, TSearchDto, TEntity, TKey, TService> : BaseController
    public class CrudControllerGeneric<TDto, TListDto, TSearchDto, TEntity, TKey> : BaseController
        where TDto : BaseDto<TDto, TEntity, TKey>, new()
        where TListDto : BaseDto<TListDto, TEntity, TKey>, new()
        where TEntity : BaseEntityWithActors<TKey>, new()
        where TSearchDto : BaseSearchDto, IHaveCustomExpression<TEntity, TSearchDto, TKey>, new()
        //where TService : CrudService<TDto, TListDto, TSearchDto, TEntity, TKey>, new()
    {
        //private readonly TService _service;

        private readonly ICrudService<TDto, TListDto, TSearchDto, TEntity, TKey> _service;
        public CrudControllerGeneric(ICrudService<TDto, TListDto, TSearchDto, TEntity, TKey> service)
        {
            _service = service;
        }

        [HttpPost("Search")]
        public virtual async Task<ApiResult<IPagedList<TListDto>>> Get(TSearchDto searchDto,
            CancellationToken cancellationToken)
        {
            return await _service.Get(searchDto, cancellationToken);
        }


        [HttpGet("{id}")]
        public virtual async Task<ApiResult<TListDto>> Get(TKey id, CancellationToken cancellationToken)
        {
            return await _service.Get(id, cancellationToken);
        }

        [HttpPost]
        public virtual async Task<ApiResult<TListDto>> Create(TDto dto, CancellationToken cancellationToken)
        {
            var creatorId = User.Identity.GetUserId();

            return await _service.Create(dto, creatorId, cancellationToken);
        }

        [HttpPut]
        public virtual async Task<ApiResult<TListDto>> Update(TDto dto, CancellationToken cancellationToken)
        {
            var modifierId = User.Identity.GetUserId();
            return await _service.Update(dto, modifierId, cancellationToken);
        }

        [HttpDelete("{id}")]
        public virtual async Task<ApiResult> Delete(TKey id, CancellationToken cancellationToken)
        {
            var modifierId = User.Identity.GetUserId();
            return await _service.Delete(id, modifierId, cancellationToken);
        }
    }
}