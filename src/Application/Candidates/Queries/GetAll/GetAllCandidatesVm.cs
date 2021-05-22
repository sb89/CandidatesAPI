using System.Collections.Generic;

namespace Application.Candidates.Queries.GetAll
{
    public class GetAllCandidatesVm
    {
        public IEnumerable<CandidateDto> Candidates { get; set; }
    }
}