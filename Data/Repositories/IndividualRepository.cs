using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common;
using Common.Utilities;
using Data.Contracts;
using Entities.PersonModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Data.Repositories
{
    public class IndividualRepository : Repository<CreIndividual>,IIndividualRepository, IScopedDependency
    {
        public IndividualRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {
        }

    }
}
