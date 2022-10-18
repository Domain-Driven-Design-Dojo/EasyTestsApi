using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common;
using Common.Utilities;
using Data.ApplicationUtilities;
using Data.Contracts;
using DataTransferObjects.DataTransferObjects.HrDTOs;
using DataTransferObjects.SharedModels;
using Entities.DatabaseModels.HrModels;
using Microsoft.EntityFrameworkCore;
using Services.IServices.V2;
using X.PagedList;

namespace Services.Services.V2
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

        public async Task<ApiResult<IPagedList<PersonListDto>>> GetBranchesPeople(PersonSearchDto dto, CancellationToken cancellationToken)
        {
            var peopleId = (from person in _repository.TableNoTracking
                            select person.Id).Distinct();

            var people = _repository.TableNoTracking.Where(x => peopleId.Contains(x.Id));

            var expression = dto.GenerateExpression(dto);

            var result =
              await people.
              Where(expression).
              ProjectTo<PersonListDto>(_mapper.ConfigurationProvider)
              .ToPagedListAsync(dto.PageNumber ?? 1, dto.RecordsPerPage ?? 10, cancellationToken);

            if (result is { Count: 0 })
                return new ApiResult<IPagedList<PersonListDto>>(false, ApiResultStatusCode.NotFound, null);

            return new ApiResult<IPagedList<PersonListDto>>(true, ApiResultStatusCode.Success, result, null, result.TotalItemCount,
                result.PageNumber, result.PageCount);

        }
        public async Task<ApiResult<PersonListDto>> CreatePersonWithDetail(PersonCuDto input, long creatorId, CancellationToken cancellationToken)
        {
            if (input is null)
                return new ApiResult<PersonListDto>(false, ApiResultStatusCode.CompanyOrIndividualIsRequired, null, null);

            if (input.TypeId != (int)GlobalEnums.PeopleType.Individual && input.TypeId != (int)GlobalEnums.PeopleType.Company)
                return new ApiResult<PersonListDto>(false, ApiResultStatusCode.PersonTypeIsWrong, null, null);

            var personEntity = input.ToEntity(_mapper);
            if (personEntity.FPeopleTypesId == (int)GlobalEnums.PeopleType.Individual)
            {
                if (personEntity.IndividualPerson == null)
                    return new ApiResult<PersonListDto>(false, ApiResultStatusCode.PersonInfoIsRequired, null, null);
            }
            else if (personEntity.FPeopleTypesId == (int)GlobalEnums.PeopleType.Company)
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
        
        public async Task<ApiResult<PersonFullInfoListDto>> GetFullInfo(long id, CancellationToken cancellationToken)
        {
            var dto = await _repository.TableNoTracking.ProjectTo<PersonFullInfoListDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(p => p.Id.Equals(id), cancellationToken);

            if (dto == null)
                return new ApiResult<PersonFullInfoListDto>(false, ApiResultStatusCode.NotFound, null);

            return dto;
        }

        public async Task<ApiResult<PersonListDto>> UpdatePersonWithDetail(PersonCuDto input, long modifierId, CancellationToken cancellationToken)
        {
            if (input is null)
                return new ApiResult<PersonListDto>(false, ApiResultStatusCode.CompanyOrIndividualIsRequired, null, null);

            if (input.TypeId != (int)GlobalEnums.PeopleType.Individual && input.TypeId != (int)GlobalEnums.PeopleType.Company)
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

            if (personEntity.FPeopleTypesId == (int)GlobalEnums.PeopleType.Individual)//individual
            {
                if (personEntity.IndividualPerson is not null)
                {
                    if (input.IndividualPerson.Id == 0)
                        return new ApiResult<PersonListDto>(false, ApiResultStatusCode.BadRequest, null, null);
                }
                else
                    return new ApiResult<PersonListDto>(false, ApiResultStatusCode.PersonInfoIsRequired, null, null);
            }
            else if (personEntity.FPeopleTypesId == (int)GlobalEnums.PeopleType.Company)
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

        //public async Task<ApiResult<PersonListDto>> GetPersonByNationalId(string nationalId, CancellationToken cancellationToken)
        //{
        //    if (String.IsNullOrEmpty(nationalId)) return new ApiResult<PersonListDto>(false, ApiResultStatusCode.NationalCodeIsRequired, null);
        //    if (nationalId.Length == 10)
        //    {
        //        var individual = await _repository.Entities.Where(p => p.IndividualPerson.NationalId == nationalId)
        //            .Include(p => p.IndividualPerson).Include(p => p.PersonType)
        //            .ProjectTo<PersonListDto>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(cancellationToken);
        //        if (individual is null)
        //            return new ApiResult<PersonListDto>(false, ApiResultStatusCode.NotFound, null);

        //        return individual;

        //    }

        //    if (nationalId.Length == 11)
        //    {
        //        var company = await _repository.Entities.Where(p => p.Company.NationalId == nationalId)
        //            .Include(p => p.Company).Include(p => p.PersonType).ProjectTo<PersonListDto>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(cancellationToken);
        //        if (company is null)
        //            return new ApiResult<PersonListDto>(false, ApiResultStatusCode.NotFound, null);

        //        return company;
        //    }

        //    return new ApiResult<PersonListDto>(false, ApiResultStatusCode.NationalCodeFormatIsWrong, null);
        //}
    }
}
