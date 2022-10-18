using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common;
using Common.Exceptions;
using Data.ApplicationUtilities;
using Data.Contracts;
using Entities.DatabaseModels.HrModels;
using Services.IServices;

namespace Services.Services.V1
{
    public class IndividualsService : IIndividualsService
    {
        private readonly IIndividualRepository _repository;
        private readonly IRepositoryWithActors<CrePerson, long> _personRepository;

        public IndividualsService(IIndividualRepository repository,
            IRepositoryWithActors<CrePerson, long> personRepository)
        {
            _repository = repository;
            _personRepository = personRepository;
        }

        public async Task<CreIndividual> CreateIndividual(CreIndividual input, long creatorId, CancellationToken cancellationToken)
        {

            CrePerson newPerson = new CrePerson
            {
                FPeopleTypesId = 1, 
                IndividualPerson = input
            };

            await _personRepository.AddAsync(newPerson, creatorId, cancellationToken);
            return input;
        }
    }


}
