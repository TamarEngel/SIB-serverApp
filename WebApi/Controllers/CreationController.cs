using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;
using web.Core.DTOs;
using web.Core.models;
using web.Core.Service;
using web.Core.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreationController : ControllerBase
    {
        private readonly ICreationService _creationService;
        private readonly IS3Service _s3Service;

        public CreationController(ICreationService creationService, IS3Service s3Service)
        {
            _creationService = creationService;
            _s3Service = s3Service;
        }


        // ⬆️ שלב 1: קבלת URL להעלאת קובץ ל-S3
        [HttpGet("upload-url")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> GetUploadUrl([FromQuery] string fileName, [FromQuery] string contentType, [FromQuery] int challengeId)
        {
            if (string.IsNullOrEmpty(fileName))
                        return BadRequest("Missing file name");

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
                return Unauthorized("Invalid user ID");
            if (!await _creationService.IsValidAddCreationAsync(userId, challengeId))
                return BadRequest("Can`t Upload 2 creations!");

            var url = await _s3Service.GeneratePresignedUrlAsync(fileName, contentType);
            return Ok(new { url });
        }

        // ⬇️ שלב 2: קבלת URL להורדת קובץ מה-S3
        [HttpGet("download-url/{fileName}")]
        public async Task<IActionResult> GetDownloadUrl(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                return BadRequest("File name is required.");

            var url = await _s3Service.GetDownloadUrlAsync(fileName);

            if (string.IsNullOrEmpty(url))
                return NotFound("File not found.");

            return Ok(new { downloadUrl = url });
        }

        // GET: api/Creation
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Creation>>> GetAllCreationsAsync()
        {
            var list = await _creationService.GetAllCreationAsync();
            if (list == null || list.Count == 0)
                return NotFound("No creations found.");
            return Ok(list);
        }

        // GET api/Creation/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Creation>> GetCreationByIdAsync(int id)
        {
            Console.WriteLine("Controller");
            var creation = await _creationService.GetCreationByIdAsync(id);
            if (creation == null)
                return NotFound($"Creation with ID {id} not found.");
            return Ok(creation);
        }

        // POST api/Creation
        //[HttpPost]
        //[Authorize(Roles = "Admin,User")]
        //public async Task<ActionResult> AddCreationAsync([FromBody] CreationPostDTO creation)
        //{
        //    if (creation == null)
        //        return BadRequest("Invalid creation data.");

        //    var success = await _creationService.AddCreationAsync(creation);
        //    if (!success)
        //        return BadRequest("Failed to add creation");
        //    return Ok(new { message = "Creation added successfully." });
        //}

        [HttpPost]
        [Authorize(Roles = "Admin,User")]
        public async Task<ActionResult<Creation>> AddCreationAsync([FromBody] CreationPostDTO creation)
        {
            if (creation == null)
                return BadRequest("Invalid creation data.");

            var newCreation = await _creationService.AddCreationAsync(creation);
            if (newCreation == null)
                return BadRequest("Failed to add creation");

            return Ok(new { creation=newCreation,message = "Creation added successfully." });  // החזר את היצירה החדשה עם כל המידע (כולל ID)
        }


        // PUT api/Creation/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<ActionResult> UpdateCreationAsync(int id, [FromBody] CreationPostDTO creation)
        {
            if (creation == null)
                return BadRequest("Invalid creation data.");

            var success = await _creationService.UpdateCreationAsync(id, creation);
            if (!success)
                return NotFound($"Creation with ID {id} not found.");
            return Ok(new { message = "Creation updated successfully." });
        }

        // PATCH api/Creation/vote/5
        [HttpPatch("vote/{id}")]
        public async Task<ActionResult> UpdateCreationVoteAsync(int id)
        {
            var success = await _creationService.UpdateCreationVoteAsync(id);
            if (!success)
                return NotFound($"Creation with ID {id} not found.");
            return Ok(new { message = "Vote count updated successfully." });
        }


        // PATCH api/Creation/vote/5
        [HttpPatch("decreaseVote/{id}")]
        public async Task<ActionResult> DecreaseCreationVoteAsync(int id)
        {
            var success = await _creationService.DecreaseCreationVoteAsync(id);
            if (!success)
                return NotFound($"Creation with ID {id} not found.");
            return Ok(new { message = "Vote count updated successfully." });
        }

        // DELETE api/Creation/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<ActionResult> DeleteCreationAsync(int id)
        {
            var success = await _creationService.DeleteCreationAsync(id);
            if (!success)
                return NotFound($"Creation with ID {id} not found.");
            return Ok(new { message = "Creation deleted successfully." });
        }
    }
}
