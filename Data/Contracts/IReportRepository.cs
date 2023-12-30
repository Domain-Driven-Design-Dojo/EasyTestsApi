using Entities.BaseModels;
using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using X.PagedList;

namespace Data.Contracts
{
    public interface IReportRepository<TEntity, TKey> where TEntity : BaseEntityWithActors<TKey>
    {
        DbSet<TEntity> Entities { get; }
        IQueryable<TEntity> TableNoTracking { get; }
        TEntity GetById(params object[] ids);
        ValueTask<TEntity> GetByIdAsync(CancellationToken cancellationToken, params object[] ids);
        Task<TEntity> Get(Expression<Func<TEntity, bool>> whereExpr);
        Task<List<TEntity>> GetEntitiesAsync(Expression<Func<TEntity, bool>> whereExpr);
        Task<List<TEntity>> GetEntitiesFromRawSqlAsync(string rawSql, OracleParameter[] parameters);
        Task<IPagedList<TEntity>> GetEntitiesAsync(Expression<Func<TEntity, bool>> whereExpr,
            int pageNumber, int recordsNumber);
    }
}
