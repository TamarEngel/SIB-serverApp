using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using web.Core.models;

namespace web.Core.Repositories
{
    public interface ICreationRepository
    {
        Task<List<Creation>> GetAllCreationAsync();
        Task<Creation> GetCreationByIdAsync(int id);
        Task<bool> IsValidAddCreationAsync(int userId, int challengeId);
        Task<Creation> AddCreationAsync(Creation creation);
        Task<bool> UpdateCreationAsync(int id, Creation creation);
        Task<bool> DecreaseCreationVoteAsync(int id);
        Task<bool> UpdateCreationVoteAsync(int id);
        Task<bool> DeleteCreationAsync(int id);
    }
}
