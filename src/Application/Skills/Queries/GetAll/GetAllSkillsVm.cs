using System.Collections.Generic;

namespace Application.Skills.Queries.GetAll
{
    public class GetAllSkillsVm
    {
        public IEnumerable<SkillDto> Skills { get; set; }
    }
}