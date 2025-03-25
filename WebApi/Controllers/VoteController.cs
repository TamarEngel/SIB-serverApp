using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using web.Core.DTOs;
using web.Core.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoteController : ControllerBase
    {
        private readonly IVoteService _voteService;
        private readonly IMapper _mapper;

        public VoteController(IVoteService voteService, IMapper mapper)
        {
            _voteService = voteService;
            _mapper = mapper;
        }

        [HttpPost]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> VoteImage([FromBody] VotePostDTO voteRequest)
        {
            try
            {
                if (await _voteService.IsSelfVotingAsync(voteRequest.CreationId, voteRequest.UserId))
                    return BadRequest("You cannot vote for your own image.");

                var vote = await _voteService.VoteImageAsync(voteRequest.UserId, voteRequest.CreationId);

                if (vote == null)
                    return BadRequest("User has already voted for this image.");

                var voteDto = _mapper.Map<VotePostDTO>(vote);
                return Ok(voteDto);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);  // אם יש שגיאה מותאמת מה-Repository
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }



        // DELETE api/<VoteController>
        [HttpDelete("deleteVote")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> Delete([FromBody] VotePostDTO voteRequest)
        {
            try
            {
                var result = await _voteService.DeleteVoteAsync(voteRequest.UserId, voteRequest.CreationId);
                if (!result)
                    return BadRequest("No vote found to delete.");

                return Ok("Vote deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpGet("hasVoted")]
        public async Task<IActionResult> HasUserVoted([FromQuery] int userId, [FromQuery] int creationId)
        {
            Console.WriteLine($"userId: {userId}, creationId: {creationId}");

            if (userId <= 0 || creationId <= 0)
                return BadRequest("Invalid parameters");

            bool hasVoted = await _voteService.HasUserVotedAsync(userId, creationId);
            return Ok(hasVoted);
        }
    }
}

