using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using web.Core.models;

namespace web.Core.Repositories
{
    public interface IChallengeRepository
    {
        Task<List<Challenge>> GetAllChallengesAsync();
        Task<List<Challenge>> GetActiveChallengesAsync();
        Task<List<Challenge>> GetNotActiveChallengesAsync();
        Task<Challenge> GetChallengeByIdAsync(int id);
        Task<List<Challenge>> GetSortChallengeAsync();
        Task<List<Creation>> GetSortCreationsByChallengeAsync(int challengeId);
        Task<Creation> GetWinCreationAsync(int ChallengeId);
        Task<bool> AddChallengeAsync(Challenge challenge);
        Task<bool> UpdateChallengeAsync(int id, Challenge challenge);
        Task<bool> UpdateCountCreationsAsync(int id);
        Task<bool> DeleteChallengeAsync(int id);
        Task<bool> ProcessExpiredChallengesAsync();
    }
}
