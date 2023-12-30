using Data.Contracts;
using Entities.PersonModels;
using Services.ServicesContracts.V1.Persons;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Services.V1.Persons
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
                FPersonsTypesId = 1,
                IndividualPerson = input
            };

            await _personRepository.AddAsync(newPerson, creatorId, cancellationToken);
            return input;
        }
    }


}
