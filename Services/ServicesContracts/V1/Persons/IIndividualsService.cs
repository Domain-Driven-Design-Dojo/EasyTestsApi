using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Entities.PersonModels;

namespace Services.ServicesContracts.V1.Persons
{
    public interface IIndividualsService
    {
        Task<CreIndividual> CreateIndividual(CreIndividual input, long creatorId, CancellationToken cancellationToken);
    }
}
