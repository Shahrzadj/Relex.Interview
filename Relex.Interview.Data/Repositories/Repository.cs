using Microsoft.EntityFrameworkCore;
using Relex.Interview.Common.Utilities;
using Relex.Interview.Data.Contracts;
using Relex.Interview.Entities;

namespace Relex.Interview.Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class, IEntity
    {
        protected readonly ApplicationDbContext DbContext;
        public DbSet<TEntity> Entities { get; }
        public virtual IQueryable<TEntity> Table => Entities;
        public virtual IQueryable<TEntity> TableNoTracking => Entities.AsNoTracking();

        public Repository(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
            Entities = DbContext.Set<TEntity>();
        }

        public virtual async Task<TEntity> GetByIdAsync(int id,CancellationToken cancellationToken)
        {
            return await Entities.SingleOrDefaultAsync(i => i.Id == id,cancellationToken);
        }
        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await Entities.ToListAsync(cancellationToken);
        }
        public virtual  IEnumerable<TEntity> GetAll()
        {
            return  Entities.ToList();
        }
        public virtual async Task AddAsync(TEntity entity, CancellationToken cancellationToken)
        {
            Assert.NotNull(entity, nameof(entity));
            await Entities.AddAsync(entity, cancellationToken).ConfigureAwait(false);
        }

        public virtual async Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken)
        {
            Assert.NotNull(entities, nameof(entities));
            await Entities.AddRangeAsync(entities, cancellationToken).ConfigureAwait(false);
        }

        public virtual async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken)
        {
            Assert.NotNull(entity, nameof(entity));
            Entities.Update(entity);
        }

        public virtual async Task UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken)
        {
            Assert.NotNull(entities, nameof(entities));
            Entities.UpdateRange(entities);
        }

        public virtual async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken)
        {
            Assert.NotNull(entity, nameof(entity));
            Entities.Remove(entity);
        }

        public virtual async Task DeleteRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken)
        {
            Assert.NotNull(entities, nameof(entities));
            Entities.RemoveRange(entities);
        }

        public virtual TEntity GetById(int id)
        {
            return Entities.Find(id);
        }

        public virtual void Add(TEntity entity)
        {
            Assert.NotNull(entity, nameof(entity));
            Entities.Add(entity);
        }

        public virtual void AddRange(IEnumerable<TEntity> entities)
        {
            Assert.NotNull(entities, nameof(entities));
            Entities.AddRange(entities);
        }

        public virtual void Update(TEntity entity)
        {
            Assert.NotNull(entity, nameof(entity));
            Entities.Update(entity);
        }

        public virtual void UpdateRange(IEnumerable<TEntity> entities)
        {
            Assert.NotNull(entities, nameof(entities));
            Entities.UpdateRange(entities);
        }

        public virtual void Delete(TEntity entity)
        {
            Assert.NotNull(entity, nameof(entity));
            Entities.Remove(entity);
        }

        public int SaveChanges()
        {
            return DbContext.SaveChanges();
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return DbContext.SaveChangesAsync(cancellationToken);
        }
    }
}