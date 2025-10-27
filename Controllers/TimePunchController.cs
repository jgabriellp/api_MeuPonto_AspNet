using MeuPonto.Model.Dto.RequestDto;
using MeuPonto.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace MeuPonto.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TimePunchController : ControllerBase
    {
        private readonly ITimePunchService _timePunchService;
        
        public TimePunchController(ITimePunchService timePunchService)
        {
            _timePunchService = timePunchService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTimePunchesAsync()
        {
            var timePunches = await _timePunchService.GetAllAsync();
            return Ok(timePunches);
        }

        [HttpGet("companyId/{companyId}")]
        public async Task<IActionResult> GetAllTimePunchesByCompanyIdAsync(long companyId)
        {
            var timePunches = await _timePunchService.GetAllByCompanyIdAsync(companyId);
            if (timePunches == null)
            {
                return NotFound();
            }
            return Ok(timePunches);
        }

        [HttpGet("userId/{userId}")]
        public async Task<IActionResult> GetAllTimePunchesByUserIdAsync(long userId)
        {
            var timePunches = await _timePunchService.GetAllByUserIdAsync(userId);
            if (timePunches == null)
            {
                return NotFound();
            }
            return Ok(timePunches);
        }

        [HttpGet("{id}", Name = "GetTimePunchById")]
        public async Task<IActionResult> GetTimePunchByIdAsync(long id)
        {
            var timePunch = await _timePunchService.GetByIdAsync(id);
            if (timePunch == null)
            {
                return NotFound();
            }
            return Ok(timePunch);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTimePunchAsync([FromBody] TimePunchRequestDto timePunch)
        {
            var createdTimePunch = await _timePunchService.CreateAsync(timePunch);
            if (createdTimePunch == null)
            {
                return BadRequest("Or time punch User or time punch Company doesn't exist");
            }

            return CreatedAtAction("GetTimePunchById", new { id = createdTimePunch.Id }, createdTimePunch);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTimePunchAsync(long id, [FromBody] TimePunchRequestDto timePunch)
        {
            var updatedTimePunch = await _timePunchService.UpdateAsync(id, timePunch);
            if (updatedTimePunch == false)
            {
                return NotFound("Or time punch or User or time punch Company doesn't exist");
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTimePunchAsync(long id)
        {
            var isDeleted = await _timePunchService.DeleteAsync(id);
            if (!isDeleted)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
