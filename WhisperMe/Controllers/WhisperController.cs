using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WhisperMe.Services.Interfaces;
using WhisperMe.ViewModels.Dtos;
using WhisperMe.ViewModels.Requests;

namespace WhisperMe.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WhisperController : Controller
    {
        private readonly IWhisperService _whisperService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public WhisperController(IWhisperService whisperService, IHttpContextAccessor httpContextAccessor)
        {
            _whisperService = whisperService;
            _httpContextAccessor = httpContextAccessor;
        }

        [AllowAnonymous]
        [HttpPost("SendWhisper")]
        public async Task<IActionResult> SendWhisper([FromBody] WhisperDTO request)
        {
            await _whisperService.SendWhisper(request);
            return NoContent();
        }

        [HttpDelete("RemoveWhisper")]
        public async Task<IActionResult> RemoveWhisper([FromBody] RemoveWhisperRequest request)
        {
            await _whisperService.RemoveWhisper(request.Guid);
            return NoContent();
        }

        [HttpGet("ListWhispers")]
        public async Task<IActionResult> ListWhispers()
        {
            var list = await _whisperService.ListWhispers(_httpContextAccessor.HttpContext.Request.Cookies.FirstOrDefault((jwt) => jwt.Key == "token").Value);
            return Ok(list);
        }

    }


}
