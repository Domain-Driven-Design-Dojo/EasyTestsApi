using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common;
using Data.Contracts;
using DataTransferObjects.DTOs.Shared;
using DataTransferObjects.DTOs.V1.Persons;
using Entities.PersonModels;
using Microsoft.EntityFrameworkCore;
using Services.ServicesContracts.V1.Persons;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using X.PagedList;

namespace Services.Services.V1.Persons
{
    public class PersonService : IPersonService
    {
        private readonly IRepositoryWithActors<CrePerson, long> _repository;
        private readonly IRepository<CreIndividual> _individualRepository;
        private readonly IRepository<CreCompany> _companyRepository;

        private readonly IMapper _mapper;

        public PersonService(IRepositoryWithActors<CrePerson, long> repository, IMapper mapper
            , IRepository<CreIndividual> individualRepository
            , IRepository<CreCompany> companyRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _individualRepository = individualRepository;
            _companyRepository = companyRepository;
        }

        public async Task<ApiResult<PersonListDto>> CreatePerson(PersonCuDto input, long creatorId, CancellationToken cancellationToken)
        {
            if (input is null)
                return new ApiResult<PersonListDto>(false, ApiResultStatusCode.CompanyOrIndividualIsRequired, null, null);

            if (input.TypeId != 1 && input.TypeId != 2)
                return new ApiResult<PersonListDto>(false, ApiResultStatusCode.PersonTypeIsWrong, null, null);

            var personEntity = input.ToEntity(_mapper);
            if (personEntity.FPersonsTypesId == 1)//individual
            {
                if (personEntity.IndividualPerson is null)
                    return new ApiResult<PersonListDto>(false, ApiResultStatusCode.PersonInfoIsRequired, null, null);
            }
            else if (personEntity.FPersonsTypesId == 2)
            {
                if (personEntity.Company is not null)
                {
                    if (string.IsNullOrEmpty(personEntity.Company.NationalId))
                        return new ApiResult<PersonListDto>(false, ApiResultStatusCode.NationalCodeIsRequired, null, null);

                    var existingNationalId = await _companyRepository.TableNoTracking
                        .Where(x => x.NationalId == personEntity.Company.NationalId)
                        .ToListAsync(cancellationToken);

                    if (existingNationalId is { Count: > 0 })
                        return new ApiResult<PersonListDto>(false, ApiResultStatusCode.NationalIdExists, null, null);
                }
                else
                {
                    return new ApiResult<PersonListDto>(false, ApiResultStatusCode.PersonInfoIsRequired, null, null);
                }
            }

            await _repository.AddAsync(personEntity, creatorId, cancellationToken);

            if (personEntity.Id == 0)
                return new ApiResult<PersonListDto>(false, ApiResultStatusCode.InsertFailed, null, null);

            var dto = await _repository.TableNoTracking.ProjectTo<PersonListDto>(_mapper.ConfigurationProvider).SingleOrDefaultAsync(p => p.Id.Equals(personEntity.Id), cancellationToken);

            if (dto == null)
                return new ApiResult<PersonListDto>(false, ApiResultStatusCode.InsertFailed, null, null);

            return dto;
        }

        public async Task<ApiResult<PersonCuDto>> GetFullInfo(long id, CancellationToken cancellationToken)
        {
            var dto = await _repository.TableNoTracking.ProjectTo<PersonCuDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(p => p.Id.Equals(id), cancellationToken);

            if (dto == null)
                return new ApiResult<PersonCuDto>(false, ApiResultStatusCode.NotFound, null);

            return dto;
        }

        public async Task<ApiResult<PersonListDto>> UpdatePerson(PersonCuDto input, long modifierId, CancellationToken cancellationToken)
        {
            if (input is null)
                return new ApiResult<PersonListDto>(false, ApiResultStatusCode.CompanyOrIndividualIsRequired, null, null);

            if (input.TypeId != 1 && input.TypeId != 2)
                return new ApiResult<PersonListDto>(false, ApiResultStatusCode.PersonTypeIsWrong, null, null);

            if (input.Id == 0)
                return new ApiResult<PersonListDto>(false, ApiResultStatusCode.CompanyOrIndividualIsRequired, null, null);

            var existingPerson =
                await _repository.TableNoTracking
                    .Include(x => x.Company)
                    .Include(x => x.IndividualPerson)
                    .SingleOrDefaultAsync(x => x.Id == input.Id, cancellationToken);

            if (existingPerson == null)
                return new ApiResult<PersonListDto>(false, ApiResultStatusCode.NotFound, null, null);

            var personEntity = input.ToEntity(_mapper);
            personEntity.CreationDate = existingPerson.CreationDate;
            personEntity.CreatorId = existingPerson.CreatorId;

            if (personEntity.FPersonsTypesId == 1)//individual
            {
                if (personEntity.IndividualPerson is not null)
                {
                    if (input.IndividualPerson.Id == 0)
                        return new ApiResult<PersonListDto>(false, ApiResultStatusCode.BadRequest, null, null);

                }
                else
                    return new ApiResult<PersonListDto>(false, ApiResultStatusCode.PersonInfoIsRequired, null, null);
            }
            else if (personEntity.FPersonsTypesId == 2)
            {
                if (personEntity.Company is not null)
                {
                    if (input.Company.Id == 0)
                        return new ApiResult<PersonListDto>(false, ApiResultStatusCode.BadRequest, null, null);

                    if (string.IsNullOrEmpty(personEntity.Company.NationalId))
                        return new ApiResult<PersonListDto>(false, ApiResultStatusCode.NationalCodeIsRequired, null, null);

                    var existingNationalId = await _companyRepository.TableNoTracking
                        .Where(x => x.NationalId == personEntity.Company.NationalId && x.Id != personEntity.Company.Id)
                        .ToListAsync(cancellationToken);

                    if (existingNationalId is { Count: > 0 })
                        return new ApiResult<PersonListDto>(false, ApiResultStatusCode.NationalIdExists, null, null);
                }
                else
                {
                    return new ApiResult<PersonListDto>(false, ApiResultStatusCode.PersonInfoIsRequired, null, null);
                }
            }

            personEntity.IsActive = existingPerson.IsActive;

            await _repository.UpdateAsync(personEntity, modifierId, cancellationToken);

            if (personEntity.Id == 0)
                return new ApiResult<PersonListDto>(false, ApiResultStatusCode.UpdateFailed, null, null);

            var dto = await _repository.TableNoTracking.ProjectTo<PersonListDto>(_mapper.ConfigurationProvider).SingleOrDefaultAsync(p => p.Id.Equals(personEntity.Id), cancellationToken);

            if (dto == null)
                return new ApiResult<PersonListDto>(false, ApiResultStatusCode.UpdateFailed, null, null);

            return dto;
        }
    }
}
