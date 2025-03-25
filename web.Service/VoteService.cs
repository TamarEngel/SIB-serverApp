using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using web.Core.models;
using web.Core.Repositories;
using web.Core.Services;

namespace web.Service
{
    public class VoteService:IVoteService
    {
        private readonly IVoteRepository _voteRepository;

        public VoteService(IVoteRepository voteRepository)
        {
            _voteRepository = voteRepository;
        }
        public async Task<Vote> VoteImageAsync(int userId, int creationId)
        {
            return await _voteRepository.VoteImageAsync(userId, creationId);
        }
        public async Task<bool> DeleteVoteAsync(int userId, int creationId)
        {
            return await _voteRepository.DeleteVoteAsync(userId, creationId);
        }
        public async Task<bool> IsSelfVotingAsync(int creationId, int userId)
        {
            return await _voteRepository.IsSelfVotingAsync(creationId, userId);
        }
        public async Task<bool> HasUserVotedAsync(int userId, int creationId)
        {
            return await _voteRepository.HasUserVotedAsync(userId, creationId);
        }
    }
}
