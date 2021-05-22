using System.Collections.Generic;

namespace Application.CandidateSkills.Queries
{
    public class GetSkillsForCandidateVm
    {
        public IEnumerable<SkillDto> Skills { get; set; }
    }
}