using Microsoft.EntityFrameworkCore.Storage;

namespace AuctionPlatforn.Infrastructure.Repositories
{
    public interface IGenericRepository<TEntity> : IDisposable where TEntity : class
    {
        TEntity Create(TEntity entity);

        Task<TEntity> CreateAsync(TEntity entity);
        void CreateTransact(TEntity entity);
        IList<TEntity> CreateRannge(IList<TEntity> entity);

        Task<IList<TEntity>> CreateRanngeAsync(IList<TEntity> entity);
        IList<TEntity> CreateRanngeTransact(IList<TEntity> entity);

        Task CreateAsyncTransact(TEntity entity);

        Task<IList<TEntity>> GetAll();
        Task<IList<TEntity>> GetAllUndeleted();

        Task<TEntity> GetById(int id);

        Task<TEntity> GetByIdWithoutTracking(int id);

        IList<TEntity> Find(Func<TEntity, bool> predicate);
        IList<TEntity> FindWithTracking(Func<TEntity, bool> predicate);

        void UpdateTransact(TEntity entity);

        IList<TEntity> UpdateRangeTransact(IList<TEntity> entities);

        TEntity Update(TEntity entity);
        TEntity UpdateWithoutTracking(TEntity entity);

        Task<TEntity> UpdateAsync(TEntity entity);

        IList<TEntity> UpdateRange(IList<TEntity> entities);

        Task<IList<TEntity>> UpdateRangeAsync(IList<TEntity> entities);

        void DeleteTransact(int id);
        void DeleteTransactWithEntity(TEntity entity);
        void DeleteRangeTransact(IList<TEntity> entities);
        TEntity Delete(int id);
        Task<TEntity> DeleteAsync(int id);

        Task DeleteAsync(TEntity entity);

        void DeleteRange(IList<TEntity> entities);

        Task DeleteRangeAsync(IList<TEntity> entities);
        Task<TEntity> SoftDeleteAsync(TEntity entity);

        IList<TEntity> SearchBy(string columName, string searchText);

        int Count(Func<TEntity, bool> predicate);

        void Save();

        Task SaveAsync();

        IDbContextTransaction BeginTransacition();
    }
}
