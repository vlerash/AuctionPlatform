namespace AuctionPlatforn.Infrastructure.Repositories._00_UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AuctionPlatformDbContext _context;

        public UnitOfWork(AuctionPlatformDbContext context)
        {
            _context = context;
        }

        public void Complete()
        {
            _context.SaveChanges();
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
