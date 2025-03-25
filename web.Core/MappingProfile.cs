using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using web.Core.DTOs;
using web.Core.models;

namespace web.Core
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserPostDTO>().ReverseMap();
            CreateMap<User, UserUpdateDTO>().ReverseMap();
            CreateMap<Creation, CreationPostDTO>().ReverseMap();
            CreateMap<Challenge, ChallengePostDTO>().ReverseMap();
            CreateMap<Vote, VotePostDTO>().ReverseMap();

        }
    }
}
