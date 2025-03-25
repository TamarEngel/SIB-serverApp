using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using web.Core.models;
using web.Core.Repositories;
using static System.Net.Mime.MediaTypeNames;

namespace web.Data.Repositories
{
    public class CreationRepository:ICreationRepository
    {
        private readonly DataContext _context;

        public CreationRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Creation>> GetAllCreationAsync()
        {
            return await _context.CreationList.ToListAsync();
        }
        public async Task<Creation> GetCreationByIdAsync(int id)
        {
            return await _context.CreationList.FirstOrDefaultAsync(c => c.Id == id);
        }
        public async Task<bool> IsValidAddCreationAsync(int userId,int  challengeId)
        {
            var existingCreation = await _context.CreationList
            .FirstOrDefaultAsync(c => c.UserId == userId && c.ChallengeId == challengeId);

            if (existingCreation != null)
                return false;
            return true;
        }
        public async Task<Creation> AddCreationAsync(Creation creation)
        {
            var currentUser = await _context.UserList.FirstOrDefaultAsync(u => u.UserId == creation.UserId);
            var currentChallenge = await _context.ChallengeList.FirstOrDefaultAsync(c => c.Id == creation.ChallengeId);
            if (currentUser == null || currentChallenge == null )
                return null;

            creation.UserId = currentUser.Id;
            currentChallenge.CountCreations = currentChallenge.CountCreations + 1;

            await _context.CreationList.AddAsync(creation);
            await _context.SaveChangesAsync();
            return creation;
        }

        public async Task<bool> UpdateCreationAsync(int id, Creation creation)
        {
            var currentCreation = await GetCreationByIdAsync(id);
            if (currentCreation != null)
            {
                currentCreation.Votes = creation.Votes;
                currentCreation.ImageUrl = creation.ImageUrl;
                //currentCreation.UserId = creation.UserId;
                //currentCreation.ChallengeId = creation.ChallengeId;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateCreationVoteAsync(int id)
        {
            var currentCreation = await GetCreationByIdAsync(id);
            if (currentCreation == null)
                return false;
            currentCreation.Votes = currentCreation.Votes + 1;
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DecreaseCreationVoteAsync(int id)
        {
            var currentCreation = await GetCreationByIdAsync(id);
            if (currentCreation == null)
                return false;
            currentCreation.Votes = currentCreation.Votes - 1;
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteCreationAsync(int id)
        {
            var currentCreation = await GetCreationByIdAsync(id);
            if (currentCreation == null)
                return false;
            currentCreation.IsDeleted = true;
            var currentChallenge = await _context.ChallengeList.FirstOrDefaultAsync(c => c.Id == currentCreation.ChallengeId);
            currentChallenge.CountCreations = currentChallenge.CountCreations - 1;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
