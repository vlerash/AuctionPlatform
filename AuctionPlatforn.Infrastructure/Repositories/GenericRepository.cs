using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;

namespace AuctionPlatforn.Infrastructure.Repositories;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
{
    public AuctionPlatformDbContext Context;
    public DbSet<TEntity> DbSet { get; }

    public GenericRepository(AuctionPlatformDbContext context)
    {
        Context = context;
        DbSet = Context.Set<TEntity>();
    }

    public void Save() => Context.SaveChanges();

    public async Task SaveAsync() => await Context.SaveChangesAsync();

    #region Unit of Work (Transactions)

    public void UpdateTransact(TEntity entity)
    {
        Context.Entry(entity).State = EntityState.Modified;
    }

    public IList<TEntity> UpdateRangeTransact(IList<TEntity> entities)
    {
        entities.ToList().ForEach(entity => Context.Entry(entity).State = EntityState.Modified);
        return entities;
    }

    public void DeleteTransact(int id)
    {
        var entity = DbSet.Find(id);
        Context.Remove(entity);
    }

    public void DeleteTransactWithEntity(TEntity entity)
    {
        Context.Remove(entity);
    }

    public void DeleteRangeTransact(IList<TEntity> entities)
    {
        Context.RemoveRange(entities);
    }

    public void CreateTransact(TEntity entity)
    {
        Context.Add(entity);
    }

    public async Task CreateAsyncTransact(TEntity entity)
    {
        await Context.AddAsync(entity);
    }

    public IList<TEntity> CreateRanngeTransact(IList<TEntity> entity)
    {
        Context.AddRange(entity);

        return entity;
    }

    #endregion Unit of Work (Transactions)

    public async Task<IList<TEntity>> GetAll()
    {
        return await Context.Set<TEntity>().AsNoTracking().ToListAsync();
    }

    public async Task<TEntity> GetById(int id)
    {
        return await Context.Set<TEntity>().FindAsync(id);
    }
    public async Task<TEntity> GetByIdWithoutTracking(int id)
    {
        var entity = await Context.Set<TEntity>().FindAsync(id);
        Context.Entry(entity).State = EntityState.Detached;
        return entity;
    }
    public async Task<IList<TEntity>> GetAllUndeleted()
    {
        // Class of Entity
        var obj = Expression.Parameter(typeof(TEntity), typeof(TEntity).Name);

        // Search Value
        var searchValue = Expression.Constant(false, typeof(bool));

        // Property of Entity (property to do the search on)
        var objProperty = Expression.PropertyOrField(obj, "IsDeleted");

        // Lambda expression
        var expression = Expression.Equal(objProperty, searchValue);

        var lambda = Expression.Lambda<Func<TEntity, bool>>(expression, obj);

        var compiledLambda = lambda.Compile();

        var searchResult = Context.Set<TEntity>().AsNoTracking().Where(compiledLambda).ToList();

        return searchResult;
    }

    public IList<TEntity> Find(Func<TEntity, bool> predicate)
    {
        return Context.Set<TEntity>().AsNoTracking().Where(predicate).ToList();
    }

    public IList<TEntity> SearchBy(string columName, string searchText)
    {
        Type columNameType;
        object searchTextType;
        Expression<Func<TEntity, bool>> lambda;

        try
        {
            columNameType = typeof(TEntity).GetProperty(columName).PropertyType;
        }
        catch (Exception ex)
        {
            throw new ArgumentNullException("Column Name doesnt exists." + Environment.NewLine + ex.Message);
        }

        try
        {
            searchTextType = Convert.ChangeType(searchText, columNameType);
        }
        catch (Exception ex)
        {
            throw new KeyNotFoundException("Could not convert the type." + Environment.NewLine + ex.Message);
        }

        //Class of Entity
        var obj = Expression.Parameter(typeof(TEntity), typeof(TEntity).Name);

        //Search Value
        var constant = Expression.Constant(searchTextType, columNameType);

        //Property of Entity
        var objProperty = Expression.PropertyOrField(obj, columName);

        if (columNameType == typeof(string))
        {
            //Lambda expression
            var expression = Expression.Call(objProperty, "Contains", null, constant);
            lambda = Expression.Lambda<Func<TEntity, bool>>(expression, obj);
        }
        else
        {
            var expression = Expression.Equal(objProperty, constant);
            lambda = Expression.Lambda<Func<TEntity, bool>>(expression, obj);
        }

        var compiledLambda = lambda.Compile();

        var searchResult = Context.Set<TEntity>().AsNoTracking().Where(compiledLambda).ToList();

        return searchResult;
    }

