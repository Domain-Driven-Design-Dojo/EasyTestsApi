using AutoMapper;
using Data.Contracts;
using DataTransferObjects.DataTransferObjects.HrDTOs;
using Entities.DatabaseModels.HrModels;
using WebFramework.Api;

namespace Api.Controllers.V1.BasicInformation
{
    public class PeopleTypesController : CrudControllerWithActors<PersonTypeDto, PersonTypeSelectDto, CrePeopleType, int>
    {
        public PeopleTypesController(IRepositoryWithActors<CrePeopleType, int> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
