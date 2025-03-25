using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using web.Core.DTOs;
using web.Core.models;
using web.Core.Repositories;
using web.Core.Service;
using web.Data.Repositories;

namespace web.Service
{
    public class ChallengeService:IChallengeService
    {
        private readonly IChallengeRepository _challengeRepository;
        private readonly IMapper _mapper;

        public ChallengeService(IChallengeRepository challengeRepository, IMapper mapper)
        {
            _challengeRepository = challengeRepository;
            _mapper = mapper;
        }

        public async Task<List<Challenge>> GetAllChallengesAsync()
        {
            return await _challengeRepository.GetAllChallengesAsync();
        }
        public async Task<List<Challenge>> GetActiveChallengesAsync()
        {
            return await _challengeRepository.GetActiveChallengesAsync();
        }
        public async Task<List<Challenge>> GetNotActiveChallengesAsync()
        {
            return await _challengeRepository.GetNotActiveChallengesAsync();
        }
        public async Task<Challenge> GetChallengeByIdAsync(int id)
        {
            return await _challengeRepository.GetChallengeByIdAsync(id);
        }

        public async Task<List<Challenge>> GetSortChallengeAsync()
        {
            return await _challengeRepository.GetSortChallengeAsync();
        }
        public async Task<List<Creation>> GetSortCreationsByChallengeAsync(int challengeId)
        {
            return await _challengeRepository.GetSortCreationsByChallengeAsync(challengeId);
        }

        public async Task<Creation> GetWinCreationAsync(int challengeId)
        {
            return await _challengeRepository.GetWinCreationAsync(challengeId);
        }

        public async Task<bool> AddChallengeAsync(ChallengePostDTO challenge)
        {
            var tmp = _mapper.Map<Challenge>(challenge);
            return await _challengeRepository.AddChallengeAsync(tmp);
        }

        public async Task<bool> UpdateChallengeAsync(int id, ChallengePostDTO challenge)
        {
            var tmp = _mapper.Map<Challenge>(challenge);
            return await _challengeRepository.UpdateChallengeAsync(id, tmp);
        }

        public async Task<bool> UpdateCountCreationsAsync(int id)
        {
            return await _challengeRepository.UpdateCountCreationsAsync(id);
        }

        public async Task<bool> DeleteChallengeAsync(int id)
        {
            return await _challengeRepository.DeleteChallengeAsync(id);
        }

        public async Task<bool> ProcessExpiredChallengesAsync()
        {
            return await _challengeRepository.ProcessExpiredChallengesAsync();
        }

    }
}
