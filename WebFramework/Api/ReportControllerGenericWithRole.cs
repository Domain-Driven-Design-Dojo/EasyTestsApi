using Common;
using Common.Exceptions;
using DataTransferObjects.BasicDTOs;
using DataTransferObjects.CustomExpressions;
using DataTransferObjects.SharedModels;
using Entities.DatabaseModels.CommonModels.BaseModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.IServices;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using X.PagedList;

namespace WebFramework.Api
{
    [ApiVersion("2")]
    [Authorize]
    public class ReportControllerGenericWithRole<TListDto, TSearchDto, TEntity, TKey> : BaseController
        where TListDto : BaseDto<TListDto, TEntity, TKey>, new()
        where TEntity : BaseEntityWithActors<TKey>, new()
        where TSearchDto : BaseSearchDto, IHaveCustomExpression<TEntity, TSearchDto, TKey>, new()
    {

        private readonly IReportService<TListDto, TSearchDto, TEntity, TKey> _service;
        public string _RoleEntityName;
        public ReportControllerGenericWithRole(IReportService<TListDto, TSearchDto, TEntity, TKey> service, string roleEntityName)
        {
            _service = service;
            _RoleEntityName = roleEntityName;
        }

        [HttpPost("Search")]
        public virtual async Task<ApiResult<IPagedList<TListDto>>> Get(TSearchDto searchDto,
            CancellationToken cancellationToken)
        {
            if (!User.IsInRole("SuperAdmin") && !User.IsInRole(_RoleEntityName.ToString() + "-List"))
                throw new AppException(ApiResultStatusCode.Forbidden, null, HttpStatusCode.Forbidden, null, null);
            return await _service.Get(searchDto, cancellationToken);
        }


        [HttpGet("{id}")]
        public virtual async Task<ApiResult<TListDto>> Get(TKey id, CancellationToken cancellationToken)
        {
            if (!User.IsInRole("SuperAdmin") && !User.IsInRole(_RoleEntityName + "-Detail"))
                throw new AppException(ApiResultStatusCode.Forbidden, null, HttpStatusCode.Forbidden, null, null);
            return await _service.Get(id, cancellationToken);
        }

    }
}
