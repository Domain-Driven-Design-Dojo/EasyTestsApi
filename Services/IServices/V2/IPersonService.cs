using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Common;
using DataTransferObjects.DataTransferObjects.HrDTOs;
using DataTransferObjects.SharedModels;
using X.PagedList;

namespace Services.IServices.V2
{
    public interface IPersonService: IScopedDependency
    {
        Task<ApiResult<PersonListDto>> CreatePersonWithDetail(PersonCuDto input,long creatorId, CancellationToken cancellationToken);
        Task<ApiResult<PersonFullInfoListDto>> GetFullInfo(long id, CancellationToken cancellationToken);
        Task<ApiResult<PersonListDto>> UpdatePersonWithDetail(PersonCuDto input, long modifierId, CancellationToken cancellationToken);
        //Task<ApiResult<PersonListDto>> GetPersonByNationalId(string nationalId, CancellationToken cancellationToken);
    }
}
