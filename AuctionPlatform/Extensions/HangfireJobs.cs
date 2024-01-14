using AuctionPlatform.Business.Auctions;
using Hangfire;

namespace AuctionPlatform.Extensions
{
    public static class HangfireJobs
    {
        public static void CompleteAuctions()
        {
            Task.Run(() => RecurringJob.AddOrUpdate<IAuctionService>(x => x.CheckAndCompleteAuctions(), Cron.Minutely)).ConfigureAwait(false);
        }
    }
}
