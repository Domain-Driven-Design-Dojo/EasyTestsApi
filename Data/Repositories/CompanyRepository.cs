using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common;
using Data.Contracts;
using Entities.PersonModels;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class CompanyRepository : Repository<CreCompany>, ICompanyRepository, IScopedDependency
    {
        public CompanyRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<CreCompany> GetCompanyByNationalId(string nationalId, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(p => p.NationalId.Equals(nationalId)).SingleOrDefaultAsync(cancellationToken);
        }
    }
}
