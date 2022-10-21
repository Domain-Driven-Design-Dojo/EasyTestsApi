using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Common;
using DataTransferObjects.DataTransferObjects.AccountsDTOs;
using DataTransferObjects.DataTransferObjects.UserDTOs;
using DataTransferObjects.SharedModels;
using Entities.DatabaseModels.UserModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.IServices;
using Services.IServices.V2;
using Services.Services.V2;
using WebFramework.Api;
using X.PagedList;

namespace Api.Controllers.V2.Users
{

    [ApiVersion("2")]
    public class GroupController : CrudControllerGenericWithRole<GroupCuDto, GroupListDto, GroupSearchDto, AccGroup, int>
    {
        private readonly ICrudService<GroupCuDto, GroupListDto, GroupSearchDto, AccGroup, int> _service;

        public GroupController(ICrudService<GroupCuDto, GroupListDto, GroupSearchDto, AccGroup, int> service) : base(service, "Group")
        {
            _service = service;
        }
    }
}
