using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DataTransferObjects.DataTransferObjects.HrDTOs;

namespace Services.IServices.V2
{
    public interface IPeopleTypeService
    {
        Task<List<PeopleTypeListDto>> GetPeopleTypeListAsync(CancellationToken cancellationToken);
    }
}
