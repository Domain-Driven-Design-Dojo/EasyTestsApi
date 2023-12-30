using AutoMapper;
using Data.Contracts;
using DataTransferObjects.DTOs.Shared.Users;
using Entities.UserModels;
using Microsoft.AspNetCore.Authorization;
using WebFramework.Api;

namespace Api.Controllers.V1.Users
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
