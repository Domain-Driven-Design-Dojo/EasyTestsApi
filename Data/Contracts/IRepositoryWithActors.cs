using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Entities.BaseModels;
using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;
using X.PagedList;

namespace Data.Contracts
{
    public interface IRepositoryWithActors<TEntity, TKey> where TEntity : BaseEntityWithActors<TKey>
   {
      DbSet<TEntity> Entities { get; }
      IQueryable<TEntity> Table { get; }
      IQueryable<TEntity> TableNoTracking { get; }

      void Add(TEntity entity, long requestingUser, bool saveNow = true);
      Task AddAsync(TEntity entity, long requestingUser, CancellationToken cancellationToken, bool saveNow = true);
      void AddRange(IEnumerable<TEntity> entities, long requestingUser, bool saveNow = true);
      Task AddRangeAsync(IEnumerable<TEntity> entities, long requestingUser, CancellationToken cancellationToken, bool saveNow = true);
      void Attach(TEntity entity);
      void Delete(TEntity entity, bool saveNow = true);
      Task DeleteAsync(TEntity entity, CancellationToken cancellationToken, bool saveNow = true);
      void DeleteRange(IEnumerable<TEntity> entities, bool saveNow = true);
      Task DeleteRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken, bool saveNow = true);
      void Detach(TEntity entity);
      TEntity GetById(params object[] ids);
      ValueTask<TEntity> GetByIdAsync(CancellationToken cancellationToken, params object[] ids);
      Task<TEntity> Get(Expression<Func<TEntity, bool>> whereExpr);
      Task<List<TEntity>> GetEntitiesAsync(Expression<Func<TEntity, bool>> whereExpr);
      Task<List<TEntity>> GetEntitiesFromRawSqlAsync(string rawSql, OracleParameter[] parameters);
      Task<IPagedList<TEntity>> GetEntitiesAsync(Expression<Func<TEntity, bool>> whereExpr,
          int pageNumber, int recordsNumber);
      void LoadCollection<TProperty>(TEntity entity, Expression<Func<TEntity, IEnumerable<TProperty>>> collectionProperty) where TProperty : class;
      Task LoadCollectionAsync<TProperty>(TEntity entity, Expression<Func<TEntity, IEnumerable<TProperty>>> collectionProperty, CancellationToken cancellationToken) where TProperty : class;
      void LoadReference<TProperty>(TEntity entity, Expression<Func<TEntity, TProperty>> referenceProperty) where TProperty : class;
      Task LoadReferenceAsync<TProperty>(TEntity entity, Expression<Func<TEntity, TProperty>> referenceProperty, CancellationToken cancellationToken) where TProperty : class;
      void Update(TEntity entity, long requestingUser, bool saveNow = true);
      Task UpdateAsync(TEntity entity, long requestingUser, CancellationToken cancellationToken, bool saveNow = true);
      void UpdateRange(IEnumerable<TEntity> entities, long requestingUser, bool saveNow = true);
      Task UpdateRangeAsync(IEnumerable<TEntity> entities, long requestingUser, CancellationToken cancellationToken, bool saveNow = true);
   }
}
