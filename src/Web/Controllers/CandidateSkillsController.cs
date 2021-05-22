using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [ApiController]
    [Route("candidates/{candidateId:int}/skills")]
    public class CandidateSkillsController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult> GetAll(int candidateId)
        {
            Console.WriteLine(candidateId);
            return Ok();
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