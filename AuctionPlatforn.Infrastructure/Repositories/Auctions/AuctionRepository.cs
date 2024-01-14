using AuctionPlatform.Domain.Entities;
using AuctionPlatform.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System;

namespace AuctionPlatforn.Infrastructure.Repositories.Auctions
{
    public class AuctionRepository : GenericRepository<Auction>, IAuctionRepository
    {
        public AuctionRepository(AuctionPlatformDbContext context) : base(context)
        {
        }

        public async Task<IList<Auction>> GetCurrentAuctionsByTimeLeftAscending()
        {
            return await DbSet.AsNoTracking()
                              .Where(a => a.AuctionStatus == AuctionStatusEnum.Open && a.EndTime > DateTime.Now)
                              .Include(x => x.User)
                              .Include(x => x.HighestBidder)
                              .Include(x => x.Bids)
                              .OrderBy(a => a.EndTime)
                              .ToListAsync();
        }
        
        public async Task<IList<Auction>> GetEndedAuctions()
        {
            return await DbSet.AsNoTracking()
                              .Where(a => a.AuctionStatus == AuctionStatusEnum.Open && a.EndTime <= DateTime.Now)
                              .Include(x => x.User)
                              .Include(x => x.HighestBidder)
                              .Include(x => x.Bids)
                              .ToListAsync();
        }

        public async Task<Auction> GetAuctionById(int auctionId)
        {
            return await DbSet
                .Include(x => x.User) 
                .Include(x => x.HighestBidder) 
                .Include(x => x.Bids) 
                .FirstOrDefaultAsync(a => a.Id == auctionId);
        }
    }
}
