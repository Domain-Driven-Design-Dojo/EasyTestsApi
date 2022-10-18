using AutoMapper;
using Data.Contracts;
using DataTransferObjects.DataTransferObjects.AccountsDTOs;
using DataTransferObjects.DataTransferObjects.UserDTOs;
using Entities.DatabaseModels.UserModels;
using Microsoft.AspNetCore.Authorization;
using WebFramework.Api;

namespace Api.Controllers.V1
{
    [Authorize]
    public class RolesController : CrudController<RoleDto, AccRole>
    {
        public RolesController(IRepository<AccRole> repository, IMapper mapper)
            : base(repository, mapper)
        {

        }
    }
}
