using AutoMapper;
using Data.Contracts;
using DataTransferObjects.DTOs.Shared.Users;
using Entities.UserModels;
using WebFramework.Api;

namespace Api.Controllers.V1.Users
{
    public class GroupsController : CrudControllerWithActors<GroupDto, GroupSelectDto, AccGroup, int>
    {
        public GroupsController(IRepositoryWithActors<AccGroup, int> repository, IMapper mapper) : base(repository, mapper)
        {

        }
    }
}
