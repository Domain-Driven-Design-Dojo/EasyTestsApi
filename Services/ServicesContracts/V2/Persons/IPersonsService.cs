using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Common;
using DataTransferObjects.DTOs.Shared;
using DataTransferObjects.DTOs.V1.Persons;
using X.PagedList;

namespace Services.ServicesContracts.V2.Persons
{
    public interface IPersonsService : IScopedDependency
    {
        Task<ApiResult<PersonListDto>> CreatePersonWithDetail(PersonCuDto input, long creatorId, CancellationToken cancellationToken);
        Task<ApiResult<PersonFullInfoListDto>> GetFullInfo(long id, CancellationToken cancellationToken);
        Task<ApiResult<PersonListDto>> UpdatePersonWithDetail(PersonCuDto input, long modifierId, CancellationToken cancellationToken);
        //Task<ApiResult<PersonListDto>> GetPersonByNationalId(string nationalId, CancellationToken cancellationToken);
    }
}
