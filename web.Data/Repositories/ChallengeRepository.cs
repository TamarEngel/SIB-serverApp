using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using web.Core.interfaces;
using web.Core.models;
using web.Core.Repositories;
using web.Core.Services;
using static System.Net.Mime.MediaTypeNames;

namespace web.Data.Repositories
{
    public class ChallengeRepository:IChallengeRepository
    {
        private readonly DataContext _context;
        private readonly IEmailWinnerService _emailService;

        public ChallengeRepository(DataContext context, IEmailWinnerService emailService)
        {
            _context = context;
            _emailService = emailService;
        }
        public async Task<List<Challenge>> GetAllChallengesAsync()
        {
            return await _context.ChallengeList.Include(c => c.ChallengeCreationList).ToListAsync();
        }
        public async Task<List<Challenge>> GetActiveChallengesAsync()
        {
            return await _context.ChallengeList.Where(c => !c.IsDeleted).Include(c => c.ChallengeCreationList).ToListAsync();
        }
        public async Task<List<Challenge>> GetNotActiveChallengesAsync()
        {
            return await _context.ChallengeList.Where(c => c.IsDeleted).Include(c => c.ChallengeCreationList).ToListAsync();
        }
        public async Task<Challenge> GetChallengeByIdAsync(int id)
        {
            return await _context.ChallengeList.Include(c => c.ChallengeCreationList).FirstOrDefaultAsync(challenge => challenge.Id == id);
        }

        public async Task<List<Creation>> GetSortCreationsByChallengeAsync(int challengeId)
        {
            var challenge = await GetChallengeByIdAsync(challengeId);
            var creationList = challenge.ChallengeCreationList.OrderByDescending(c => c.Votes).ToList();

            return creationList;
        }
        public async Task<List<Challenge>> GetSortChallengeAsync()
        {
            var challenges = await _context.ChallengeList
                            .OrderByDescending(c => c.CreatedAt)
                            .ToListAsync();
            return challenges;
        }

        public async Task<Creation> GetWinCreationAsync(int ChallengeId)
        {
            var creationList = await GetSortCreationsByChallengeAsync(ChallengeId);
            var topVoteCreation = creationList.FirstOrDefault();
            return topVoteCreation;
        }

        public async Task<bool> AddChallengeAsync(Challenge challenge)
        {
            if (await _context.ChallengeList.AnyAsync(c => c.Id == challenge.Id))
                return false;
            await _context.ChallengeList.AddAsync(challenge);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateChallengeAsync(int id, Challenge challenge)
        {
            var currentChallenge = await GetChallengeByIdAsync(id);
            if (currentChallenge != null)
            {
                currentChallenge.Title = challenge.Title;
                currentChallenge.Description = challenge.Description;
                currentChallenge.CountCreations = challenge.CountCreations;

                //currentChallenge.StartDate = challenge.StartDate;
                //currentChallenge.EndDate = challenge.EndDate;
                //currentChallenge.ChallengeCreationList = challenge.ChallengeCreationList;

                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<bool> UpdateCountCreationsAsync(int id)
        {
            var currentChallenge = await GetChallengeByIdAsync(id);
            if (currentChallenge == null)
                return false;
            currentChallenge.CountCreations = currentChallenge.CountCreations + 1;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteChallengeAsync(int id)
        {
            var challenge = await GetChallengeByIdAsync(id);
            if (challenge == null)
                return false;
            challenge.IsDeleted = true;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ProcessExpiredChallengesAsync()
        {
            var expiredChallenges =  await _context.ChallengeList
                    .Where(c => c.EndDate <= DateTime.UtcNow && c.IsDeleted == false)
                    .ToListAsync(); 
            if (expiredChallenges.Count < 0)
                return false;
            foreach(var challenge in expiredChallenges)
            {
                challenge.IsDeleted = true;

                var winCreation = await GetWinCreationAsync(challenge.Id);
                var userId = winCreation.UserId;//10
                var winner = _context.UserList.FirstOrDefault(u => u.Id == userId);
                if (winner == null) continue; // אם אין מנצח, ממשיכים הלאה

                var subject = "You won the challenge!";
                var body = $"Congratulations {winner.Name}! You won the challenge '{challenge.Title}' 🎉";

                await _emailService.SendEmailAsync(winner.Email, subject, body);

                
                //challenge.IsWinnerEmailSent = true;
                //await _challengeRepository.UpdateAsync(challenge);

                await _context.SaveChangesAsync();
            }
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
