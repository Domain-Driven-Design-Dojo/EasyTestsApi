using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common;
using Common.Exceptions;
using Data.Contracts;
using DataTransferObjects.DataTransferObjects.HrDTOs;
using Entities.DatabaseModels.HrModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.IServices;
using WebFramework.Api;

//using System.Xml.Linq;

namespace Api.Controllers.V1.People
{
    [ApiVersion("1")]
    public class PeopleController : CrudControllerWithActors<PersonDto, PersonSelectDto, CrePerson, long>
    {
        private readonly IIndividualsService _individualsService;
        private readonly IPeopleService _peopleService;
        private readonly IMapper _mapper;

        public PeopleController(IRepositoryWithActors<CrePerson, long> repository, 
            IMapper mapper,
            IIndividualsService individualsService,
            IPeopleService peopleService) : base(repository, mapper)
        {
            _individualsService = individualsService;
            _peopleService = peopleService;
            _mapper = mapper;
        }

        [HttpPost("CreateIndividual")]
        public IndividualDto CreateIndividual(IndividualDto input,
            CancellationToken cancellationToken)
        {
            var individual = _individualsService.CreateIndividual(_mapper.Map(input, new CreIndividual()),
                User.Identity.GetUserId(), cancellationToken);
            IndividualDto result = new IndividualDto();

            _mapper.Map(individual.Result, result);
            return result;
        }

        [HttpPost("CreatePerson")]
        public PersonDto CreatePerson(PersonDto input,
            CancellationToken cancellationToken)
        {
            var person = _peopleService.CreatePerson(_mapper.Map(input, new CrePerson()),
                User.Identity.GetUserId(), cancellationToken);

            PersonDto result = new PersonDto();

            _mapper.Map(person.Result, result);
            return result;
        }

        
        //[HttpPost("GetPersonByNationalId")]
        //public async Task<PersonDto> GetPersonByNationalIdAsync(PersonDto person, CancellationToken cancellationToken)
        //{
            

        //    //1	حقیقی
        //    if (!string.IsNullOrEmpty(person.IndividualPerson?.NationalId))
        //    {
        //        if (person.IndividualPerson is null)
        //            throw new AppException(ApiResultStatusCode.BadRequest);

        //        var response = await EntityFrameworkQueryableExtensions.FirstOrDefaultAsync<PersonDto>
        //            (Extensions.ProjectTo<PersonDto>(Repository.TableNoTracking, Mapper.ConfigurationProvider),
        //            p => p.IndividualPerson.NationalId.Equals(person.IndividualPerson.NationalId), cancellationToken);
        //        if (response is null)
        //            throw new AppException(ApiResultStatusCode.NotFound);
        //        return response;
        //    }

        //    //2 حقوقی
        //    if (!string.IsNullOrEmpty(person.Company?.NationalId))
        //    {
        //        if (person.Company is null)
        //            throw new AppException(ApiResultStatusCode.BadRequest);

        //        var response = await EntityFrameworkQueryableExtensions.FirstOrDefaultAsync<PersonDto>
        //            (Extensions.ProjectTo<PersonDto>(Repository.TableNoTracking, Mapper.ConfigurationProvider),
        //            p => p.Company.NationalId.Equals(person.Company.NationalId), cancellationToken);
        //        if (response is null)
        //            throw new AppException(ApiResultStatusCode.NotFound);
        //        return response;
        //    }

        //    throw new AppException(ApiResultStatusCode.BadRequest,"National id is not provided.");
        //}

        //[HttpGet("GetPersonByNationalIdString")]
        //public async Task<PersonListDto> GetPersonByNationalIdAsync(string nationalId, CancellationToken cancellationToken)
        //{

        //    if(String.IsNullOrEmpty(nationalId))
        //        throw new AppException(ApiResultStatusCode.NationalCodeIsRequired);

        //    var person = await _peopleService.GetPersonByNationalId(nationalId, cancellationToken);
               
        //    var response = _mapper.Map<PersonListDto>(person);

        //    return response;

        //}

       


    }
}
