using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data.Contracts;
using DataTransferObjects.DataTransferObjects.HrDTOs;
using Entities.DatabaseModels.HrModels;
using Microsoft.EntityFrameworkCore;
using Services.IServices.V2;

namespace Services.Services.V2
{


    public class PeopleTypeService : IPeopleTypeService
    {
        private readonly IRepository<CrePeopleType> _repository;
        private readonly IMapper _mapper;

        public PeopleTypeService(IRepository<CrePeopleType> repository, IMapper mapper)
        {
            this._repository = repository;
            this._mapper = mapper;
        }
        public async Task<List<PeopleTypeListDto>> GetPeopleTypeListAsync(CancellationToken cancellationToken)
        {
            var PeopleTypes = await _repository.TableNoTracking
               .ProjectTo<PeopleTypeListDto>(_mapper.ConfigurationProvider).ToListAsync();
            return PeopleTypes;
        }
    }
}
