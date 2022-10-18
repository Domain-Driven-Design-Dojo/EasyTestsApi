using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Entities.DatabaseModels.HrModels;

namespace Services.IServices
{
    public interface IPeopleService
    {
        Task<List<CrePerson>> GetAllPeople(CancellationToken cancellationToken);
        Task<CrePerson> CreatePerson(CrePerson input, long creatorId, CancellationToken cancellationToken);
        Task<CrePerson> GetPersonById(long Id, CancellationToken cancellationToken);
        //Task<CrePerson> GetPersonByNationalId(string nationalId, CancellationToken cancellationToken);
    }
}
