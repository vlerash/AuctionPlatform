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

        public async Task<IList<Auction>> GetActiveAuctions()
        {
            return await DbSet.AsNoTracking()
                              .Where(a => a.AuctionStatus == AuctionStatusEnum.Open && a.EndTime > DateTime.Now)
                              .Include(x => x.User)
                              .Include(x => x.HighestBidder)
                              .OrderBy(x=>x.Id)
                              .ToListAsync();
        }

        public async Task<IList<Auction>> GetCurrentAuctionsByTimeLeftAscending()
        {
            return await DbSet.AsNoTracking()
                              .Where(a => a.AuctionStatus == AuctionStatusEnum.Open && a.EndTime > DateTime.Now)
                              .Include(x => x.User)
                              .Include(x => x.HighestBidder)
                              .OrderBy(a => a.EndTime)
                              .ToListAsync();
        }
        
        public async Task<IList<Auction>> GetEndedAuctions()
        {
            return await DbSet.AsNoTracking()
                              .Where(a => a.AuctionStatus == AuctionStatusEnum.Open && a.EndTime <= DateTime.Now)
                              .Include(x => x.User)
                              .Include(x => x.HighestBidder)
                              .ToListAsync();
        }

        public async Task<Auction> GetAuctionById(int auctionId)
        {
            return await DbSet
                .Include(x => x.User) 
                .Include(x => x.HighestBidder) 
                .FirstOrDefaultAsync(a => a.Id == auctionId);
        }

    }
}
