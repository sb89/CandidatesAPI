using System.Collections.Generic;

namespace Application.Candidates.Queries.GetAll
{
    public class GetAllVm
    {
        public IEnumerable<CandidateDto> Candidates { get; set; }
    }
}