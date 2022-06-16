using Microsoft.EntityFrameworkCore;
using Relex.Interview.Entities;

namespace Relex.Interview.Data.Contracts
{
    public interface IRepository<TEntity> where TEntity : class, IEntity
    {
        DbSet<TEntity> Entities { get; }
        IQueryable<TEntity> Table { get; }
        IQueryable<TEntity> TableNoTracking { get; }

        void Add(TEntity entity);
        Task AddAsync(TEntity entity, CancellationToken cancellationToken);
        void AddRange(IEnumerable<TEntity> entities);
        Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken);
        void Delete(TEntity entity);
        Task DeleteAsync(TEntity entity, CancellationToken cancellationToken);
        TEntity GetById(int id);
        Task<TEntity> GetByIdAsync(int id,CancellationToken cancellationToken);
        IEnumerable<TEntity> GetAll();
        Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken);   
        void Update(TEntity entity);
        Task UpdateAsync(TEntity entity, CancellationToken cancellationToken);
        void UpdateRange(IEnumerable<TEntity> entities);
        Task UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken);

        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
