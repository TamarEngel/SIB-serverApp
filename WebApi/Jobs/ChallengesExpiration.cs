using web.Core.Service;

namespace Web.Api.Jobs
{
    public class ChallengesExpiration : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<ChallengesExpiration> _logger;

        public ChallengesExpiration(IServiceScopeFactory serviceScopeFactory, ILogger<ChallengesExpiration> logger)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var timer = new PeriodicTimer(TimeSpan.FromMinutes(1)); // בדיקה כל 15 דקות

            while (await timer.WaitForNextTickAsync(stoppingToken))
            {
                try
                {
                    using var scope = _serviceScopeFactory.CreateScope();
                    var challengeService = scope.ServiceProvider.GetRequiredService<IChallengeService>();

                    _logger.LogInformation("🔍 Checking for expired challenges...");
                    await challengeService.ProcessExpiredChallengesAsync();
                    _logger.LogInformation("✅ Finished processing expired challenges.");
                }
                catch (OperationCanceledException)
                {
                    _logger.LogInformation("🛑 ChallengeExpirationJob stopped.");
                    break;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "❌ Error processing expired challenges");
                }
            }
        }
    }
}