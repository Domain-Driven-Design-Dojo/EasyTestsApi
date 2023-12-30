using AutoMapper;
using Data.Contracts;
using DataTransferObjects.DTOs.V1.Persons;
using Entities.PersonModels;
using WebFramework.Api;

namespace Api.Controllers.V1.Legal
{
    public class PersonTypesController : CrudControllerWithActors<PersonTypeDto, PersonTypeSelectDto, CrePersonType, int>
    {
        public PersonTypesController(IRepositoryWithActors<CrePersonType, int> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
