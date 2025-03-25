using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using web.Core.DTOs;
using web.Core.models;
using web.Core.Repositories;
using web.Core.Service;

namespace web.Service
{
    public class CreationService:ICreationService
    {
        private readonly ICreationRepository _creationRepository;
        private readonly IMapper _mapper;

        public CreationService(ICreationRepository creationRepository, IMapper mapper)
        {
            _creationRepository = creationRepository;
            _mapper = mapper;
        }

        public async Task<List<Creation>> GetAllCreationAsync()
        {
            return await _creationRepository.GetAllCreationAsync();
        }

        public async Task<Creation> GetCreationByIdAsync(int id)
        {
            return await _creationRepository.GetCreationByIdAsync(id);
        }
        public async Task<bool> IsValidAddCreationAsync(int userId, int challengeId)
        {
            return await _creationRepository.IsValidAddCreationAsync(userId, challengeId);
        }
        public async Task<Creation> AddCreationAsync(CreationPostDTO creation)
        {
            var tmp = _mapper.Map<Creation>(creation);
            return await _creationRepository.AddCreationAsync(tmp);
        }

        public async Task<bool> UpdateCreationAsync(int id, CreationPostDTO creation)
        {
            var tmp = _mapper.Map<Creation>(creation);
            return await _creationRepository.UpdateCreationAsync(id, tmp);
        }

        public async Task<bool> UpdateCreationVoteAsync(int id)
        {
            return await _creationRepository.UpdateCreationVoteAsync(id);
        }
        public async Task<bool> DecreaseCreationVoteAsync(int id)
        {
            return await _creationRepository.DecreaseCreationVoteAsync(id);
        }

        public async Task<bool> DeleteCreationAsync(int id)
        {
            return await _creationRepository.DeleteCreationAsync(id);
        }
    }
}
