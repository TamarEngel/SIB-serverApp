using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using web.Data;

namespace web.Service
{
    //public class ChallengeStatusUpdaterService : IHostedService, IDisposable
    //{
    //    private Timer _timer;
    //    private readonly DataContext _context; // החלף את YourDbContext עם הקונטקסט שלך

    //    public ChallengeStatusUpdaterService(DataContext context)
    //    {
    //        _context = context;
    //        Console.WriteLine("זה פועלללל");
    //    }

    //    public Task StartAsync(CancellationToken cancellationToken)
    //    {
    //        _timer = new Timer(CheckChallenges, null, TimeSpan.Zero, TimeSpan.FromDays(1)); // כל יום
    //        return Task.CompletedTask;
    //    }

    //    private void CheckChallenges(object state)
    //    {
    //        var expiredChallenges = _context.ChallengeList
    //            .Where(c => c.EndDate <= DateTime.UtcNow && !c.IsDeleted)
    //            .ToList();

    //        foreach (var challenge in expiredChallenges)
    //        {
    //            challenge.IsDeleted = true; // או כל פעולה אחרת שאתה רוצה לבצע
    //        }

    //        _context.SaveChanges();
    //    }

    //    public Task StopAsync(CancellationToken cancellationToken)
    //    {
    //        _timer?.Change(Timeout.Infinite, 0);
    //        return Task.CompletedTask;
    //    }

    //    public void Dispose()
    //    {
    //        _timer?.Dispose();
    //    }
    //}
    //}

    public class ChallengeStatusUpdaterService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public ChallengeStatusUpdaterService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            Console.WriteLine("hhhhhhh");

        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await UpdateChallengesStatus();
                await Task.Delay(TimeSpan.FromDays(1), stoppingToken); // מחכה 24 שעות
            }
        }

        private async Task UpdateChallengesStatus()
        {
            // יצירת scope חדש
            using (var scope = _serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();

                var expiredChallenges = dbContext.ChallengeList
                    .Where(c => c.EndDate <= DateTime.UtcNow && c.IsDeleted == false)
                    .ToList();

                foreach (var challenge in expiredChallenges)
                {
                    challenge.IsDeleted = true;
                }

                await dbContext.SaveChangesAsync();
            }
        }
    }
}

