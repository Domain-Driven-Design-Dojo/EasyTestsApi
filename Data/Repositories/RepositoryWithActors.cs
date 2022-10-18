using Common.Utilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Data.Contracts;
using Entities.DatabaseModels.CommonModels.BaseModels;
using Data.ApplicationUtilities;
using System.Linq.Expressions;
using X.PagedList;
using System.Data;
using Oracle.ManagedDataAccess.Client;

namespace Data.Repositories
{
   public class RepositoryWithActors<TEntity, TKey> : IRepositoryWithActors<TEntity, TKey>
        where TEntity : BaseEntityWithActors<TKey>, new()
    {
        protected readonly ApplicationDbContext DbContext;
        public DbSet<TEntity> Entities { get; }
        public virtual IQueryable<TEntity> Table => Entities;
        public virtual IQueryable<TEntity> TableNoTracking => Entities.AsNoTracking();

        public RepositoryWithActors(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
            Entities = DbContext.Set<TEntity>(); // City => Cities
        }

        #region Async Method
        public virtual ValueTask<TEntity> GetByIdAsync(CancellationToken cancellationToken, params object[] ids)
        {
            return Entities.FindAsync(ids, cancellationToken);
        }

        //public virtual ValueTask<TEntity> GetByIdAsync(CancellationToken cancellationToken, TKey id)
        //{
        //    //return Entities.FindAsync(ids, cancellationToken);
        //    return Entities.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
        //}

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


        public virtual async Task AddAsync(TEntity entity, long requestingUserId, CancellationToken cancellationToken, bool saveNow = true)
        {
            entity.AddCreator<TEntity, TKey>(requestingUserId);

            //if (typeof(TEntity).IsAssignableTo(typeof(BaseEntityWithActorsNoIdentity<>)))
            if (typeof(TEntity).IsSubclassOf(typeof(BaseEntityWithActorsNoIdentity)))
            {
                var maxId = Entities.Any() ? Entities.Max(x => x.Id) : (TKey)(object) 0;
                int nextId = Convert.ToInt32(maxId);
                entity.Id = (TKey) (object) (nextId +1) ;
            }

            if (typeof(TEntity).IsSubclassOf(typeof(BaseEntityWithActorsNoIdentityLong)))
            {
                var maxId = Entities.Any() ? Entities.Max(x => x.Id) : (TKey)(object)0L;
                long nextId = Convert.ToInt64(maxId);
                entity.Id = (TKey)(object)(nextId + 1);
            }

            Assert.NotNull(entity, nameof(entity));
            await Entities.AddAsync(entity, cancellationToken).ConfigureAwait(false);
            if (saveNow)
                await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        public virtual async Task AddRangeAsync(IEnumerable<TEntity> entities, long requestingUserId, CancellationToken cancellationToken, bool saveNow = true)
        {
            foreach (var entity in entities)
            {
                entity.AddCreator<TEntity, TKey>(requestingUserId);
            }
            Assert.NotNull(entities, nameof(entities));
            await Entities.AddRangeAsync(entities, cancellationToken).ConfigureAwait(false);
            if (saveNow)
                await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        public virtual async Task UpdateAsync(TEntity entity, long requestingUserId, CancellationToken cancellationToken, bool saveNow = true)
        {
            entity.AddModifier<TEntity, TKey>(requestingUserId);
            Assert.NotNull(entity, nameof(entity));
            Entities.Update(entity);
            if (saveNow)
                await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        public virtual async Task UpdateRangeAsync(IEnumerable<TEntity> entities, long requestingUserId, CancellationToken cancellationToken, bool saveNow = true)
        {
            foreach (var entity in entities)
            {
                entity.AddModifier<TEntity, TKey>(requestingUserId);
            }

            Assert.NotNull(entities, nameof(entities));
            Entities.UpdateRange(entities);
            if (saveNow)
                await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        public virtual async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken, bool saveNow = true)
        {
            Assert.NotNull(entity, nameof(entity));
            Entities.Remove(entity);
            if (saveNow)
                await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        public virtual async Task DeleteRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken, bool saveNow = true)
        {
            Assert.NotNull(entities, nameof(entities));
            Entities.RemoveRange(entities);
            if (saveNow)
                await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }
        #endregion

        #region Sync Methods
        public virtual TEntity GetById(params object[] ids)
        {
            return Entities.Find(ids);
        }

        public virtual void Add(TEntity entity, long requestingUserId, bool saveNow = true)
        {
            entity.AddCreator<TEntity, TKey>(requestingUserId);
            Assert.NotNull(entity, nameof(entity));
            Entities.Add(entity);
            if (saveNow)
                DbContext.SaveChanges();
        }

        public virtual void AddRange(IEnumerable<TEntity> entities, long requestingUserId, bool saveNow = true)
        {
            foreach (var entity in entities)
            {
                entity.AddCreator<TEntity, TKey>(requestingUserId);
            }

            Assert.NotNull(entities, nameof(entities));
            Entities.AddRange(entities);
            if (saveNow)
                DbContext.SaveChanges();
        }

        public virtual void Update(TEntity entity, long requestingUserId, bool saveNow = true)
        {
            entity.AddModifier<TEntity, TKey>(requestingUserId);
            Assert.NotNull(entity, nameof(entity));
            Entities.Update(entity);
            if (saveNow)
                DbContext.SaveChanges();
        }

        public virtual void UpdateRange(IEnumerable<TEntity> entities, long requestingUserId, bool saveNow = true)
        {
            foreach (var entity in entities)
            {
                entity.AddModifier<TEntity, TKey>(requestingUserId);
            }
            Assert.NotNull(entities, nameof(entities));
            Entities.UpdateRange(entities);
            if (saveNow)
                DbContext.SaveChanges();
        }

        public virtual void Delete(TEntity entity, bool saveNow = true)
        {
            Assert.NotNull(entity, nameof(entity));
            Entities.Remove(entity);
            if (saveNow)
                DbContext.SaveChanges();
        }

        public virtual void DeleteRange(IEnumerable<TEntity> entities, bool saveNow = true)
        {
            Assert.NotNull(entities, nameof(entities));
            Entities.RemoveRange(entities);
            if (saveNow)
                DbContext.SaveChanges();
        }
        #endregion

        #region Attach & Detach
        public virtual void Detach(TEntity entity)
        {
            Assert.NotNull(entity, nameof(entity));
            var entry = DbContext.Entry(entity);
            if (entry != null)
                entry.State = EntityState.Detached;
        }

        public virtual void Attach(TEntity entity)
        {
            Assert.NotNull(entity, nameof(entity));
            if (DbContext.Entry(entity).State == EntityState.Detached)
                Entities.Attach(entity);
        }
        #endregion

        #region Explicit Loading
        public virtual async Task LoadCollectionAsync<TProperty>(TEntity entity, Expression<Func<TEntity, IEnumerable<TProperty>>> collectionProperty, CancellationToken cancellationToken)
            where TProperty : class
        {
            Attach(entity);

            var collection = DbContext.Entry(entity).Collection(collectionProperty);
            if (!collection.IsLoaded)
                await collection.LoadAsync(cancellationToken).ConfigureAwait(false);
        }

        public virtual void LoadCollection<TProperty>(TEntity entity, Expression<Func<TEntity, IEnumerable<TProperty>>> collectionProperty)
            where TProperty : class
        {
            Attach(entity);
            var collection = DbContext.Entry(entity).Collection(collectionProperty);
            if (!collection.IsLoaded)
                collection.Load();
        }

        public virtual async Task LoadReferenceAsync<TProperty>(TEntity entity, Expression<Func<TEntity, TProperty>> referenceProperty, CancellationToken cancellationToken)
            where TProperty : class
        {
            Attach(entity);
            var reference = DbContext.Entry(entity).Reference(referenceProperty);
            if (!reference.IsLoaded)
                await reference.LoadAsync(cancellationToken).ConfigureAwait(false);
        }

        public virtual void LoadReference<TProperty>(TEntity entity, Expression<Func<TEntity, TProperty>> referenceProperty)
            where TProperty : class
        {
            Attach(entity);
            var reference = DbContext.Entry(entity).Reference(referenceProperty);
            if (!reference.IsLoaded)
                reference.Load();
        }
        #endregion
    }

    
}
