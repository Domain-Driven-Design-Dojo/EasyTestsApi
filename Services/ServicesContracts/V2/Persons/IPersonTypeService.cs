using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DataTransferObjects.DTOs.V1.Persons;

namespace Services.ServicesContracts.V2.Persons
{
    public interface IPersonsTypeService
    {
        Task<List<PersonsTypeListDto>> GetPersonsTypeListAsync(CancellationToken cancellationToken);
    }
}
