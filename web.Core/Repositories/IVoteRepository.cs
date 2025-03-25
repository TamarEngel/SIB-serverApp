using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using web.Core.models;

namespace web.Core.Repositories
{
    public interface IVoteRepository
    {
        Task<Vote> VoteImageAsync(int userId, int creationId);
        Task<bool> DeleteVoteAsync(int userId, int creationId);
        Task<bool> IsSelfVotingAsync(int creationId, int userId);
        Task<bool> HasUserVotedAsync(int userId, int creationId);

    }
}
