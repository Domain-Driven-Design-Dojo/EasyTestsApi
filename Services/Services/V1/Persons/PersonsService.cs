using Common;
using Common.Exceptions;
using Common.Utilities;
using Data.Contracts;
using Entities.PersonModels;
using Microsoft.EntityFrameworkCore;
using Services.ServicesContracts.V1.Persons;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Services.V1.Persons
{
    public class PersonsService : IPersonsService
    {

        private readonly IIndividualRepository _repository;
        private readonly ICompanyRepository _repositoryCompany;
        private readonly IRepositoryWithActors<CrePerson, long> _personRepository;

        public PersonsService(IIndividualRepository repository,
            ICompanyRepository repositoryCompany,
            IRepositoryWithActors<CrePerson, long> personRepository)
        {
            _repository = repository;
            _repositoryCompany = repositoryCompany;
            _personRepository = personRepository;
        }

        public async Task<List<CrePerson>> GetAllPersons(CancellationToken cancellationToken)
        {
            return await _personRepository.TableNoTracking.ToListAsync(cancellationToken);
        }

        public async Task<CrePerson> GetPerson(long Id, CancellationToken cancellationToken)
        {
            var person = _personRepository.GetByIdAsync(cancellationToken, Id).Result;
            if (person == null)
                throw new BadRequestException(ApiResultStatusCode.NotFound.ToDisplay());

            return person;
        }

        public async Task<CrePerson> CreatePerson(CrePerson input, long creatorId, CancellationToken cancellationToken)
        {
            CrePerson newPerson = new CrePerson();

            if (input.FPersonsTypesId == 1) //individual
            {
                //var individual = input.IndividualPersons.FirstOrDefault();
                var individual = input.IndividualPerson;
                if (individual is null)
                {
                    throw new AppException(ApiResultStatusCode.CompanyOrIndividualIsRequired);
                }



                input.IndividualPerson = individual;
                input.Company = null;
            }
            else
            {
                var company = input.Company;
                if (company is null)
                {
                    throw new AppException(ApiResultStatusCode.CompanyOrIndividualIsRequired);
                }

                var existingCompany = await _repositoryCompany.TableNoTracking.Where(x => x.NationalId == input.Company.NationalId).ToListAsync();
                if (existingCompany is { Count: > 0 })
                    throw new AppException(ApiResultStatusCode.NationalIdExists);

                input.Company = company;
            }


            await _personRepository.AddAsync(input, creatorId, cancellationToken);
            if (input.Id > 0)
            {
                return input;
            }

            throw new AppException(ApiResultStatusCode.InsertFailed);
        }

        public async Task<CrePerson> GetPersonById(long Id, CancellationToken cancellationToken)
        {
            //var person = await _personRepository.GetByIdAsync(cancellationToken, Id);
            var person = await _personRepository.Entities.Where(p => p.Id == Id).FirstOrDefaultAsync();

            if (person is null)
                throw new AppException(ApiResultStatusCode.NotFound);
            return person;
        }

        //public async Task<CrePerson> GetPersonByNationalId(string nationalId, CancellationToken cancellationToken)
        //{

        //    if (nationalId.Length == 10)
        //    {
        //        var individual = await _personRepository.Entities.Where(p => p.IndividualPerson.NationalId == nationalId)
        //            .Include(p => p.IndividualPerson).Include(p => p.PersonType).FirstOrDefaultAsync(cancellationToken);
        //        if (individual is null)
        //            throw new AppException(ApiResultStatusCode.NotFound);

        //        return individual;

        //    }

        //    if (nationalId.Length == 11)
        //    {
        //        var company = await _personRepository.Entities.Where(p => p.Company.NationalId == nationalId)
        //            .Include(p => p.Company).Include(p => p.PersonType).FirstOrDefaultAsync(cancellationToken);
        //        if (company is null)
        //            throw new AppException(ApiResultStatusCode.NotFound);

        //        return company;
        //    }

        //    throw new AppException(ApiResultStatusCode.NationalCodeFormatIsWrong);

        //    //throw new AppException(ApiResultStatusCode.NationalCodeFormatIsWrong);


        //    //var person = await GetPersonById(individual.FPersonsId, cancellationToken);
        //    //if (person is null)
        //    //    throw new AppException(ApiResultStatusCode.NotFound);

        //    //return person;
        //}
    }
}
