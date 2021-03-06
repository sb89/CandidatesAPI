using System;
using Application.Candidates.Commands.Create;
using Application.Candidates.Commands.Update;
using Application.Candidates.Queries.Get;
using Application.Candidates.Queries.GetAll;
using Application.CandidateSkills.Commands.Create;
using Application.Skills.Queries.GetAll;
using AutoMapper;
using Domain.Entities;

namespace Application.Common.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<long, DateTimeOffset>().ConvertUsing(new LongDateTimeOffsetConverter());
            CreateMap<DateTimeOffset, long>().ConvertUsing(new DateTimeOffsetLongConverter());
            
            CreateMap<Candidate, CandidateDto>();
            CreateMap<CreateCandidateCommand, Candidate>();
            CreateMap<UpdateCandidateCommand, Candidate>();
            CreateMap<Candidate, GetCandidateVm>();

            CreateMap<Skill, SkillDto>();

            CreateMap<Skill, CandidateSkills.Queries.SkillDto>();

            CreateMap<AddSkillToCandidateCommand, CandidateSkill>();
            
        }
    }
}