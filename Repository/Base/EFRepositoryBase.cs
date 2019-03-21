using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TCE.Repository.Interfaces;

namespace TCE.Repository.Base
{
    public class EFRepositoryBase<TEntity> : IEFRepositoryBase<TEntity>, IDisposable where TEntity : class
    {
        //TODO: Resolver como passar dinamicamente o contexto
        private readonly IContextManager _contextManager;
        private readonly IDbContext _dbContext;
        private readonly IDbSet<TEntity> _dbSet;

        protected IDbContext Context
        {
            get { return _dbContext; }
        }

        protected IDbSet<TEntity> DbSet
        {
            get { return _dbSet; }
        }

        //TODO: Resolver como a DI do ContextManager sera passada
        public EFRepositoryBase(IContextManager contextManager)
        {
            _contextManager = contextManager;
            _dbContext = _contextManager.GetContext();
            _dbSet = _dbContext.Set<TEntity>();
        }

        public virtual IQueryable<TEntity> All(bool @readonly = false)
        {
            return @readonly
                ? DbSet.AsNoTracking()
                : DbSet;
        }

        public virtual IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, bool @readonly = false)
        {
            return @readonly
                ? DbSet.Where(predicate).AsNoTracking()
                : DbSet.Where(predicate);
        }

        public virtual IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, bool @readonly = true, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = DbSet;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return @readonly
                ? query.Where(predicate).AsNoTracking()
                : query.Where(predicate);
        }

        public virtual async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, bool @readonly = false)
        {
            return @readonly
                    ? await DbSet.Where(predicate).AsNoTracking().ToListAsync()
                    : await DbSet.Where(predicate).ToListAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, bool @readonly = false, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = DbSet;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return @readonly
                ? await query.Where(predicate).AsNoTracking().ToListAsync()
                : await query.Where(predicate).ToListAsync();
        }

        public virtual TEntity GetSingle(int id)
        {
            return DbSet.Find(id);
        }

        public virtual TEntity GetSingle(Expression<Func<TEntity, bool>> predicate, bool @readonly = false)
        {
            return @readonly
                ? DbSet.Where(predicate).AsNoTracking().FirstOrDefault()
                : DbSet.Where(predicate).FirstOrDefault();
        }

        public virtual async Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> predicate, bool @readonly = false)
        {
            return @readonly
                ? await DbSet.Where(predicate).AsNoTracking().FirstOrDefaultAsync()
                : await DbSet.Where(predicate).FirstOrDefaultAsync();
        }

        public virtual TEntity GetSingle(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = DbSet;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return query.Where(predicate).FirstOrDefault();
        }

        public virtual async Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = DbSet;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return await query.Where(predicate).FirstOrDefaultAsync();
        }

        public virtual TEntity GetSingle(Expression<Func<TEntity, bool>> predicate, bool @readonly = true, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = DbSet;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return @readonly
               ? query.Where(predicate).AsNoTracking().FirstOrDefault()
               : query.Where(predicate).FirstOrDefault();
        }

        public virtual async Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> predicate, bool @readonly = true, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = DbSet;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return @readonly
               ? await query.Where(predicate).AsNoTracking().FirstOrDefaultAsync()
               : await query.Where(predicate).FirstOrDefaultAsync();
        }

        public int Count()
        {
            return DbSet.Count();
        }

        public long LongCount()
        {
            return DbSet.LongCount();
        }

        public async Task<long> LongCountAsync()
        {
            return await DbSet.LongCountAsync();
        }

        public async Task<int> CountAsync()
        {
            return await DbSet.CountAsync();
        }

        public int Count(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.Count(predicate);
        }

        public long LongCount(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.LongCount(predicate);
        }

        public async Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.LongCountAsync(predicate);
        }

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.CountAsync(predicate);
        }

        public virtual void Add(TEntity entity)
        {
            DbSet.Add(entity);
            _dbContext.SaveChanges();
        }

        public virtual async Task AddAsync(TEntity entity)
        {
            DbSet.Add(entity);
            await _dbContext.SaveChangesAsync();
        }

        public virtual void DeleteWhere(Expression<Func<TEntity, bool>> predicate)
        {
            IEnumerable<TEntity> entities = DbSet.Where(predicate);

            foreach (var entity in entities)
            {
                DbSet.Remove(entity);
            }
            _dbContext.SaveChanges();
        }

        public virtual async Task DeleteWhereAsync(Expression<Func<TEntity, bool>> predicate)
        {
            IEnumerable<TEntity> entities = DbSet.Where(predicate);

            foreach (var entity in entities)
            {
                DbSet.Remove(entity);
            }
            await _dbContext.SaveChangesAsync();
        }

        public virtual void Delete(TEntity entity)
        {
            DbSet.Remove(entity);
            _dbContext.SaveChanges();
        }

        public virtual async Task DeleteAsync(TEntity entity)
        {
            DbSet.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public virtual void Update(TEntity entity)
        {
            var entry = Context.Entry(entity);
            DbSet.Attach(entity);
            entry.State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public virtual async Task UpdateAsync(TEntity entity)
        {
            var entry = Context.Entry(entity);
            DbSet.Attach(entity);
            entry.State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing) return;

            if (Context == null) return;
            Context.Dispose();
        }
    }
}