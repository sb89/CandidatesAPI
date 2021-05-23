using System.Threading.Tasks;
using Application.Candidates.Commands.Create;
using Application.Candidates.Commands.Update;
using Application.Candidates.Queries.GetAll;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CandidatesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CandidatesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<GetAllCandidatesVm>> All()
        {
            return await _mediator.Send(new GetAllCandidatesQuery());
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<int>> Create(CreateCandidateCommand candidateCommand)
        {
            return await _mediator.Send(candidateCommand);
        }
        
        [HttpPost("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> Create(int id, UpdateCandidateCommand candidateCommand)
        {
            if (id != candidateCommand.Id)
            {
                return BadRequest();
            }
            
            await _mediator.Send(candidateCommand);

            return NoContent();
        }
    }
}