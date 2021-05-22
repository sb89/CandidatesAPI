using System;
using System.Threading.Tasks;
using Application.CandidateSkills.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [ApiController]
    [Route("candidates/{candidateId:int}/skills")]
    public class CandidateSkillsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CandidateSkillsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<GetSkillsForCandidateVm>> GetAll(int candidateId)
        {
            var command = new GetSkillsForCandidateQuery
            {
                CandidateId = candidateId
            };
            
            return await _mediator.Send(command);
        }
        
        [HttpPost("{skillId}")]
        public async Task<ActionResult> Add(int candidateId, int skillId)
        {
            throw new NotImplementedException();
        }
        
        [HttpDelete("{skillId}")]
        public async Task<ActionResult> Remove(int candidateId, int skillId)
        {
            throw new NotImplementedException();
        }
    }
}