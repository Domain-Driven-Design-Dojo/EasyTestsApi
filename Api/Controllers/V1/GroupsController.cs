using AutoMapper;
using Data.Contracts;
using DataTransferObjects.DataTransferObjects.AccountsDTOs;
using DataTransferObjects.DataTransferObjects.UserDTOs;
using Entities.DatabaseModels.UserModels;
using WebFramework.Api;

namespace Api.Controllers.V1
{
   public class GroupsController : CrudControllerWithActors<GroupDto, GroupSelectDto, AccGroup, int>
   {
      public GroupsController(IRepositoryWithActors<AccGroup, int> repository, IMapper mapper) : base(repository, mapper)
      {

      }
   }
}
