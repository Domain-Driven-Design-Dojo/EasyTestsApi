using System.Threading;
using System.Threading.Tasks;
using DataTransferObjects.DataTransferObjects.HrDTOs;
using DataTransferObjects.SharedModels;

namespace Services.IServices
{
    public interface IPersonService
    {
        //Task<ApiResult<IPagedList<PersonListDto>>> SearchPerson(PersonSearchDto searchDto, CancellationToken cancellationToken);
        Task<ApiResult<PersonListDto>> CreatePerson(PersonCuDto input, long creatorId,
            CancellationToken cancellationToken);

        Task<ApiResult<PersonCuDto>> GetFullInfo(long id, CancellationToken cancellationToken);

        Task<ApiResult<PersonListDto>> UpdatePerson(PersonCuDto input, long modifierId,
            CancellationToken cancellationToken);
    }
}
