namespace AuctionPlatforn.Infrastructure.Repositories._00_UnitOfWork
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
        void Complete();
    }
}
