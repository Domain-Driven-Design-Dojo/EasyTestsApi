using System.Threading;
using System.Threading.Tasks;
using DataTransferObjects.DTOs.Shared;
using DataTransferObjects.DTOs.V1.Persons;

namespace Services.ServicesContracts.V1.Persons
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
