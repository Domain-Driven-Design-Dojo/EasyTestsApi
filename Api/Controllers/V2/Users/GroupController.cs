using DataTransferObjects.DTOs.Shared.Users;
using Entities.UserModels;
using Microsoft.AspNetCore.Mvc;
using Services.ServicesContracts.BaseServices;
using WebFramework.Api;

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
