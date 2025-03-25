using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using web.Core.DTOs;
using web.Core.models;

namespace web.Core.Service
{
    public interface ICreationService
    {
        Task<List<Creation>> GetAllCreationAsync();
        Task<Creation> GetCreationByIdAsync(int id);
        Task<bool> IsValidAddCreationAsync(int userId, int challengeId);
        Task<Creation> AddCreationAsync(CreationPostDTO creation);
        Task<bool> UpdateCreationAsync(int id, CreationPostDTO creation);
        Task<bool> UpdateCreationVoteAsync(int id);
        Task<bool> DecreaseCreationVoteAsync(int id);
        Task<bool> DeleteCreationAsync(int id);
    }
}
