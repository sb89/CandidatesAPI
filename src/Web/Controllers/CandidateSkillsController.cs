using System.Threading.Tasks;
using Application.CandidateSkills.Commands.Create;
using Application.CandidateSkills.Commands.Delete;
using Application.CandidateSkills.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/candidates/{candidateId:int}/skills")]
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
        
        [HttpPost]
        public async Task<ActionResult<int>> Add(int candidateId, AddSkillToCandidateCommand command)
        {
            if (command.CandidateId != candidateId)
            {
                return BadRequest();
            }
            
            return await _mediator.Send(command);
        }
        
        [HttpDelete("{skillId}")]
        public async Task<ActionResult> Remove(int candidateId, int skillId)
        {
            var command = new RemoveSkillFromCandidateCommand
            {
                CandidateId = candidateId,
                SkillId = skillId
            };
            
            await _mediator.Send(command);

            return NoContent();
        }
    }
}