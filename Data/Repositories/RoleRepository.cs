using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common;
using Common.Exceptions;
using Common.Utilities;
using Entities.DatabaseModels.UserModels;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class RoleRepository : Repository<AccRole>, IRoleRepository, IScopedDependency
    {
        public RoleRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task AddAsync(AccRole role, CancellationToken cancellationToken)
        {
            var exists = await TableNoTracking.AnyAsync(p => p.Name == role.Name, cancellationToken: cancellationToken);
            if (exists)
                throw new BadRequestException(ApiResultStatusCode.NameIsExists.ToDisplay());

            await base.AddAsync(role, cancellationToken);
        }
    }

    public interface IRoleRepository
    {
        Task AddAsync(AccRole role, CancellationToken cancellationToken);
    }
}
