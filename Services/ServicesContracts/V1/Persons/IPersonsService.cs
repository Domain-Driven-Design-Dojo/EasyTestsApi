using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Entities.PersonModels;

namespace Services.ServicesContracts.V1.Persons
{
    public interface IPersonsService
    {
        Task<List<CrePerson>> GetAllPersons(CancellationToken cancellationToken);
        Task<CrePerson> CreatePerson(CrePerson input, long creatorId, CancellationToken cancellationToken);
        Task<CrePerson> GetPersonById(long Id, CancellationToken cancellationToken);
        //Task<CrePerson> GetPersonByNationalId(string nationalId, CancellationToken cancellationToken);
    }
}
