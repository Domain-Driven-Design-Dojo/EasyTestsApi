using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data.Contracts;
using DataTransferObjects.DTOs.V1.Persons;
using Entities.PersonModels;
using Microsoft.EntityFrameworkCore;
using Services.ServicesContracts.V2.Persons;

namespace Services.Services.V2.Persons
{


    public class PersonTypeService : IPersonsTypeService
    {
        private readonly IRepository<CrePersonType> _repository;
        private readonly IMapper _mapper;

        public PersonTypeService(IRepository<CrePersonType> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<List<PersonsTypeListDto>> GetPersonsTypeListAsync(CancellationToken cancellationToken)
        {
            var PersonsTypes = await _repository.TableNoTracking
               .ProjectTo<PersonsTypeListDto>(_mapper.ConfigurationProvider).ToListAsync();
            return PersonsTypes;
        }
    }
}