    public int Count(Func<TEntity, bool> predicate)
    {
        return Context.Set<TEntity>().Count(predicate);
    }

    public IList<TEntity> FindWithTracking(Func<TEntity, bool> predicate)
    {
        return Context.Set<TEntity>().AsNoTracking().Where(predicate).ToList();
    }

    public TEntity Create(TEntity entity)
    {
        Context.Add(entity);
        Save();
        return entity;
    }

    public async Task<TEntity> CreateAsync(TEntity entity)
    {
        Context.Add(entity);
        await SaveAsync();
        return entity;
    }

    public IList<TEntity> CreateRannge(IList<TEntity> entity)
    {
        Context.AddRange(entity);
        Save();
        return entity;
    }

    public async Task<IList<TEntity>> CreateRanngeAsync(IList<TEntity> entity)
    {
        Context.AddRange(entity);
        await SaveAsync();
        return entity;
    }

    public TEntity Update(TEntity entity)
    {
        Context.ChangeTracker.TrackGraph(entity, e => e.Entry.State = EntityState.Modified);
        Save();
        return entity;
    }

    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        Context.ChangeTracker.TrackGraph(entity, e => e.Entry.State = EntityState.Modified);
        await SaveAsync();
        return entity;
    }

    public IList<TEntity> UpdateRange(IList<TEntity> entities)
    {
        entities.ToList().ForEach(entity => Context.Entry(entity).State = EntityState.Modified);
        Save();
        return entities;
    }

    public async Task<IList<TEntity>> UpdateRangeAsync(IList<TEntity> entities)
    {
        entities.ToList().ForEach(entity => Context.Entry(entity).State = EntityState.Modified);

        await SaveAsync();
        return entities;
    }

    public async Task<TEntity> SoftDeleteAsync(TEntity entity)
    {
        dynamic dynEntity = entity;
        dynEntity.IsDeleted = true;

        await SaveAsync();
        return dynEntity;
    }

    public TEntity Delete(int id)
    {
        var entity = DbSet.Find(id);
        if (entity != null)
        {
            Context.Remove(entity);
            Save();
        }

        return entity;
    }

    public async Task<TEntity> DeleteAsync(int id)
    {
        var entity = DbSet.Find(id);
        if (entity != null)
        {
            Context.Remove(entity);

            await SaveAsync();
        }

        return entity;
    }

    public async Task DeleteAsync(TEntity entity)
    {
        Context.Remove(entity);
        await SaveAsync();
    }

    public void DeleteRange(IList<TEntity> entities)
    {
        Context.RemoveRange(entities);
        Save();
    }

    public async Task DeleteRangeAsync(IList<TEntity> entities)
    {
        Context.RemoveRange(entities);
        await SaveAsync();
    }

    public void Dispose()

        {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            if (Context != null)
            {
                Context.Dispose();
                Context = null;
            }
        }
    }

    public IDbContextTransaction BeginTransacition()
    {
        return Context.Database.BeginTransaction();
    }

    IDbContextTransaction IGenericRepository<TEntity>.BeginTransacition()
    {
        throw new NotImplementedException();
    }

    public TEntity UpdateWithoutTracking(TEntity entity)
    {
        Context.ChangeTracker.TrackGraph(entity, e => e.Entry.State = EntityState.Modified);
        Save();
        Context.Entry(entity).State = EntityState.Detached;
        return entity;
    }

}