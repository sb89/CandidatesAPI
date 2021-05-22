using System.Collections.Generic;

namespace Application.Skills.Queries.GetAll
{
    public class GetAllVm
    {
        public IEnumerable<SkillDto> Skills { get; set; }
    }
}