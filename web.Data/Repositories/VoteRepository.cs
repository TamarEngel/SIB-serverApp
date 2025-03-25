using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using web.Core.models;
using web.Core.Repositories;

namespace web.Data.Repositories
{
    public class VoteRepository:IVoteRepository
    {
        private readonly DataContext _dataContext;
        public VoteRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<Vote> VoteImageAsync(int userId, int creationId)
        {
            // בדיקה אם המשתמש כבר הצביע לתמונה הזאת
            var existingVote = await _dataContext.VotesList
                .FirstOrDefaultAsync(v => v.UserId == userId && v.CreationId == creationId);

            if (existingVote != null)
            {
                throw new InvalidOperationException("User has already voted for this image.");
            }

            var vote = new Vote
            {
                UserId = userId,
                CreationId = creationId,
                VotedAt = DateTime.UtcNow
            };

            await _dataContext.VotesList.AddAsync(vote);

            // עדכון ספירת ההצבעות בתמונה
            var creation = await _dataContext.CreationList.FirstOrDefaultAsync(i => i.Id == creationId);
            if (creation != null)
            {
                creation.Votes++;
                _dataContext.CreationList.Update(creation);
            }

            await _dataContext.SaveChangesAsync();
            return vote;
        }
        public async Task<bool> DeleteVoteAsync(int userId, int creationId)
        {
            var vote = await _dataContext.VotesList.FirstOrDefaultAsync(v => v.UserId == userId && v.CreationId == creationId);
            if (vote == null)
                return false;
            var creation = await _dataContext.CreationList.FirstOrDefaultAsync(i => i.Id == creationId);
            if (creation != null)
            {
                creation.Votes--;
                _dataContext.CreationList.Update(creation);
            }
            _dataContext.VotesList.Remove(vote);
            return await _dataContext.SaveChangesAsync() > 0;
        }
        public async Task<bool> IsSelfVotingAsync(int creationId, int userId)
        {
            return await _dataContext.CreationList
                .AnyAsync(i => i.Id == creationId && i.UserId == userId);
        }
        public async Task<bool> HasUserVotedAsync(int userId, int creationId)
        {
            return await _dataContext.VotesList.AnyAsync(v => v.UserId == userId && v.CreationId == creationId);
        }
    }
}
