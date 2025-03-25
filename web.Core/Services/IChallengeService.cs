using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using web.Core.DTOs;
using web.Core.models;

namespace web.Core.Service
{
    public interface IChallengeService
    {
        Task<List<Challenge>> GetAllChallengesAsync();
        Task<List<Challenge>> GetActiveChallengesAsync();
        Task<List<Challenge>> GetNotActiveChallengesAsync();
        Task<Challenge> GetChallengeByIdAsync(int id);
        Task<List<Challenge>> GetSortChallengeAsync();
        Task<List<Creation>> GetSortCreationsByChallengeAsync(int challengeId);
        Task<Creation> GetWinCreationAsync(int challengeId);
        Task<bool> AddChallengeAsync(ChallengePostDTO challenge);
        Task<bool> UpdateChallengeAsync(int id, ChallengePostDTO challenge);
        Task<bool> UpdateCountCreationsAsync(int id);
        Task<bool> DeleteChallengeAsync(int id);
        Task<bool> ProcessExpiredChallengesAsync();
    }
}
