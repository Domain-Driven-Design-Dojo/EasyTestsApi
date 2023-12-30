using Data.Contracts;
using Entities.BaseModels;
using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using X.PagedList;

namespace Data.Repositories
{
    public class ReportRepository<TEntity, TKey> : IReportRepository<TEntity, TKey>
        where TEntity : BaseEntityWithActors<TKey>, new()
    {
        protected readonly ApplicationDbContext DbContext;
        public DbSet<TEntity> Entities { get; }

        public IQueryable<TEntity> TableNoTracking => Entities.AsNoTracking();

        public ReportRepository(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
            Entities = DbContext.Set<TEntity>(); 
        }

        #region Async Method
        public virtual ValueTask<TEntity> GetByIdAsync(CancellationToken cancellationToken, params object[] ids)
        {
            return Entities.FindAsync(ids, cancellationToken);
        }


        public virtual async Task<TEntity> Get(Expression<Func<TEntity, bool>> whereExpr)
        {
            IQueryable<TEntity> query = Entities.AsNoTracking();
            //foreach (var navigationExpr in navigationExprs)
            //{
            //    query = query.Include(navigationExpr);
            //}

            query = query.Where(whereExpr);

            var entities = await query.ToListAsync();
            return entities.FirstOrDefault();
        }

        public async Task<List<TEntity>> GetEntitiesAsync(Expression<Func<TEntity, bool>> whereExpr)
        {
            IQueryable<TEntity> query = Entities.AsNoTracking();
            //foreach (var navigationExpr in navigationExprs)
            //{
            //    query = query.Include(navigationExpr);
            //}

            query = query.Where(whereExpr);

            var entities = await query.ToListAsync();
            return entities;
        }

        public async Task<List<TEntity>> GetEntitiesFromRawSqlAsync(string rawSql, OracleParameter[] parameters)
        {
            //IQueryable<TEntity> query = Entities.FromSqlRaw(rawSql).AsNoTracking();
            //List<TEntity> entities = await query.ToListAsync();

            var res = await Entities.FromSqlRaw(rawSql, parameters).ToListAsync();
            return res;
        }

        public virtual async Task<IPagedList<TEntity>> GetEntitiesAsync(Expression<Func<TEntity, bool>> whereExpr,
              int pageNumber, int recordsNumber)
        {
            IQueryable<TEntity> query = Entities.AsNoTracking();
            //foreach (var navigationExpr in navigationExprs)
            //{
            //    query = query.Include(navigationExpr);
            //}

            query = query.Where(whereExpr);

            //var entities = await query.ToListAsync();
            var entities = await query.ToPagedListAsync(pageNumber, recordsNumber);
            return entities;
        }


        #endregion

        #region Sync Methods
        public virtual TEntity GetById(params object[] ids)
        {
            return Entities.Find(ids);
        }

        #endregion
    }
}
